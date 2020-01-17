import pytest
import sys

from jobs import app
from .utils import *

@pytest.mark.test_template_macros_module4
def test_template_macros_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'

@pytest.mark.test_show_job_macro_definition_module4
def test_show_job_macro_definition_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'show_job:job' in template_macros('_macros'), 'Have you created the `show_job` macro and added the correct parameter?'

@pytest.mark.test_show_job_macro_html_module4
def test_show_job_macro_html_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    html = template_macro_soup('_macros', 'show_job')
    p = html.select('.card .card-header .card-header-title')
    div = html.select('.card-content .content')
    assert len(p) == 1 and len(div) == 1, 'Has the `HTML` from `templates.html` been copied to the `show_job` macro?'

@pytest.mark.test_show_job_macro_header_module4
def test_show_job_macro_header_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'job:title' in template_variables('_macros'), 'Looks like the job title link does not have content.'

@pytest.mark.test_show_job_macro_body_module4
def test_show_job_macro_body_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'job:employer_name' in template_variables('_macros'), 'Are you showing the employer name?'
    assert 'job:salary' in template_variables('_macros'), 'Are you showing the job salary?'
    assert 'job:description' in template_variables('_macros'), 'Are you showing the job description?'

@pytest.mark.test_show_jobs_macro_definition_module4
def test_show_jobs_macro_definition_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'show_jobs:jobs' in template_macros('_macros'), 'Have you created the `show_jobs` macro and added the correct parameter?'

@pytest.mark.test_show_jobs_macro_for_loop_module4
def test_show_jobs_macro_for_loop_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'show_jobs:jobs' in template_macros('_macros'), 'Have you created the `show_jobs` macro and added the correct parameter?'
    html = template_macro_soup('_macros', 'show_jobs')
    div = html.select('div.columns.is-multiline')
    assert len(div) == 1, 'Has a `<div>` with classes of `columns` and `is-multiline` been added to the `show_jobs` macro?'
    assert 'job:jobs' in show_jobs_for(), 'Does the `show_jobs` macro contain a `for` loop?'

@pytest.mark.test_show_jobs_macro_for_loop_body_module4
def test_show_jobs_macro_for_loop_body_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert 'show_jobs:jobs' in template_macros('_macros'), 'Have you created the `show_jobs` macro and added the correct parameter?'
    html = template_macro_soup('_macros', 'show_jobs')
    div = html.select('div.column.is-half')
    assert len(div) == 1, 'Has a `<div>` with classes of `column` and `is-half` been added to the `show_jobs` macro `for` loop body?'
    assert 'show_job:job' in show_jobs_for(), 'Does the `show_jobs` macro call `show_job`?'

@pytest.mark.test_import_macros_module4
def test_import_macros_module4():
    assert template_exists('_macros'), 'The `_macros.html` template does not exist in the `templates` folder.'
    assert '_macros.html:show_job:show_jobs:True' == template_import('layout'), 'Have you imported `_macros.html` in `layout.html`?'

@pytest.mark.test_index_template_module4
def test_index_template_module4():
    assert template_exists('index'), 'The `index.html` template does not exist in the `templates` folder.'
    el = template_data('index').select('.columns .column.is-one-fifth')
    assert len(el) == 1, 'Has the `HTML` from `templates.html` been copied to the `index.html` template?'

@pytest.mark.test_display_all_jobs_module4
def test_display_all_jobs_module4():
    assert template_exists('index'), 'The `index.html` template does not exist in the `templates` folder.'
    assert 'show_jobs:jobs' in template_functions('index', 'show_jobs'), 'Have you call the `show_jobs` macro in the `index.html` file?'

@pytest.mark.test_app_jobs_route_jobs_module4
def test_app_jobs_route_jobs_module4():
    assert 'jobs' in dir(app), 'Have you created the `jobs` function?'
    execute_sql = 'execute_sql:SELECT job.id, job.title, job.description, job.salary, employer.id as employer_id, employer.name as employer_name FROM job JOIN employer ON employer.id = job.employer_id' 
    assert execute_sql in get_functions(app.jobs), '`execute_sql` has not been called or has the wrong parameters.'
    assert 'render_template:index.html:jobs:jobs' in get_functions(app.jobs), 'Have you added `jobs` to the `render_template` call.'
