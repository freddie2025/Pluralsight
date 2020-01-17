import re
import parso
from bs4 import BeautifulSoup
from jinja2 import Environment, PackageLoader, exceptions, meta, nodes
from pathlib import Path
from redbaron import RedBaron

def get_source_code(filename, admin=False):
    if admin:
        file_path = Path.cwd() / 'cms' / 'admin' / filename
    else:
        file_path = Path.cwd() / 'cms' / filename
    grammar = parso.load_grammar()
    module = grammar.parse(path=file_path.resolve())
    parse_error = len(grammar.iter_errors(module)) == 0
    try:
        error_message = grammar.iter_errors(module)[0].message
        error_start_pos = grammar.iter_errors(module)[0].start_pos[0]
    except IndexError:
        error_message = ''
        error_start_pos = ''
    message = '{} on or around line {} in `{}`.' \
        .format(error_message, error_start_pos, file_path.name)
    assert parse_error, message

    with open(file_path.resolve(), 'r') as source_code:
        return RedBaron(source_code.read())

def handlers_code():
    return get_source_code('handlers.py')

def auth_code():
    return get_source_code('auth.py', True)

def rq(string):
    return re.sub(r'(\'|")', '', str(string))

def tqrw(string):
    return str(string).replace("'", '"').replace(' ', '')

def parsed_content(name, path='templates'):
    try:
        env = Environment(loader=PackageLoader('cms', path))
        return env.parse(env.loader.get_source(env, name + '.html')[0])
    except exceptions.TemplateNotFound:
        return None

def template_data(name):
    html = ''
    for node in parsed_content(name).find_all(nodes.TemplateData):
        html += node.data
    return BeautifulSoup(html, 'html.parser')

def simplify(main):
    def _simplify(node):
        if not isinstance(node, nodes.Node):
            if isinstance(node, (type(None), bool)):
                buf.append(repr(node))
            else:
                buf.append(node)
            return

        for idx, field in enumerate(node.fields):
            value = getattr(node, field)
            if value == 'load' or value == 'store':
                return
            if idx:
                buf.append('.')
            if isinstance(value, list):
                for idx, item in enumerate(value):
                    if idx:
                        buf.append('.')
                    _simplify(item)
            else:
                _simplify(value)

    buf = []
    _simplify(main)
    return ''.join(buf)

def get_calls(name):
    calls = []
    for node in parsed_content(name).find_all(nodes.Call):
        calls.append(simplify(node))
    return calls

def get_imports(code, value):
    imports = code.find_all('from_import',  lambda node: ''.join(list(node.value.node_list.map(lambda node: str(node)))) == value).find_all('name_as_name')
    return list(imports.map(lambda node: node.value))

def get_conditional(code, values, type, nested=False):
    def flat(node):
        if node.type == 'comparison':
            return '{}:{}:{}'.format(str(node.first).replace("'", '"'), str(node.value).replace(' ', ':'), str(node.second).replace("'", '"'))
        elif node.type == 'unitary_operator':
            return '{}:{}'.format(str(node.value), str(node.target).replace("'", '"'))

    nodes = code.value if nested else code
    for value in values:
        final_node = nodes.find_all(type).find(['comparison', 'unitary_operator'], lambda node: flat(node) == value)
        if final_node is not None:
            return final_node
    return None

def get_route(code, route):
    route_function = code.find('def', name=route)
    route_function_exists = route_function is not None
    assert route_function_exists, \
        'Does the `{}` route function exist in `cms/admin/__init__.py`?'.format(route)
    return route_function

def get_methods_keyword(code, route):
    methods_keyword = get_route(code, route).find_all('call_argument', lambda node: \
        str(node.target) == 'methods')
    methods_keyword_exists = methods_keyword is not None
    assert methods_keyword_exists, \
        'Does the `{}` route have a keyword argument of `methods`?'.format(name)
    return methods_keyword

def get_request_method(code, route, parent=True):
    request_method = get_route(code, route).find('comparison', lambda node: \
        'request.method' in [str(node.first), str(node.second)])
    request_method_exists = request_method is not None
    assert request_method_exists, \
        'Do you have an `if` statement in the `{}` route that checks `request.method`?'.format(route)
    return request_method.parent if parent else request_method

def get_form_data(code, route, values, name):
    index = list(get_request_method(code, route).find_all('atomtrailers', lambda node: \
        node.parent.type == 'assignment' and \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].type == 'getitem').map(lambda node: rq(node.value[2].value)))

    get = list(get_request_method(code, route).find_all('atomtrailers', lambda node: \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].value == 'get' and \
        node.value[3].type == 'call').map(lambda node: rq(node.value[3].value[0].value)))

    diff = list(set(index + get) - values)
    diff_exists = len(diff) == 0
    message = 'You have extra `request.form` statements. You can remove those for these varaibles {}'.format(diff)
    assert diff_exists, message
    
    assignment = get_request_method(code, route).find('assign', lambda node: \
        str(node.target) == name)
    assignment_exists = assignment is not None
    assert assignment_exists, \
        'Do you have a variable named `{}`?'.format(name)
    
    name_as_string = '"{}"'.format(name.replace('content.', ''))
    sub_name = '[{}]'.format(name_as_string)
    
    right = assignment.find('atomtrailers', lambda node: \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].type == 'getitem' and \
        node.value[2].find('string', lambda node: str(node.value).replace("'", '"') == name_as_string)) is not None
    
    right_get = assignment.find('atomtrailers', lambda node: \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].value == 'get' and \
        node.value[3].type == 'call' and \
        node.value[3].find('string', lambda node: str(node.value).replace("'", '"') == name_as_string)) is not None
    
    assert right or right_get, \
        'Are you setting the `{}` variable to the correct form data?'.format(name)

def get_args(nodes, rq=True):
    args = []
    if nodes is not None: 
        for node in nodes:
            if node.target is None:
                if rq:
                    args.append(tqrw(node.value))
                else:
                    args.append(str(node.value))
            else:
                if rq:
                    args.append('{}:{}'.format(tqrw(node.target.value), tqrw(node.value)))
                else:
                    args.append('{}:{}'.format(node.target.value, str(node.value)))
    return args

def select_code(content, start, end):
    found = False
    code = []

    if isinstance(content, str):
        parsed = parsed_content(content)
    elif isinstance(content, nodes.Node):
        parsed = content
    else:
        return []

    for node in parsed.find_all(nodes.Node):
        if isinstance(node, nodes.TemplateData) and bool(re.search(start, node.data)):
            found = True

        if isinstance(node, nodes.TemplateData) and bool(re.search(end, node.data)):
            found = False

        if found and not isinstance(node, nodes.TemplateData):
            code.append(node)
    return code

def template_functions(name, function_name):
    functions = []

    for call in parsed_content(name).find_all(nodes.Call):
        if call.node.name == function_name:
            args_string = ''
            if isinstance(call.node, nodes.Name) and isinstance(call.args[0], nodes.Name):
                args_string += call.node.name + ':' + call.args[0].name
            else:
                args = getattr(call, 'args')[0]
                if isinstance(args, nodes.Const):
                    args_string += args.value + ':'
                kwargs = call.kwargs[0] if len(getattr(call, 'kwargs')) > 0 else getattr(call, 'kwargs')
                if isinstance(kwargs, nodes.Keyword):
                    args_string += kwargs.key + ':'
                    if isinstance(kwargs.value, nodes.Const):
                        args_string += kwargs.value.value
                    else:
                        if isinstance(kwargs.value, nodes.Name):
                            args_string += kwargs.value.name
                        else:
                            args_string += kwargs.value.node.name
                            if isinstance(kwargs.value.arg, nodes.Const):
                                args_string += ':' + kwargs.value.arg.value
            functions.append(args_string)

    return functions

def template_variables(name):
    return [node.name for node in parsed_content(name).find_all(nodes.Name)]

def template_block(name):
    blocks = []
    for block in parsed_content(name).find_all(nodes.Block):
        blocks.append(block.name)
    return blocks

def template_extends(name):
    return list(meta.find_referenced_templates(parsed_content(name)))
