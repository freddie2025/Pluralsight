import pytest
import sys

from jobs import app
from .utils import *

@pytest.mark.test_app_job_template_module5
def test_app_job_template_module5():
    assert template_exists('job'), 'The `job.html` template does not exist in the `templates` folder.'
    assert 'layout.html' in template_extends('job'), 'The `job.html` template does not extend `layout.html`.'
    assert 'content' in template_block('job'), 'Have you added a template `block` called `content`?'
    assert 'show_job:job' in template_functions('job', 'show_job'), 'Have you call the `show_job` macro in the `job.html` file?'

@pytest.mark.test_app_job_route_module5
def test_app_job_route_module5():
    assert 'job' in dir(app), 'Have you created the `job` function?'
    result = [item for item in get_functions(app.job) if item.startswith('render_template:job.html')]
    assert len(result) == 1, 'Have you called the `render_template` function.'
    return_values = get_functions_returns(app.job)[0]
    assert return_values['value/args/s'] == 'job.html' and return_values['value/func/id'] == 'render_template', 'Did you return the `render_template` call?'

@pytest.mark.test_app_job_route_decorator_module5
def test_app_job_route_decorator_module5():
    assert 'job' in dir(app), 'Have you created the `job` function?'
    assert 'route:/job/<job_id>' in get_functions(app.job), 'Have you added a `job_id` parameter to the job function'

@pytest.mark.test_app_job_route_parameter_module5
def test_app_job_route_parameter_module5():
    assert 'job' in dir(app), 'Have you created the `job` function?'
    assert 'job:job_id:job:id' in template_functions('_macros', 'url_for'), 'Looks like the job title link `href` is incorrect.'
    assert 'job_id' in inspect.getfullargspec(app.job).args, 'Have you added the correct parameters to the `job` function parameters list?'

@pytest.mark.test_app_job_route_data_module5
def test_app_job_route_data_module5():
    assert 'job' in dir(app), 'Have you created the `job` function?'
    execute_sql = 'execute_sql:SELECT job.id, job.title, job.description, job.salary, employer.id as employer_id, employer.name as employer_name FROM job JOIN employer ON employer.id = job.employer_id WHERE job.id = ?:job_id:single:True'
    assert execute_sql in get_functions(app.job), '`execute_sql` has not been called or has the wrong parameters.'

@pytest.mark.test_app_job_route_pass_data_module5
def test_app_job_route_pass_data_module5():
    assert 'job' in dir(app), 'Have you created the `job` function?'
    assert 'render_template:job.html:job:job' in get_functions(app.job), 'Have you added `job` to the `render_template` call.'
