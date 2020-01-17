## Imports
import pytest
import re

from pathlib import Path
from redbaron import RedBaron

from tests.utils import *
#!

## Paths
admin = Path.cwd() / 'cms' / 'admin'
admin_module = admin / '__init__.py'
admin_templates = admin / 'templates' / 'admin'
content_form = admin_templates / 'content_form.html'
content_path = admin_templates / 'content.html'

admin_exists = Path.exists(admin) and Path.is_dir(admin)
admin_module_exists = Path.exists(admin_module) and Path.is_file(admin_module)
admin_templates_exists = Path.exists(admin_templates) and Path.is_dir(admin_templates)
content_form_exists = Path.exists(content_form) and Path.is_file(content_form)
content_exists = Path.exists(content_path) and Path.is_file(content_path)
content_form_template = template_data('content_form')
#!

## Module Functions
def rq(string):
    return re.sub(r'(\'|")', '', str(string))

def admin_module_code():
    with open(admin_module.resolve(), 'r') as admin_module_source_code:
        return RedBaron(admin_module_source_code.read())

def get_route(route):
    route_function = admin_module_code().find('def', name=route)
    route_function_exists = route_function is not None
    assert route_function_exists, \
        'Does the `{}` route function exist in `cms/admin/__init__.py`?'.format(route)
    return route_function

def get_methods_keyword(route):
    methods_keyword = get_route(route).find_all('call_argument', lambda node: \
        str(node.target) == 'methods')
    methods_keyword_exists = methods_keyword is not None
    assert methods_keyword_exists, \
        'Does the `{}` route have a keyword argument of `methods`?'.format(name)
    return methods_keyword

def get_request_method(route, parent=True):
    request_method = get_route(route).find('comparison', lambda node: \
        'request.method' in [str(node.first), str(node.second)])
    request_method_exists = request_method is not None
    assert request_method_exists, \
        'Do you have an `if` statement in the `{}` route that checks `request.method`?'.format(route)
    return request_method.parent if parent else request_method

def get_form_data(route, name):
    index = list(get_request_method(route).find_all('atomtrailers', lambda node: \
        node.parent.type == 'assignment' and \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].type == 'getitem').map(lambda node: rq(node.value[2].value)))

    get = list(get_request_method(route).find_all('atomtrailers', lambda node: \
        node.value[0].value == 'request' and \
        node.value[1].value == 'form' and \
        node.value[2].value == 'get' and \
        node.value[3].type == 'call').map(lambda node: rq(node.value[3].value[0].value)))

    diff = list(set(index + get) - {'slug', 'type_id', 'body', 'title'})
    diff_exists = len(diff) == 0
    message = 'You have extra `request.form` statements. You can remove those for these varaibles {}'.format(diff)
    assert diff_exists, message

    assignment = get_request_method(route).find('assign', lambda node: \
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
        'Are you setting the `{}` varaible to the correct form data?'.format(name)
#!

## Tests
@pytest.mark.test_edit_route_module3
def test_edit_route_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    accept_id = get_route('edit')\
        .find('def_argument', lambda node: node.target.value == 'id') is not None
    assert accept_id, \
        'Is the `edit` route function accepting an argument of `id`?'

    content = get_route('edit').find('assign', lambda node: node.target.value == 'content')
    content_exists = content is not None
    assert content_exists, \
        'Are you setting the `content` variable correctly?'
    query_call = content.find('atomtrailers', lambda node: \
        node.value[0].value == 'Content' and \
        node.value[1].value == 'query' and \
        node.value[2].value == 'get_or_404' and \
        node.value[3].type == 'call' and \
        node.value[3].value[0].value.value == 'id'
        ) is not None
    assert query_call, \
        'Are you calling the `Content.query.get_or_404()` function and are you passing in the correct argument?'

    edit_decorator = get_route('edit').find('dotted_name', lambda node: \
        node.value[0].value == 'admin_bp' and \
        node.value[1].type == 'dot' and \
        node.value[2].value == 'route' and \
        node.parent.call.type == 'call' and \
        bool(re.search('/edit/<(int:)?id>', node.parent.call.value[0].value.value))
        ) is not None
    assert edit_decorator, \
        'Have you add a route decorator to the `edit` route function? Are you passing the correct route pattern?'

    strings = list(get_methods_keyword('edit').find_all('string').map(lambda node: node.value.replace("'", '"')))
    post_check = '"GET"' in strings and '"POST"' in strings
    assert post_check, \
        'Have you added the `methods` keyword argument to the `edit` route allowing `POST` and `GET`?'

@pytest.mark.test_edit_route_queries_module3
def test_edit_route_queries_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    type = get_route('edit').find('assign', lambda node: \
        node.target.value == 'type')
    type_exists = type is not None
    assert type_exists, \
        'Are you setting the `type` variable correctly?'
    get_call = type.find('atomtrailers', lambda node: \
        node.value[0].value == 'Type' and \
        node.value[1].value == 'query' and \
        node.value[2].value == 'get' and \
        node.value[3].type == 'call'
        )
    get_call_exists = get_call is not None
    assert get_call_exists, \
        'Are you calling the `Type.query.get()` function and assigning the result to `type`?'

    get_call_argument = get_call.find('call_argument', lambda node: \
        node.value[0].value == 'content' and \
        node.value[1].value == 'type_id') is not None
    assert get_call_argument, \
        'Are you passing the correct argument to the `Type.query.get()` function?'

    types = get_route('edit').find('assign', lambda node: \
        node.target.value == 'types')
    types_exists = types is not None
    assert types_exists, \
        'Are you setting the `types` variable correctly?'
    all_call = types.find('atomtrailers', lambda node: \
        node.value[0].value == 'Type' and \
        node.value[1].value == 'query' and \
        node.value[2].value == 'all' and \
        node.value[3].type == 'call'
        ) is not None
    assert all_call, \
        'Are you calling the `Type.query.all()` function and assigning the result to `types`?'

@pytest.mark.test_edit_route_render_template_module3
def test_edit_route_render_template_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    return_render = get_route('edit').find('return', lambda node: \
        node.value[0].value == 'render_template' and \
        node.value[1].type == 'call')
    return_render_exists = return_render is not None
    assert return_render_exists, \
        'Are you returning a call to the `render_template()` function?'

    return_render_args = list(return_render.find_all('call_argument').map(lambda node: str(node.target) + ':' + str(node.value).replace("'", '"')))
    template_exists = 'None:"admin/content_form.html"' in return_render_args
    assert template_exists, \
        'Are you passing the correct HTML template to the `render_template()` function?'

    types_exists = 'types:types' in return_render_args
    assert types_exists, \
        'Are you passing a `types` keyword argument set to `types` to the `render_template()` function?'

    title_exists = 'title:"Edit"' in return_render_args
    assert title_exists, \
        'Are you passing a `title` keyword argument set to `"Edit"` to the `render_template()` function?'

    item_title_exists = 'item_title:content.title' in return_render_args
    assert item_title_exists, \
        'Are you passing a `item_title` keyword argument set to `content.title` to the `render_template()` function?'

    slug_exists = 'slug:content.slug' in return_render_args
    assert slug_exists, \
        'Are you passing a `slug` keyword argument set to `content.slug` to the `render_template()` function?'

    type_name_exists = 'type_name:type.name' in return_render_args
    assert type_name_exists, \
        'Are you passing a `type_name` keyword argument set to `type.name` to the `render_template()` function?'

    type_id_exists = 'type_id:content.type_id' in return_render_args
    assert type_id_exists, \
        'Are you passing a `type_id` keyword argument set to `content.type_id` to the `render_template()` function?'

    body_exists = 'body:content.body' in return_render_args
    assert body_exists, \
        'Are you passing a `body` keyword argument set to `content.body` to the `render_template()` function?'

    argument_count = len(return_render_args) == 8
    assert argument_count, \
        'Are you passing the correct number of keyword arguments to the `render_template()` function?'

@pytest.mark.test_template_populate_form_controls_module3
def test_template_populate_form_controls_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    content_form_filters = filters('content_form')
    title_filter = 'item_title.default...None.None' in content_form_filters
    assert title_filter, \
        'Is _title_ `<input>` `value` attribute set to `item_title`? Have you added the `default(\'\')` filter?'

    slug_filter = 'slug.default...None.None' in content_form_filters
    assert slug_filter, \
        'Is _slug_ `<input>` `value` attribute set to `item_title`? Have you added the `default(\'\')` filter?'

    body_filter = 'body.default...None.None' in content_form_filters
    assert body_filter, \
        'Is _body_ `<textarea>` text content set to `body`? Have you added the `default(\'\')` filter?'

    assert content_exists, \
        'Is the `content.html` file in the `admin/templates` folder?'

    content_url_for = 'url_for.admin.edit.id.item.id.None.None' in get_calls('content')
    assert content_url_for, \
        'Do you have an `href` with a call to `url_for` pointing to `admin.edit` passing in `id=item.id`?'

@pytest.mark.test_edit_route_form_data_module3
def test_edit_route_form_data_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    post_check = str(get_request_method('edit', False)).find('POST')
    assert post_check, \
        'Are you testing if the request method is `POST`?'
    try:
        get_form_data('edit', 'content.title')
        get_form_data('edit', 'content.slug')
        get_form_data('edit', 'content.type_id')
        get_form_data('edit', 'content.body')
    except:
        assert False, 'Are you setting all proprties of the `content` object correctly?'

    import_datetime = admin_module_code().find('name', lambda node: \
        node.value == 'datetime' and \
        node.parent.type == 'from_import' and \
        node.parent.targets[0].value == 'datetime') is not None
    assert import_datetime, \
        'Are you importing `datetime` from `datetime`?'

    content_updated_at = get_request_method('edit').find('assign', lambda node: \
        str(node.target) == 'content.updated_at')
    content_updated_at_exists = content_updated_at is not None
    assert content_updated_at_exists, \
        'Do you have a variable named `content_updated_at`?'
    right = content_updated_at.find('atomtrailers', lambda node: \
        node.value[0].value == 'datetime' and \
        node.value[1].value == 'utcnow' and \
        len(node.value) == 3 and \
        node.value[2].type == 'call') is not None
    assert right, \
        'Are you setting `content.updated_at` to the current date?'

    error = get_request_method('edit').find('assign', lambda node: \
        node.target.value == 'error')
    error_exists = error is not None
    assert error_exists, \
        'Do you have a variable named `error`?'
    error_none = error.value.to_python() is None
    assert error_none, \
        'Are you setting the `error` variable correctly?'

@pytest.mark.test_edit_route_validate_data_module3
def test_edit_route_validate_data_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    title_error = get_conditional(get_request_method('edit'), ['not:request.form["title"]', 'request.form["title"]:is:None', 'request.form["title"]:==:None', 'request.form["title"]:is:""', 'request.form["title"]:==:""'], 'if', True)
    title_if_exists = title_error is not None
    assert title_if_exists, \
        'Do you have a nested `if` statement that tests if `title` is `not` empty.'

    title_error_message = title_error.parent.find('assign', lambda node: node.target.value == 'error')
    title_error_message_exists = title_error_message is not None and title_error_message.value.type == 'string'
    assert title_error_message_exists, \
        'Are you setting the `error` variable to the appropriate `string` in the `if` statement.'

@pytest.mark.test_edit_route_update_data_module3
def test_edit_route_update_data_module3():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    error_check = get_request_method('edit').find('comparison', lambda node: \
        'error' in [str(node.first), str(node.second)])
    error_check_exists = error_check is not None and error_check.parent.type == 'if' and \
        ((error_check.first.value == 'error' and error_check.second.value == 'None') or \
        (error_check.first.value == 'None' and error_check.second.value == 'error')) and \
        (error_check.value.first == '==' or error_check.value.first == 'is')
    assert error_check_exists, \
        'Do you have an if statment that is checking if `error` is `None`?'

    error_check_if = error_check.parent
    commit_call = error_check_if.find('atomtrailers', lambda node: \
        node.value[0].value == 'db' and \
        node.value[1].value == 'session' and \
        node.value[2].value == 'commit' and \
        node.value[3].type == 'call'
        ) is not None
    assert commit_call, \
        'Are you calling the `db.session.commit()` function?'

    return_redirect = error_check_if.find('return', lambda node: \
        node.value[0].value == 'redirect' and \
        node.value[1].type == 'call')
    return_redirect_exists = return_redirect is not None
    assert return_redirect_exists, \
        'Are you returning a call to the `redirect()` function?'

    url_for_call = return_redirect.find_all('atomtrailers', lambda node: \
        node.value[0].value == 'url_for' and \
        node.value[1].type == 'call')
    url_for_call_exists = url_for_call is not None
    assert url_for_call_exists, \
        'Are you passing a call to the `url_for()` function to the `redirect()` function?'

    url_for_args = list(url_for_call.find_all('call_argument').map(lambda node: \
        str(node.target) + ':' + str(node.value).replace("'", '"')))
    url_content = 'None:"admin.content"' in url_for_args
    assert url_content, \
        "Are you passing the `'admin.content'` to the `url_for()` function?"

    url_type = 'type:type.name' in url_for_args
    assert url_type, \
        'Are you passing a `type` keyword argument set to `type.name` to the `url_for()` function?'

    flash_exists = get_request_method('edit').find('atomtrailers', lambda node: \
        node.value[0].value == 'flash' and \
        node.value[1].type == 'call' and \
        node.value[1].value[0].value.value == 'error') is not None
    assert flash_exists, \
        'Are you flashing an `error` at the end of the `request.method` `if`?'
#!
