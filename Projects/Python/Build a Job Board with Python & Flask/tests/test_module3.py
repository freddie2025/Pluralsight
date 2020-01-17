import pytest
import inspect

from jobs import app
from .utils import *

@pytest.mark.test_app_import_sqlite_module3
def test_app_import_sqlite_module3():
    assert 'sqlite3' in dir(app), 'Have you imported `sqlite`?'

@pytest.mark.test_app_import_g_module3
def test_app_import_g_module3():
    assert 'g' in dir(app), 'Have you imported the `g` class from `flask`?'

@pytest.mark.test_app_db_path_module3
def test_app_db_path_module3():
    assert 'PATH' in dir(app), 'Have you created a constant called `PATH`.'
    assert app.PATH == 'db/jobs.sqlite', 'Have you created a constant called `PATH`?'

@pytest.mark.test_app_open_connection_get_attribute_module3
def test_app_open_connection_get_attribute_module3():
    assert 'open_connection' in dir(app), 'Have you defined a function named `open_connection`.'
    assert 'getattr:g:_connection:None' in get_functions(app.open_connection), 'Have you used the `getattr` function to get the global `_connection`?'

@pytest.mark.test_app_open_connection_connection_module3
def test_app_open_connection_connection_module3():
    assert 'g' in dir(app), 'Have you imported the `g` class from `flask`?'
    assert 'app' in dir(app), 'Have you created an instance of the `Flask` class called `app`?'
    assert 'open_connection' in dir(app), 'Have you defined a function named `open_connection`.'
    with app.app.app_context():
        app.open_connection()
        assert hasattr(app.g, '_connection'), 'Did you assign the `_connection` attribute to `g`?'
        _, _, db_name = app.g._connection.execute('PRAGMA database_list').fetchone()
        assert os.path.join(os.getcwd(), 'db', 'jobs.sqlite') == db_name, 'Did you pass the `connect` function the `PATH` constant?'

@pytest.mark.test_app_open_connection_row_factory_module3
def test_app_open_connection_row_factory_module3():
    assert 'g' in dir(app), 'Have you imported the `g` class from `flask`?'
    assert 'app' in dir(app), 'Have you created an instance of the `Flask` class called `app`?'
    assert 'open_connection' in dir(app), 'Have you defined a function named `open_connection`.'
    with app.app.app_context():
        db = app.open_connection()
        assert isinstance(db, app.sqlite3.Connection), 'Are you returning the database connection?'
        assert id(db.row_factory) == id(app.sqlite3.Row), 'Have you set the database `row_factory` to the sqlite3.Row class?'

@pytest.mark.test_app_execute_sql_module3
def test_app_execute_sql_module3():
    assert 'app' in dir(app), 'Have you created an instance of the `Flask` class called `app`?'
    assert 'execute_sql' in dir(app), 'Have you defined a function named `execute_sql`.'
    assert 'open_connection' in get_functions(app.execute_sql), 'Have you called the `open_connection` function in `execute_sql`?'

@pytest.mark.test_app_execute_sql_parameters_module3
def test_app_execute_sql_parameters_module3():
    assert 'execute_sql' in dir(app), 'Have you defined a function named `execute_sql`.'
    parameters = inspect.getfullargspec(app.execute_sql)
    assert len(parameters.args) == 4, 'Have you added parameters to the `execute_sql` function.'
    assert parameters.args[0] == 'sql' and parameters.args[1] == 'values' and parameters.args[2] == 'commit' and parameters.args[3] == 'single', 'Have you added the correct parameters to the `execute_sql` function parameters list?'
    assert parameters.defaults[0] == () and parameters.defaults[1] == False and parameters.defaults[2] == False, 'Do the `args` and `one` parameters have the correct defaults in the `execute_sql` function parameters list?'

@pytest.mark.test_app_execute_sql_execute_module3
def test_app_execute_sql_execute_module3():
    assert 'execute_sql' in dir(app), 'Have you defined a function named `execute_sql`.'
    assert 'execute:sql:values' in get_functions(app.execute_sql), 'Have you called the `execute` function in `execute_sql`?'

@pytest.mark.test_app_execute_sql_results_module3
def test_app_execute_sql_results_module3():
    assert 'execute_sql' in dir(app), 'Have you defined a function named `execute_sql`.'
    assert 'fetchall' in get_functions(app.execute_sql), 'Have you called the `fetchall` function in `execute_sql`?'
    assert 'fetchone' in get_functions(app.execute_sql), 'Have you called the `fetchone` function in `execute_sql`?'
    assert 'commit' in get_functions(app.execute_sql), 'Have you called the `close` function in `execute_sql`?'
    assert 'close' in get_functions(app.execute_sql), 'Have you called the `close` function in `execute_sql`?'
    assert len(get_statements(app.execute_sql)) >= 0, 'Have created an if statement in the `execute_sql` function?'
    assert 'results' == get_statements(app.execute_sql)[0]['body/targets/id'], 'Have you assigned the `results` variable to `connection.commit()`?'
    with app.app.app_context():
        results = app.execute_sql('SELECT * FROM job', single=True)
        assert type(results) != list, 'Have you create an if statement to only return one result in `one` is true?'

@pytest.mark.test_app_close_connection_module3
def test_app_close_connection_module3():
    assert 'close_connection' in dir(app), 'Have you defined a function named `close_connection`.'
    assert 'getattr:g:_connection:None' in get_functions(app.open_connection), 'Have you used the `getattr` function to get the global `_connection`?'
    assert 'close' in get_functions(app.execute_sql), 'Have you called the `close` function in `execute_sql`?'

@pytest.mark.test_app_close_connection_decorator_module3
def test_app_close_connection_decorator_module3():
    assert 'close_connection' in dir(app), 'Have you defined a function named `close_connection`.'
    decorators = get_decorators(app.close_connection)['close_connection']
    assert len(decorators) == 1, 'Have you added the correct decorator to `close_connection`.'
    decorator = decorators[0][0]
    assert decorator == 'teardown_appcontext', 'Does `close_connection` have a `teardown_appcontext` decorator?'