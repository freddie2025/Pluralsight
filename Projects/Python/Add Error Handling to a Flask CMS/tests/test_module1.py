## Imports
import pytest
from tests.utils import *
#!

## Tests
@pytest.mark.test_disable_werkzeug_logging_module1
def test_disable_werkzeug_logging_module1():
    # 01. Disable Werkzeug Logging
    # from logging import getLogger
    # request_log = getLogger('werkzeug')
    # request_log.disabled = True
    logging_import = get_imports(handlers_code(), 'logging')
    logging_import_exits = logging_import is not None
    assert logging_import_exits, \
        'Do you have a `logging` import statement?'
    get_logger_exists = 'getLogger' in logging_import
    assert get_logger_exists, \
        'Are you importing `getLogger` from `logging` in `cms/handlers.py`?'

    request_log = handlers_code().find('assign', lambda node: node.target.value == 'request_log')
    request_log_exists = request_log is not None
    assert request_log_exists, \
        'Are you setting the `request_log` variable correctly?'
    get_call = request_log.find('atomtrailers', lambda node: \
        node.value[0].value == 'getLogger' and \
        node.value[1].type == 'call'
        )
    get_call_exists = get_call is not None
    assert get_call_exists, \
        'Are you calling the `getLogger()` function and assigning the result to `request_log`?'
    get_argument = get_call.find('call_argument', lambda node: \
        str(node.value.value).replace("'", '"') == '"werkzeug"'  
    ) is not None
    assert get_argument, \
        'Are you passing the `getLogger()` function the correct argument?'

    request_log_disabled = handlers_code().find('assign', lambda node: \
        node.target.find('atomtrailers', lambda node: \
            node.value[0].value == 'request_log' and \
            node.value[1].value == 'disabled') and \
        node.value.value == 'True') is not None

    assert request_log_disabled, \
        'Have you set the `disabled` property on `request_log` to `True`?'

@pytest.mark.test_configure_logging_module1
def test_configure_logging_module1():
    # 02. Configure Logging
    # def configure_logging(name, level):
    #     log = getLogger(name)
    #     log.setLevel(level)
    def_configure_logging = handlers_code().find('def', lambda node: \
        node.name == 'configure_logging' and \
        node.arguments[0].target.value == 'name' and \
        node.arguments[1].target.value == 'level')
    def_configure_logging_exists = def_configure_logging is not None
    assert def_configure_logging_exists, \
        'Have you created a function at the top of `handlers.py` called `configure_logging`? Do you have the correct parameters?'

    log = def_configure_logging.find('assign', lambda node: node.target.value == 'log')
    log_exists = log is not None
    assert log_exists, \
        'Are you setting the `log` variable correctly?'
    get_call = log.find('atomtrailers', lambda node: \
        node.value[0].value == 'getLogger' and \
        node.value[1].type == 'call'
        )
    get_call_exists = get_call is not None
    assert get_call_exists, \
        'Are you calling the `getLogger()` function and assigning the result to `log`?'
    get_argument = get_call.find('call_argument', lambda node: \
        str(node.value.value) == 'name') is not None
    assert get_argument, \
        'Are you passing the `getLogger()` function the correct argument?'

    level_call = def_configure_logging.find('atomtrailers', lambda node: \
        node.value[0].value == 'log' and \
        node.value[1].value == 'setLevel' and \
        node.value[2].type == 'call'
        )
    level_call_exists = level_call is not None
    assert level_call_exists, \
        'Are you calling the `log.setLevel()` function?'
    level_argument = level_call.find('call_argument', lambda node: \
        str(node.value.value) == 'level') is not None
    assert level_argument, \
        'Are you passing the `log.setLevel()` function the correct argument?'

@pytest.mark.test_rotating_file_handler_module1
def test_rotating_file_handler_module1():
    # 03. Rotate File Handler
    # from logging.handlers import RotatingFileHandler
    # handler = RotatingFileHandler('logs/{}.log'.format(name), maxBytes=1024*1024, backupCount=10)
    def_configure_logging = handlers_code().find('def', lambda node: \
        node.name == 'configure_logging' and \
        node.arguments[0].target.value == 'name' and \
        node.arguments[1].target.value == 'level')
    def_configure_logging_exists = def_configure_logging is not None
    assert def_configure_logging_exists, \
        'Have you created a function at the top of `handlers.py` called `configure_logging`? Do you have the correct parameters?'

    handler_import = get_imports(handlers_code(), 'logging.handlers')
    handler_import_exits = handler_import is not None
    assert handler_import_exits, \
        'Do you have a `logging.handlers` import statement?'
    rotating_file_exists = 'RotatingFileHandler' in handler_import
    assert rotating_file_exists, \
        'Are you importing `RotatingFileHandler` from `logging.handlers` in `cms/handlers.py`?'

    handler = def_configure_logging.find('assign', lambda node: node.target.value == 'handler')
    handler_exists = handler is not None
    assert handler_exists, \
        'Are you setting the `handler` variable correctly?'
    rotating_file_call = handler.find('atomtrailers', lambda node: \
        node.value[0].value == 'RotatingFileHandler' and \
        node.value[1].type == 'call'
        )
    rotating_file_call_exists = rotating_file_call is not None
    assert rotating_file_call_exists, \
        'Are you creating a RotatingFileHandler instance and assigning it to `handler`?'

    rotating_file_args = get_args(rotating_file_call[1])

    first_arg = len(rotating_file_args) >= 1 and rotating_file_args[0] == '"logs/{}.log".format(name)'
    assert first_arg, \
        'Are you passing the correct path to the `RotatingFileHandler`?'

    max_bytes_exists = 'maxBytes:1024*1024' in rotating_file_args
    assert max_bytes_exists, \
        'Are you passing a `maxBytes` keyword argument set to `1024*1024`?'

    backup_count_exists = 'backupCount:10' in rotating_file_args
    assert backup_count_exists, \
        'Are you passing a `backupCount` keyword argument set to `10`?'

    arg_count = len(rotating_file_args) == 3
    assert arg_count, \
        'Are you passing the correct number of keyword arguments to `RotatingFileHandler()`?'

@pytest.mark.test_add_handler_module1
def test_add_handler_module1():
    # 04. Add Log Handler
    # log.addHandler(handler)
    # return log
    def_configure_logging = handlers_code().find('def', lambda node: \
        node.name == 'configure_logging' and \
        node.arguments[0].target.value == 'name' and \
        node.arguments[1].target.value == 'level')
    def_configure_logging_exists = def_configure_logging is not None
    assert def_configure_logging_exists, \
        'Have you created a function at the top of `handlers.py` called `configure_logging`? Do you have the correct parameters?'

    add_handler_call = def_configure_logging.find('atomtrailers', lambda node: \
        node.value[0].value == 'log' and \
        node.value[1].value == 'addHandler' and \
        node.value[2].type == 'call'
        )
    add_handler_call_exists = add_handler_call is not None
    assert add_handler_call_exists, \
        'Are you calling the `log.addHandler()` function?'
    add_handler_argument = add_handler_call.find('call_argument', lambda node: \
        str(node.value.value) == 'handler') is not None
    assert add_handler_argument, \
        'Are you passing the `log.addHandler()` function the correct argument?'

    return_log = def_configure_logging.find('return')
    return_log_exists = return_log is not None and return_log.value.value == 'log'
    assert return_log_exists, \
        'Are you returning `log` from the `configure_logging` function?'

@pytest.mark.test_timestamp_module1
def test_timestamp_module1():
    # 05. Timestamp Formatting
    # from time import strftime
    # timestamp = strftime('[%d/%b/%Y %H:%M:%S]')
    time_import = get_imports(handlers_code(), 'time')
    time_import_exits = time_import is not None
    assert time_import_exits, \
        'Do you have a `time` import statement?'
    strftime_exists = 'strftime' in time_import
    assert strftime_exists, \
        'Are you importing `strftime` from `time` in `cms/handlers.py`?'

    timestamp = handlers_code().find('assign', lambda node: node.target.value == 'timestamp')
    timestamp_exists = timestamp is not None
    assert timestamp_exists, \
        'Are you setting the `timestamp` variable correctly?'
    strftime_call = timestamp.find('atomtrailers', lambda node: \
        node.value[0].value == 'strftime' and \
        node.value[1].type == 'call'
        )
    strftime_call_exists = strftime_call is not None
    assert strftime_call_exists, \
        'Are you calling the `strftime()` function and assigning the result to `timestamp`?'
    strftime_argument = strftime_call.find('call_argument', lambda node: \
        str(node.value.value).replace("'", '"') == '"[%d/%b/%Y %H:%M:%S]"') is not None
    assert strftime_argument, \
        'Are you passing `strftime()` the correct format string?'

@pytest.mark.test_access_log_module1
def test_access_log_module1():
    # 06. Access Log
    # from logging import INFO, WARN, ERROR
    # access_log = configure_logging('access', INFO)
    logging_import = get_imports(handlers_code(), 'logging')
    logging_import_exists = logging_import is not None
    assert logging_import_exists, \
        'Do you have an import from `logging` statement?'

    logging_import_info = 'INFO' in logging_import
    assert logging_import_info, \
        'Are you importing `INFO` from `logging` in `cms/handlers.py`?'

    logging_import_warn = 'WARN' in logging_import
    assert logging_import_warn, \
        'Are you importing `WARN` from `logging` in `cms/handlers.py`?'

    logging_import_error = 'ERROR' in logging_import
    assert logging_import_error, \
        'Are you importing `ERROR` from `logging` in `cms/handlers.py`?'

    access_log = handlers_code().find('assign', lambda node: node.target.value == 'access_log')
    access_log_exists = access_log is not None
    assert access_log_exists, \
        'Are you setting the `access_log` variable correctly?'

    configure_logging_call = access_log.find('atomtrailers', lambda node: \
        node.value[0].value == 'configure_logging' and \
        node.value[1].type == 'call'
        )
    configure_logging_call_exists = configure_logging_call is not None
    assert configure_logging_call_exists, \
        'Are you calling the `configure_logging()` function and assigning the result to `access_log`?'

    configure_logging_args = get_args(configure_logging_call[1])

    first_arg = len(configure_logging_args) >= 1 and configure_logging_args[0] == '"access"'
    assert first_arg, \
        'Are you passing the correct name to `configure_logging()`?'

    second_arg = configure_logging_args[1] == 'INFO'
    assert second_arg, \
        'Are you passing the correct level to `configure_logging()`?'

    arg_count = len(configure_logging_args) == 2
    assert arg_count, \
        'Are you passing the correct number of arguments to `configure_logging()`?'

@pytest.mark.test_after_request_module1
def test_after_request_module1():
    # 07. After Request
    # @app.after_request
    # def after_request(response):
    #     return response
    after_request = handlers_code().find('def', lambda node: \
        node.name == 'after_request' and \
        node.arguments[0].target.value == 'response')
    after_request_exists = after_request is not None
    assert after_request_exists, \
        'Have you created a function called `after_request` with a parameter of `response`?'

    decorator_exists = after_request.find('decorator', lambda node: node.find('dotted_name', lambda node: \
          node.value[0].value == 'app' and \
          node.value[1].type == 'dot' and \
          node.value[2].value == 'after_request')) is not None
    assert decorator_exists, \
        'The `after_request` function should have a decorator of `@app.after_request`.'

    return_after_request = after_request.find('return')
    return_after_request_exists = return_after_request is not None and return_after_request.value.value == 'response'
    assert return_after_request_exists, \
        'Are you returning `response` from the `after_request` function?'

@pytest.mark.test_access_log_format_module1
def test_access_log_format_module1():
    # 08. Access Log Format
    # access_log.info('%s - - %s "%s %s %s" %s -', request.remote_addr, timestamp, request.method, request.path, request.scheme.upper(), response.status_code)
    after_request = handlers_code().find('def', lambda node: \
        node.name == 'after_request' and \
        node.arguments[0].target.value == 'response')
    after_request_exists = after_request is not None
    assert after_request_exists, \
        'Have you created a function called `after_request` with a parameter of `response`?'

    info_call = after_request.find('atomtrailers', lambda node: \
        node.value[0].value == 'access_log' and \
        node.value[1].value == 'info' and \
        node.value[2].type == 'call')
    info_call_exists = info_call is not None
    assert info_call_exists, \
        'Are you calling the `access_log.info()` function?'

    info_args = get_args(info_call[-1], False)

    first_arg = len(info_args) >= 1 and info_args[0] == '\'%s - - %s "%s %s %s" %s -\'' 
    assert first_arg, \
        'Are you passing the correct log format to `access_log.info()` as the first argument?'

    second_arg = len(info_args) >= 2 and info_args[1] == 'request.remote_addr' 
    assert second_arg, \
        'Are you passing the `request.remote_addr` to `access_log.info()` as the second argument?'

    third_arg = len(info_args) >= 3 and info_args[2] == 'timestamp' 
    assert third_arg, \
        'Are you passing `timestamp` to `access_log.info()` as the third argument?'

    fourth_arg = len(info_args) >= 4 and info_args[3] == 'request.method' 
    assert fourth_arg, \
        'Are you passing `request.method` to `access_log.info()` as the fourth argument?'

    fifth_arg = len(info_args) >= 5 and info_args[4] == 'request.path' 
    assert fifth_arg, \
        'Are you passing `request.path` to `access_log.info()` as the fifth argument?'

    sixth_arg = len(info_args) >= 6 and info_args[5] == 'request.scheme.upper()' 
    assert sixth_arg, \
        'Are you passing `request.scheme.upper()` to `access_log.info()` as the sixth argument?'

    seventh_arg = len(info_args) == 7 and info_args[6] == 'response.status_code' 
    assert seventh_arg, \
        'Are you passing `response.status_code` to `access_log.info()` as the seventh argument?'

    arg_count = len(info_args) == 7
    assert arg_count, \
        'Are you passing the correct number of arguments to `access_log.info()`?'

@pytest.mark.test_valid_status_codes_module1
def test_valid_status_codes_module1():
    # 09. Valid Status Codes
    # if int(response.status_code) < 400:
    after_request = handlers_code().find('def', lambda node: \
        node.name == 'after_request' and \
        node.arguments[0].target.value == 'response')
    after_request_exists = after_request is not None
    assert after_request_exists, \
        'Have you created a function called `after_request` with a parameter of `response`?'

    response_code = get_conditional(after_request, ['int(response.status_code):<:400', '400:>:int(response.status_code)'], 'if')
    response_code_exists = response_code is not None
    assert response_code_exists, \
        'Do you have an `if` statement that tests if `int(response.status_code)` is less than `400`?'

    info_call = response_code.parent.find('atomtrailers', lambda node: \
        node.value[0].value == 'access_log' and \
        node.value[1].value == 'info' and \
        node.value[2].type == 'call')
    info_placement = info_call is not None and \
        (len(response_code.indentation) * 2) == len(info_call.indentation)
    assert info_placement, \
        'Have you placed `access_log.info()` in the `if` statement?'
#!