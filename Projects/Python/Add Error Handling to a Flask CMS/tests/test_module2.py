## Imports
import pytest
from tests.utils import *
#!

## Paths
not_found_template = Path.cwd() / 'cms' / 'templates' / 'not_found.html'
not_found_template_exists = Path.exists(not_found_template) and Path.is_file(not_found_template)
error_template = Path.cwd() / 'cms' / 'templates' / 'error.html'
error_template_exists = Path.exists(error_template) and Path.is_file(error_template)
#!

## Tests
@pytest.mark.test_inject_titles_module2
def test_inject_titles_module2():
    # 01. Inject Titles
    # @app.context_processor
    # def inject_titles():
    #     titles = Content.query.with_entities(Content.slug, Content.title).join(Type).filter(Type.name == 'page')
    #     return dict(titles=titles)
    inject_titles = handlers_code().find('def', name='inject_titles')
    inject_titles_exists = inject_titles is not None
    assert inject_titles_exists, \
        'Have you created a function called `inject_titles`?'

    decorator_exists = inject_titles.find('decorator', lambda node: node.find('dotted_name', lambda node: \
          node.value[0].value == 'app' and \
          node.value[1].type == 'dot' and \
          node.value[2].value == 'context_processor')) is not None
    assert decorator_exists, \
        'The `inject_titles` function should have a decorator of `@app.context_processor`.'

    titles = inject_titles.find('assign', lambda node: node.target.value == 'titles')
    titles_exists = titles is not None
    assert titles_exists, \
        'Are you setting the `titles` variable correctly?'
    with_entities_call = titles.find('atomtrailers', lambda node: \
        node.value[0].value == 'Content' and \
        node.value[1].value == 'query' and \
        node.value[2].value == 'with_entities' and \
        node.value[3].type == 'call'
        )

    with_entities_call_exists = with_entities_call is not None
    assert with_entities_call_exists, \
        'Are you calling the `Content.query.with_entities()` function?'

    with_entities_call_node = with_entities_call.find('name', value='with_entities').next
    with_entities_args = get_args(with_entities_call_node)

    slug_arg = 'Content.slug' in with_entities_args
    assert slug_arg, \
        'Are you passing `Content.slug` to the `with_entities()` function?'

    title_arg = 'Content.title' in with_entities_args
    assert title_arg, \
        'Are you passing `Content.title` to the `with_entities()` function?'

    join_call = with_entities_call.find('name', value='join')
    join_call_exists = join_call is not None
    assert join_call_exists, \
        'Are you appending a call to `join()` on the `Content.query.with_entities()` call?'

    join_args = get_args(join_call.next)
    type_arg = 'Type' in join_args
    assert type_arg, \
        'Are you passing `Type` to the `join()` function?'

    filter_call = with_entities_call.find('name', value='filter')
    filter_call_exists = filter_call is not None
    assert filter_call_exists, \
        'Are you appending a call to `filter()` on the `join()` call?'
    filter_args = get_args(filter_call.next)

    page_arg = 'Type.name=="page"' in filter_args
    assert page_arg, \
        'Are you passing the correct condition to the `filter()` function?'

    return_dict = inject_titles.find('atomtrailers', lambda node:
        node.parent.type == 'return' and \
        node.value[0].value == 'dict' and \
        node.value[1].type == 'call')
    return_dict_exists = return_dict is not None
    assert return_dict_exists, \
        'Are you returning a `dict()` from the `inject_titles()` function?'

    return_dict_args = 'titles:titles' in get_args(return_dict.find('call'))
    assert return_dict_args, \
        'Are you passing a `titles` keyword argument set to `titles` to `dict()`?'

@pytest.mark.test_not_found_template_module2
def test_not_found_template_module2():
    # 02. Not Found Template
    # Create `templates/not_found.html`
    assert not_found_template_exists, \
        'Have you created a `not_found.html` file in the `cms/templates` folder?'

    extends_base = 'base.html' in template_extends('not_found')
    assert extends_base, \
        'The `not_found.html` template does not extend `base.html`.'

    content_block = 'content' in template_block('not_found')
    assert content_block, \
        'Have you added a template `block` called `content`?'

    url_for_call = 'index:slug:home' in template_functions('not_found', 'url_for')
    assert url_for_call, \
        'Are you redirecting the user back Home with a call to `url_for()`.'

@pytest.mark.test_not_found_handler_module2
def test_not_found_handler_module2():
    # 03. Not Found Handler
    # @app.errorhandler(404)
    # def page_not_found(e):
    #     return render_template('not_found.html'), 404
    def_page_not_found = handlers_code().find('def', lambda node: \
        node.name == 'page_not_found' and \
        node.arguments[0].target.value == 'e')
    def_page_not_found_exists = def_page_not_found is not None
    assert def_page_not_found_exists, \
        'Have you created a function called `page_not_found` with a parameter of `e`?'

    decorator = def_page_not_found.find('decorator', lambda node: node.find('dotted_name', lambda node: \
          node.value[0].value == 'app' and \
          node.value[1].type == 'dot' and \
          node.value[2].value == 'errorhandler' and \
          node.parent.call.value[0].value.value == '404'))

    decorator_exists = decorator is not None
    assert decorator_exists, \
        'The `page_not_found` function should have a decorator of `@app.errorhandler(404)`.'

    return_404 = def_page_not_found.find('tuple', lambda node: \
        node.parent.type == 'return' and \
        node.value[0].value[0].value == 'render_template' and \
        node.value[0].value[1].value[0].value.value.replace("'", '"') == '"not_found.html"' and \
        node.value[-1].value == '404') is not None
    assert return_404, \
        'The `page_not_found` function should render the `not_found.html` template with a `404`.'

@pytest.mark.test_error_log_module2
def test_error_log_module2():
    # 04. Error Log
    # error_log = configure_logging('error', ERROR)
    error_log = handlers_code().find('assign', lambda node: node.target.value == 'error_log')
    error_log_exists = error_log is not None
    assert error_log_exists, \
        'Are you setting the `error_log` variable correctly?'

    configure_logging_call = error_log.find('atomtrailers', lambda node: \
        node.value[0].value == 'configure_logging' and \
        node.value[1].type == 'call'
        )
    configure_logging_call_exists = configure_logging_call is not None
    assert configure_logging_call_exists, \
        'Are you calling the `configure_logging()` function and assigning the result to `error_log`?'

    configure_logging_args = get_args(configure_logging_call[1])

    first_arg = len(configure_logging_args) >= 1 and configure_logging_args[0] == '"error"'
    assert first_arg, \
        'Are you passing the correct name to `configure_logging()`?'

    second_arg = configure_logging_args[1] == 'ERROR'
    assert second_arg, \
        'Are you passing the correct level to `configure_logging()`?'

    arg_count = len(configure_logging_args) == 2
    assert arg_count, \
        'Are you passing the correct number of arguments to `configure_logging()`?'

@pytest.mark.test_error_handler_module2
def test_error_handler_module2():
    # 05. Error Handler
    # from traceback import format_exc
    # @app.errorhandler(Exception)
    # def handle_exception(e):
    #     tb = format_exc()
    traceback_import = get_imports(handlers_code(), 'traceback')
    traceback_import_exits = traceback_import is not None
    assert traceback_import_exits, \
        'Do you have a `traceback` import statement?'
    format_exc_exists = 'format_exc' in traceback_import
    assert format_exc_exists, \
        'Are you importing `format_exc` from `traceback` in `cms/handlers.py`?'

    def_handle_exception = handlers_code().find('def', lambda node: \
        node.name == 'handle_exception' and \
        node.arguments[0].target.value == 'e')
    def_handle_exception_exists = def_handle_exception is not None
    assert def_handle_exception_exists, \
        'Have you created a function called `handle_exception` with a parameter of `e`?'

    decorator = def_handle_exception.find('decorator', lambda node: node.find('dotted_name', lambda node: \
          node.value[0].value == 'app' and \
          node.value[1].type == 'dot' and \
          node.value[2].value == 'errorhandler' and \
          node.parent.call.value[0].value.value == 'Exception'))

    decorator_exists = decorator is not None
    assert decorator_exists, \
        'The `handle_exception` function should have a decorator of `@app.errorhandler(Exception)`.'

    tb = def_handle_exception.find('assign', lambda node: node.target.value == 'tb')
    tb_exists = tb is not None
    assert tb_exists, \
        'Are you setting the `tb` variable correctly?'
    format_exc_call = tb.find('atomtrailers', lambda node: \
        node.value[0].value == 'format_exc' and \
        node.value[1].type == 'call'
        )
    format_exc_call_exists = format_exc_call is not None
    assert format_exc_call_exists, \
        'Are you calling the `format_exc()` function and assigning the result to `tb`?'

@pytest.mark.test_error_log_format_module2
def test_error_log_format_module2():
    # 06. Error Log Format
    # error_log.error('%s - - %s "%s %s %s" 500 -\n%s', request.remote_addr, timestamp, request.method, request.path, request.scheme.upper(), tb)
    def_handle_exception = handlers_code().find('def', lambda node: \
        node.name == 'handle_exception' and \
        node.arguments[0].target.value == 'e')
    def_handle_exception_exists = def_handle_exception is not None
    assert def_handle_exception_exists, \
        'Have you created a function called `handle_exception` with a parameter of `e`?'

    error_call = def_handle_exception.find('atomtrailers', lambda node: \
        node.value[0].value == 'error_log' and \
        node.value[1].value == 'error' and \
        node.value[2].type == 'call')
    error_call_exists = error_call is not None
    assert error_call_exists, \
        'Are you calling the `error_log.error()` function?'

    error_args = get_args(error_call[-1], False)

    first_arg = len(error_args) >= 1 and error_args[0] == '\'%s - - %s "%s %s %s" 500 -\\n%s\''
    assert first_arg, \
        'Are you passing the correct log format to `error_log.error()` as the first argument?'

    second_arg = len(error_args) >= 2 and error_args[1] == 'request.remote_addr' 
    assert second_arg, \
        'Are you passing the `request.remote_addr` to `error_log.error()` as the second argument?'

    third_arg = len(error_args) >= 3 and error_args[2] == 'timestamp' 
    assert third_arg, \
        'Are you passing `timestamp` to `error_log.error()` as the third argument?'

    fourth_arg = len(error_args) >= 4 and error_args[3] == 'request.method' 
    assert fourth_arg, \
        'Are you passing `request.method` to `error_log.error()` as the fourth argument?'

    fifth_arg = len(error_args) >= 5 and error_args[4] == 'request.path' 
    assert fifth_arg, \
        'Are you passing `request.path` to `error_log.error()` as the fifth argument?'

    sixth_arg = len(error_args) >= 6 and error_args[5] == 'request.scheme.upper()' 
    assert sixth_arg, \
        'Are you passing `request.scheme.upper()` to `error_log.error()` as the sixth argument?'

    seventh_arg = len(error_args) == 7 and error_args[6] == 'tb' 
    assert seventh_arg, \
        'Are you passing `tb` to `error_log.error()` as the seventh argument?'

    arg_count = len(error_args) == 7
    assert arg_count, \
        'Are you passing the correct number of arguments to `error_log.error()`?'

@pytest.mark.test_error_template_module2
def test_error_template_module2():
    # 07. Error Template
    # Create `templates/error.html`
    assert error_template_exists, \
        'Have you created a `error.html` file in the `cms/templates` folder?'

    extends_base = 'base.html' in template_extends('error')
    assert extends_base, \
        'The `error.html` template does not extend `base.html`.'

    content_block = 'content' in template_block('error')
    assert content_block, \
        'Have you added a template `block` called `content`?'

    error_varaible = 'error' in template_variables('error')
    assert error_varaible, \
        'Have you added a template variable called `error` to the `content` block?'

    url_for_call = 'index:slug:home' in template_functions('error', 'url_for')
    assert url_for_call, \
        'Are you redirecting the user back Home with a call to `url_for()`.'

@pytest.mark.test_render_original_error_template_module2
def test_render_original_error_template_module2():
    # 08. Render Original Error Template
    # original = getattr(e, 'original_exception', None)
    # return render_template('error.html', error=original), 500
    def_handle_exception = handlers_code().find('def', lambda node:
        node.name == 'handle_exception' and \
        node.arguments[0].target.value == 'e')
    def_handle_exception_exists = def_handle_exception is not None
    assert def_handle_exception_exists, \
        'Have you created a function called `handle_exception` with a parameter of `e`?'

    getattr_call = def_handle_exception.find('assign', lambda node:
        node.value.value[0].value == 'getattr' and \
        node.value.value[1].type == 'call')
    getattr_call_exists = getattr_call is not None
    assert getattr_call_exists, \
        'Are you calling the `getattr` function and assigning it to a variable?'

    getattr_variable = getattr_call.target.value

    getattr_args = get_args(getattr_call.find('call'))

    arg_count = len(getattr_args) == 3
    assert arg_count, \
        'Are you passing the correct number of arguments to `getattr()`?'

    first_arg = getattr_args[0] == 'e'
    assert first_arg, \
        'Are you passing `e` to `getattr()` as the first argument?'

    second_arg = getattr_args[1] == '"original_exception"'
    assert second_arg, \
        'Are you passing `"original_exception"` to `getattr()` as the second argument?'

    third_arg = getattr_args[2] == 'None'
    assert third_arg, \
        'Are you passing `None` to `getattr()` as the third argument?'

    return_500 = def_handle_exception.find('tuple', lambda node:
        node.parent.type == 'return' and \
        node.value[0].value[0].value == 'render_template' and \
        node.value[0].value[1].value[0].value.value.replace("'", '"') == '"error.html"' and \
        len(node.value[0].value[1].value) == 2 and \
        node.value[0].value[1].value[1].target.value == 'error' and \
        node.value[0].value[1].value[1].value.value == getattr_variable and \
        node.value[-1].value == '500') is not None

    assert return_500, \
        'The `handle_exception` function should render the `error.html` template with a `500`. Pass the keyword argument `error` set to original.'

@pytest.mark.test_render_simple_error_template_module2
def test_render_simple_error_template_module2():
    # 09. Render Simple Error Template
    # if original is None:
    #     return render_template('error.html'), 500
    def_handle_exception = handlers_code().find('def', lambda node: \
        node.name == 'handle_exception' and \
        node.arguments[0].target.value == 'e')
    def_handle_exception_exists = def_handle_exception is not None
    assert def_handle_exception_exists, \
        'Have you created a function called `handle_exception` with a parameter of `e`?'

    getattr_call = def_handle_exception.find('assign', lambda node:
        node.value.value[0].value == 'getattr' and \
        node.value.value[1].type == 'call')
    getattr_call_exists = getattr_call is not None
    assert getattr_call_exists, \
        'Are you calling the `getattr` function and assigning it to a variable?'

    getattr_variable = getattr_call.target.value

    original_if = get_conditional(def_handle_exception, ['{}:is:None'.format(getattr_variable)], 'if')
    original_if_exists = original_if is not None
    assert original_if_exists, \
        'Do you have an `if` statement that tests if `{}` is `None`?'.format(getattr_variable)

    return_500 = original_if.parent.find('tuple', lambda node: \
        node.parent.type == 'return' and \
        node.value[0].value[0].value == 'render_template' and \
        node.value[0].value[1].value[0].value.value.replace("'", '"') == '"error.html"' and \
        node.value[-1].value == '500')

    assert return_500, \
        'Are you rendering the `error.html` template with a `500` in the `if` statement'
#!