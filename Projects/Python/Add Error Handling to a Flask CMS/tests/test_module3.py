## Imports
import pytest
from tests.utils import *
#!

## Tests
@pytest.mark.test_namespace_module3
def test_namespace_module3():
    # 01. Signals
    # from blinker import Namespace
    # _signals = Namespace()
    blinker_import = get_imports(auth_code(), 'blinker')
    blinker_import_exits = blinker_import is not None
    assert blinker_import_exits, \
        'Do you have a `blinker` import statement?'
    namespace_exists = 'Namespace' in blinker_import
    assert namespace_exists, \
        'Are you importing `Namespace` from `blinker` in `cms/admin/auth.py`?'

    namespace_call = auth_code().find('assign', lambda node:
        node.find('name', lambda node:
            node.value == 'Namespace' and \
            node.next.type == 'call'))
    namespace_call_exists = namespace_call is not None
    assert namespace_call_exists, \
        'Are you creating an instance of `Namespace()` in `cms/admin/auth.py`?'

@pytest.mark.test_unauthorized_signal_module3
def test_unauthorized_signal_module3():
    # 02. Unauthorized Signal
    # unauthorized = _signals.signal('unauthorized')
    namespace_call = auth_code().find('assign', lambda node:
        node.find('name', lambda node:
            node.value == 'Namespace' and \
            node.next.type == 'call'))
    namespace_call_exists = namespace_call is not None
    assert namespace_call_exists, \
        'Are you creating an instance of `Namespace()`?'

    namespace_instance = namespace_call.target.value

    signal_call = auth_code().find('atomtrailers', lambda node: \
        node.parent.type == 'assignment' and \
        node.parent.target.value == 'unauthorized' and \
        node.value[0].value == namespace_instance and \
        node.value[1].value == 'signal' and \
        node.value[2].type == 'call')
    signal_call_exists = signal_call is not None
    assert signal_call_exists, \
        'Are you calling the `{}.signal()` function and assigning it to `unauthorized`?'.format(namespace_instance)

    signal_args = get_args(signal_call[-1])

    arg_count = len(signal_args) == 1
    assert arg_count, \
        'Are you passing the correct number of arguments to `{}.signal()`?'.format(namespace_instance)

    first_arg = signal_args[0] == '"unauthorized"'
    assert first_arg, \
        'Are you passing `"unauthorized"` to `{}.signal()` as the first argument?'.format(namespace_instance)

@pytest.mark.test_send_unauthorized_signal_module3
def test_send_unauthorized_signal_module3():
    # 03. Send Unauthorized Signal
    # from flask import current_app
    # unauthorized.send(current_app._get_current_object(), user_id=user.id, username=user.username)
    flask_import = get_imports(auth_code(), 'flask')
    flask_import_exits = flask_import is not None
    assert flask_import_exits, \
        'Do you have a `flask` import statement?'
    current_app_exists = 'current_app' in flask_import
    assert current_app_exists, \
        'Are you importing `current_app` from `flask` in `cms/admin/auth.py`?'

    send_call = auth_code().find('atomtrailers', lambda node: \
        node.value[0].value == 'unauthorized' and \
        node.value[1].value == 'send' and \
        node.value[2].type == 'call')
    send_call_exists = send_call is not None
    assert send_call_exists, \
        'Are you calling the `unauthorized.send()` function?'

    send_args = get_args(send_call[-1])

    first_arg = len(send_args) >= 1 and send_args[0] == 'current_app._get_current_object()'
    assert first_arg, \
        'Are you passing `current_app._get_current_object()` to `unauthorized.send()` as the first argument?'

    user_id_exists = 'user_id:user.id' in send_args
    assert user_id_exists, \
        'Are you passing a `user_id` keyword argument set to `user.id` to the `unauthorized.send()` function?'

    username_exists = 'username:user.username' in send_args
    assert username_exists, \
        'Are you passing a `username` keyword argument set to `user.username` to the `unauthorized.send()` function?'

    arg_count = len(send_args) == 3
    assert arg_count, \
        'Are you passing the correct number of arguments to the `unauthorized.send()` function?'

@pytest.mark.test_import_unauthorized_signal_module3
def test_import_unauthorized_signal_module3():
    # 04. Import Unauthorized Signal
    # from cms.admin.auth import unauthorized
    auth_import = get_imports(handlers_code(), 'cms.admin.auth')
    auth_import_exits = auth_import is not None
    assert auth_import_exits, \
        'Do you have a `cms.admin.auth` import statement?'
    unauthorized_exists = 'unauthorized' in auth_import
    assert unauthorized_exists, \
        'Are you importing `unauthorized` from `cms.admin.auth` in `cms/handlers.py`?'

@pytest.mark.test_unauthorized_log_module3
def test_unauthorized_log_module3():
    # 05. Unauthorized Log
    # unauthorized_log = configure_logging('unauthorized', WARN)
    unauthorized_log = handlers_code().find('assign', lambda node: node.target.value == 'unauthorized_log')
    unauthorized_log_exists = unauthorized_log is not None
    assert unauthorized_log_exists, \
        'Are you setting the `unauthorized_log` variable correctly?'

    configure_logging_call = unauthorized_log.find('atomtrailers', lambda node: \
        node.value[0].value == 'configure_logging' and \
        node.value[1].type == 'call'
        )
    configure_logging_call_exists = configure_logging_call is not None
    assert configure_logging_call_exists, \
        'Are you calling the `configure_logging()` function and assigning the result to `unauthorized_log`?'

    configure_logging_args = get_args(configure_logging_call[1])

    first_arg = len(configure_logging_args) >= 1 and configure_logging_args[0] == '"unauthorized"'
    assert first_arg, \
        'Are you passing the correct name to `configure_logging()`?'

    second_arg = len(configure_logging_args) == 2 and configure_logging_args[1] == 'WARN'
    assert second_arg, \
        'Are you passing the correct level to `configure_logging()`?'

    arg_count = len(configure_logging_args) == 2
    assert arg_count, \
        'Are you passing the correct number of arguments to `configure_logging()`?'

@pytest.mark.test_unauthorized_log_format_module3
def test_unauthorized_log_format_module3():
    # 06. Unauthorized Log Format
    # def log_unauthorized(app, user_id, username, **kwargs):
    #     unauthorized_log.warning('Unauthorized: %s %s %s', timestamp, user_id, username)
    def_log_unauthorized = handlers_code().find('def', lambda node: \
        node.name == 'log_unauthorized' and \
        node.arguments[0].target.value == 'app' and \
        node.arguments[1].target.value == 'user_id' and \
        node.arguments[2].target.value == 'username' and \
        node.arguments[3].type == 'dict_argument' and \
        node.arguments[3].value.value == 'kwargs')

    def_log_unauthorized_exists = def_log_unauthorized is not None
    assert def_log_unauthorized_exists, \
        'Have you created a function called `log_unauthorized`? Does it have the correct parameters?'

    warning_call = def_log_unauthorized.find('atomtrailers', lambda node: \
        node.value[0].value == 'unauthorized_log' and \
        node.value[1].value == 'warning' and \
        node.value[2].type == 'call')
    warning_call_exists = warning_call is not None
    assert warning_call_exists, \
        'Are you calling the `unauthorized_log.warning()` function?'

    warning_args = get_args(warning_call[-1], False)

    first_arg = len(warning_args) >= 1 and warning_args[0].replace("'", '"') == '"Unauthorized: %s %s %s"'
    assert first_arg, \
        'Are you passing the correct log format to `unauthorized_log.warning()` as the first argument?'

    second_arg = len(warning_args) >= 2 and warning_args[1] == 'timestamp'
    assert second_arg, \
        'Are you passing `timestamp` to `unauthorized_log.warning()` as the second argument?'

    third_arg = len(warning_args) >= 3 and warning_args[2] == 'user_id'
    assert third_arg, \
        'Are you passing `user_id` to `unauthorized_log.warning()` as the third argument?'

    fourth_arg = len(warning_args) >= 4 and warning_args[3] == 'username'
    assert fourth_arg, \
        'Are you passing `username` to `unauthorized_log.warning()` as the fourth argument?'

    arg_count = len(warning_args) == 4
    assert arg_count, \
        'Are you passing the correct number of arguments to `unauthorized_log.warning()`?'

@pytest.mark.test_connect_decorator_module3
def test_connect_decorator_module3():
    # 07. Connect Decorator
    # @unauthorized.connect
    def_log_unauthorized = handlers_code().find('def', lambda node: \
        node.name == 'log_unauthorized' and \
        node.arguments[0].target.value == 'app' and \
        node.arguments[1].target.value == 'user_id' and \
        node.arguments[2].target.value == 'username' and \
        node.arguments[3].type == 'dict_argument' and \
        node.arguments[3].value.value == 'kwargs')

    def_log_unauthorized_exists = def_log_unauthorized is not None
    assert def_log_unauthorized_exists, \
        'Have you created a function called `log_unauthorized`? Do you have the correct parameters?'

    decorator_exists = def_log_unauthorized.find('decorator', lambda node: node.find('dotted_name', lambda node: \
          node.value[0].value == 'unauthorized' and \
          node.value[1].type == 'dot' and \
          node.value[2].value == 'connect')) is not None
    assert decorator_exists, \
        'The `log_unauthorized` function should have a decorator of `@unauthorized.connect`.'
#!
