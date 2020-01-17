import ast
import inspect
import json
import os
import collections

from bs4 import BeautifulSoup
from jinja2 import Environment, PackageLoader, exceptions, meta, nodes

env = Environment(loader=PackageLoader('jobs', 'templates'))

def flatten(d, parent_key='', sep='_'):
    items = []
    for k, v in d.items():
        new_key = parent_key + sep + k if parent_key else k
        if isinstance(v, collections.MutableMapping):
            items.extend(flatten(v, new_key, sep=sep).items())
        else:
            items.append((new_key, v))
    return dict(items)

def get_decorators(source):
    decorators = {}
    def visit_FunctionDef(node):
        decorators[node.name] = []
        for n in node.decorator_list:
            name = ''
            if isinstance(n, ast.Call):
                name = n.func.attr if isinstance(n.func, ast.Attribute) else n.func.id
            else:
                name = n.attr if isinstance(n, ast.Attribute) else n.id

            args = [a.s for a in n.args] if hasattr(n, 'args') else []
            decorators[node.name].append((name, args))

    node_iter = ast.NodeVisitor()
    node_iter.visit_FunctionDef = visit_FunctionDef
    node_iter.visit(ast.parse(inspect.getsource(source)))
    return decorators

def get_functions(source):
    functions = []

    def visit_Call(node):
        path = node.func.attr if isinstance(node.func, ast.Attribute) else node.func.id
        if len(node.args) != 0:
            path += ':' + ':'.join([str(val) for arg in node.args for val in build_dict(arg).values()])

        if len(node.keywords) != 0:
            path += ':' + ':'.join([str(val) for keyword in node.keywords for val in build_dict(keyword).values()])

        functions.append(path)

    node_iter = ast.NodeVisitor()
    node_iter.visit_Call = visit_Call
    node_iter.visit(ast.parse(inspect.getsource(source)))
    return functions

def get_functions_returns(source):
    returns = []

    def visit_Return(node):
        returns.append(build_dict(node))

    node_iter = ast.NodeVisitor()
    node_iter.visit_Return = visit_Return
    node_iter.visit(ast.parse(inspect.getsource(source)))
    return returns

def get_statements(source):
    statements = []

    def visit_If(node):
        statements.append(build_dict(node))

    node_iter = ast.NodeVisitor()
    node_iter.visit_If = visit_If
    node_iter.visit(ast.parse(inspect.getsource(source)))
    return statements

def build_dict(node):
    result = {}
    if node.__class__.__name__ == 'Is' or node.__class__.__name__ == 'Eq':
        result['node_type'] = node.__class__.__name__
    for attr in dir(node):
        if not attr.startswith("_") and attr != 'ctx' and attr != 'lineno' and attr != 'col_offset':
            value = getattr(node, attr)
            if isinstance(value, ast.AST):
                value = build_dict(value)
            elif isinstance(value, list):
                final = [build_dict(n) for n in value]
                value = final[0] if len(final) == 1 else final
            if value != []:
                result[attr] = value
    return flatten(result, sep='/')

def list_routes(app):
    rules = []

    for rule in app.url_map.iter_rules():
        methods = ','.join(sorted(rule.methods))
        if rule.endpoint is not 'static':
            rules.append(rule.endpoint + ':' + methods + ':' + str(rule))

    return rules

def template_values(name, function):
    values = []

    for call in parsed_content(name).find_all(nodes.Call):
        if call.node.name == function:
            values.append(call.args[0].value + ':' + call.kwargs[0].key + ':' +  call.kwargs[0].value.value)

    return values

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

def show_jobs_for():
    values = []
    for node in parsed_content('_macros').find_all(nodes.For):
        values.append(node.target.name + ':' + node.iter.name)

    for call in parsed_content('_macros').find_all(nodes.Call):
        if call.node.name == 'show_job' and call.args[0].name == 'job':
            values.append('show_job:job')

    return values

def employer_for():
    values = []

    for node in parsed_content('employer').find_all(nodes.For):
        path = node.target.name
        if isinstance(node.iter, nodes.Name):
            path += ':' + node.iter.name
        elif isinstance(node.iter, nodes.Call):
            path += ':' + node.iter.node.name + ':' + str(node.iter.args[0].value) + ':' + str(node.iter.args[1].node.name) + ':' + str(node.iter.args[1].arg.value)
        values.append(path)

    return values

def template_macros(name):
    macros = []
    for macro in parsed_content(name).find_all(nodes.Macro):
        macros.append(macro.name + ':' + macro.args[0].name)
    return macros

def template_block(name):
    blocks = []
    for block in parsed_content(name).find_all(nodes.Block):
        blocks.append(block.name)
    return blocks

def template_macro_soup(name, macro_name):
    for macro in parsed_content(name).find_all(nodes.Macro):
        if macro.name == macro_name:
            html = ''
            for template_data in macro.find_all(nodes.TemplateData):
                html += template_data.data
    return source_soup(html)

def template_data(name):
    html = ''
    for node in parsed_content(name).find_all(nodes.TemplateData):
        html += node.data
    return source_soup(html)

def template_variables(name):
    return [item.node.name + ':' + item.arg.value for item in parsed_content(name).find_all(nodes.Getitem)]

def template_exists(name):
    return os.path.isfile('jobs/templates/' + name + '.html')

def template_source(name):
    try:
        return env.loader.get_source(env, name + '.html')[0]
    except exceptions.TemplateNotFound:
        return None

def source_soup(source):
    return BeautifulSoup(source, 'html.parser')

def template_soup(name):
    return BeautifulSoup(template_source(name), 'html.parser')

def template_find(name, tag, limit=None):
    return BeautifulSoup(template_source(name), 'html.parser').find_all(tag, limit=limit)

def parsed_content(name):
    return env.parse(template_source(name))

def template_extends(name):
    return list(meta.find_referenced_templates(parsed_content(name)))

def template_import(name):
    for node in parsed_content(name).find_all(nodes.FromImport):
        return node.template.value + ':' + ':'.join(node.names) + ':' + str(node.with_context)