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
@pytest.mark.test_template_add_from_controls_module2
def test_template_add_from_controls_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    assert admin_exists and admin_templates_exists, \
        'Have you created a `templates` folder in the `admin` blueprint folder?'
    assert content_form_exists, \
        'Is the `content_form.html` file in the `admin/templates` folder?'

    title_exists = len(content_form_template.select('input[name="title"][class="input"][type="text"]')) == 1
    assert title_exists, \
        'Have you added an `<input>` with the correct attributes to the `title` control `<div>`?'

    slug_exists = len(content_form_template.select('input[name="slug"][class="input"][type="text"]')) == 1
    assert slug_exists, \
        'Have you added an `<input>` with the correct attributes to the `slug` control `<div>`?'

    body_exists = len(content_form_template.select('textarea[name="body"][class="textarea"]')) == 1
    assert body_exists, \
        'Have you added an `<textarea>` with the correct attributes to the `content` control `<div>`?'

@pytest.mark.test_template_type_dropdown_module2
def test_template_type_dropdown_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    assert admin_exists and admin_templates_exists, \
        'Have you created a `templates` folder in the `admin` blueprint folder?'
    assert content_form_exists, \
        'Is the `content_form.html` file in the `admin/templates` folder?'

    select_exists = len(content_form_template.select('select[name="type_id"]')) == 1
    assert select_exists, \
        'Have you added a `<select>` with the correct attributes to the `type` control `<div>`?'

    select_template_code = select_code('content_form', '<select', '</select>')
    for_loop = len(select_template_code) > 0 and is_for(select_template_code[0])
    assert for_loop, \
        'Do you have a `for` loop in your `<select>` element?'

    cycle_types = select_template_code[0].target.name == 'type' and select_template_code[0].iter.name == 'types'
    assert cycle_types, \
        'Is the for loop cycling through `types`?'

    option_el = select_code(select_template_code[0], '<option', '</option>')
    len_option = len(option_el) > 0
    assert len_option, \
        'Have you added an `<option>` element inside the `for` loop?'

    type_id = 'type.id' in get_variables(option_el)
    assert type_id, \
        'Is the `value` attribute set to `type.id`?'

    selected = simplify(if_statements('content_form')) == 'type.name.eq.type_name.selected.None'
    assert selected, \
        'Do you have an `if` statement in the `<option>` to test whether `type.name` is equal to `type_name`?'

    type_name_exists = simplify(select_code(select_template_code[0], '>', '</option>')[0]) == 'type.name'
    assert type_name_exists, \
        'Are you adding `type.name` as the option name?'

@pytest.mark.test_template_buttons_module2
def test_template_buttons_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    assert admin_exists and admin_templates_exists, \
        'Have you created a `templates` folder in the `admin` blueprint folder?'
    assert content_form_exists, \
        'Is the `content_form.html` file in the `admin/templates` folder?'

    submit_exists = len(content_form_template.select('input[type="submit"][value="Submit"].button.is-link')) == 1
    assert submit_exists, \
        'Have you added an `<input>` with the correct attributes to the first `is-grouped` control `<div>`?'

    cancel_el = content_form_template.select('a.button.is-text')
    cancel_exists = len(cancel_el) == 1
    assert cancel_exists, \
        'Have you added an `<a>` with the correct attributes to the second `is-grouped` control `<div>`?'
    
    a_contents_len = len(cancel_el[0].contents) >= 1 
    assert a_contents_len, \
            'Does your cancel link contain the word `Cancel`?'

    a_contents = (cancel_el[0].contents[0]).lower() == 'cancel'
    assert a_contents, \
        'Does your cancel link contain the word `Cancel`?'

    links = 'admin.content:type:type_name' in template_functions('content_form', 'url_for')
    assert links, \
        'Do you have an `href` with a call to `url_for` pointing to `admin.content` passing in `type=type_name`?'

@pytest.mark.test_create_route_methods_module2
def test_create_route_methods_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    flask_import = get_imports(admin_module_code(), 'flask')
    flask_import_exits = flask_import is not None
    assert flask_import_exits, \
        'Do you have an import from `flask` statement?'
    request_import = 'request' in flask_import
    assert request_import, \
        'Are you importing `request` from `flask` in `cms/admin/__init__.py`?'

    strings = list(get_methods_keyword('create').find_all('string').map(lambda node: node.value.replace("'", '"')))
    methods_exist = '"GET"' in strings and '"POST"' in strings
    assert methods_exist, \
        'Have you added the `methods` keyword argument to the `create` route allowing `POST` and `GET`?'
    post_check = str(get_request_method('create', False)).find('POST')
    assert post_check, 'Are you testing if the request method is `POST`?'
    get_form_data('create', 'title')

@pytest.mark.test_create_route_form_data_module2
def test_create_route_form_data_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    get_form_data('create', 'slug')
    get_form_data('create', 'type_id')
    get_form_data('create', 'body')

    error = get_request_method('create').find('assign', lambda node: \
        node.target.value == 'error')
    error_exists = error is not None
    assert error_exists, \
        'Do you have a variable named `error`?'
    error_none = error.value.to_python() is None
    assert error_none, \
        'Are you setting the `error` variable correctly?'

@pytest.mark.test_create_route_validate_data_module2
def test_create_route_validate_data_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    title_error = get_conditional(get_request_method('create'), ['not:request.form["title"]', 'request.form["title"]:is:None', 'request.form["title"]:==:None', 'request.form["title"]:is:""', 'request.form["title"]:==:""', 'not:title', 'title:is:None', 'title:==:None', 'title:is:""', 'title:==:""'], 'if', True)

    title_if_exists = title_error is not None
    assert title_if_exists, \
        'Do you have a nested `if` statement that tests if `title` is `not` empty?'
    title_error_message = title_error.parent.find('assign', lambda node: node.target.value == 'error')
    title_error_message_exists = title_error_message is not None and title_error_message.value.type == 'string'
    assert title_error_message_exists, \
        'Are you setting the `error` variable to the appropriate `string` in the `if` statement?'

    type_id_error = get_conditional(get_request_method('create'), ['not:request.form["type_id"]', 'request.form["type_id"]:is:None', 'request.form["type_id"]:==:None', 'request.form["type_id"]:is:""', 'request.form["type_id"]:==:""', 'not:type_id', 'type_id:is:None', 'type_id:==:None', 'type_id:is:""', 'type_id:==:""'], 'elif', True)

    type_id_elif_exists = type_id_error is not None
    assert type_id_elif_exists, \
        'Do you have an `elif` statement that tests if `type` is `not` empty?'
    type_id_error_message = type_id_error.parent.find('assign', lambda node: node.target.value == 'error')
    type_id_error_message_exists = type_id_error_message is not None and type_id_error_message.value.type == 'string'
    assert type_id_error_message_exists, \
        'Are you setting the `error` variable to the appropriate `string` in the `elif` statement?'

@pytest.mark.test_create_route_insert_data_module2
def test_create_route_insert_data_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'
    error_check = get_request_method('create').find('comparison', lambda node: \
        'error' in [str(node.first), str(node.second)])
    error_check_exists = error_check is not None and error_check.parent.type == 'if' and \
        ((error_check.first.value == 'error' and error_check.second.value == 'None') or \
        (error_check.first.value == 'None' and error_check.second.value == 'error')) and \
        (error_check.value.first == '==' or error_check.value.first == 'is')
    assert error_check_exists, \
        'Do you have an `if` statment that is checking if `error` is `None`?'

    error_check_if = error_check.parent
    content = error_check_if.find('assign', lambda node: \
        node.target.value == 'content')
    content_exists = content is not None
    assert content_exists, \
        'Are you setting the `content` variable correctly?'
    content_instance = content.find('atomtrailers', lambda node: \
        node.value[0].value == 'Content')
    content_instance_exists = content_instance is not None
    assert content_instance_exists, \
        'Are you setting the `content` variable to an instance of `Content`?'
    content_args = list(content_instance.find_all('call_argument').map(lambda node: \
        node.target.value + ':' + node.value.value))

    title_exists = 'title:title' in content_args
    assert title_exists, \
        'Are you passing a `title` keyword argument set to `title` to the `Content` instance?'

    slug_exists = 'slug:slug' in content_args
    assert slug_exists, \
        'Are you passing a `slug` keyword argument set to `slug` to the `Content` instance?'

    type_id_exists = 'type_id:type_id' in content_args
    assert type_id_exists, \
        'Are you passing a `type_id` keyword argument set to `type_id` to the `Content` instance?'

    body_exists = 'body:body' in content_args
    assert body_exists, \
        'Are you passing a `body` keyword argument set to `body` to the `Content` instance?'

    content_count = len(content_args) == 4
    assert content_count, \
        'Are you passing the correct number of keyword arguments to the `Content` instance?'

    module_import = get_imports(admin_module_code(), 'cms.admin.models') or get_imports(admin_module_code(), '.models')
    module_import_exists = module_import is not None
    assert module_import_exists, \
        'Are you importing the correct methods and classes from `cms.admin.models` in `cms/admin/__init__.py`?'

    name_as_name_db = 'db' in module_import
    assert name_as_name_db, \
        'Are you importing the `db` SQLAlchemy instance from `cms.admin.models` in `admin/cms/__init__.py`?'

    add_call = error_check_if.find('atomtrailers', lambda node: \
        node.value[0].value == 'db' and \
        node.value[1].value == 'session' and \
        node.value[2].value == 'add' and \
        node.value[3].type == 'call' and \
        node.value[3].value[0].value.value == 'content'
        ) is not None
    assert add_call, \
        'Are you calling the `db.session.add()` function and passing in the correct argument?'

    commit_call = error_check_if.find('atomtrailers', lambda node: \
        node.value[0].value == 'db' and \
        node.value[1].value == 'session' and \
        node.value[2].value == 'commit' and \
        node.value[3].type == 'call'
        ) is not None
    assert commit_call, \
        'Are you calling the `db.session.commit()` function?'

@pytest.mark.test_create_route_redirect_module2
def test_create_route_redirect_module2():
    assert admin_module_exists, \
        'Have you created the `cms/admin/__init__.py` file?'

    flask_import = get_imports(admin_module_code(), 'flask')
    flask_import_exits = flask_import is not None
    assert flask_import_exits, \
        'Do you have an import from `flask` statement?'

    redirect_import = 'redirect' in flask_import
    assert redirect_import, \
        'Are you importing `redirect` from `flask` in `cms/admin/__init__.py`?'
    url_for_import = 'url_for' in flask_import
    assert url_for_import, \
        'Are you importing `url_for` from `flask` in `cms/admin/__init__.py`?'

    error_check = get_request_method('create').find('comparison', lambda node: \
        'error' in [str(node.first), str(node.second)])
    error_check_exists = error_check is not None and error_check.parent.type == 'if' and \
        ((error_check.first.value == 'error' and error_check.second.value == 'None') or \
        (error_check.first.value == 'None' and error_check.second.value == 'error')) and \
        (error_check.value.first == '==' or error_check.value.first == 'is')
    assert error_check_exists, \
        'Do you have an `if` statment that is checking if `error` is `None`?'
    error_check_if = error_check.parent

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

    url_for_args = list(url_for_call.find_all('call_argument').map(lambda node: str(node.target) + ':' + str(node.value.value).replace("'", '"')))
    url_content = 'None:"admin.content"' in url_for_args
    assert url_content, \
        "Are you passing the `'admin.content'` route to the `url_for()` function?"

    url_type = 'type:type' in url_for_args
    assert url_type, \
        'Are you passing a `type` keyword argument set to `type` to the `url_for()` function?'

    flash_exists = get_request_method('create').find('atomtrailers', lambda node: \
        node.value[0].value == 'flash' and \
        node.value[1].type == 'call' and \
        node.value[1].value[0].value.value == 'error') is not None
    assert flash_exists, \
        'Are you flashing an `error` at the end of the `request.method` `if`?'
#!
