import pytest
import matplotlib
import numpy as np
import pandas as pd
matplotlib.use('Agg')

from .utils import get_assignments, get_calls
from stats import attendance

@pytest.mark.test_import_pandas_module2
def test_import_pandas_module2():
    assert 'pd' in dir(attendance), 'Have you imported `pandas` as `pd`?'

@pytest.mark.test_import_matplotlib_module2
def test_import_matplotlib_module2():
    assert 'plt' in dir(attendance), 'Have you imported `matplotlib.pyplot` as `plt`?'

@pytest.mark.test_import_games_dataframe_module2
def test_import_games_dataframe_module2():
    assert 'games' in dir(attendance), 'Have you imported `games` from `data`?'

@pytest.mark.test_select_attendance_module2
def test_select_attendance_module2():
    try:
        from data import games
        local_attendance = games.loc[(games['type'] == 'info') & (games['multi2'] == 'attendance'), ['year', 'multi3']]
        assert 'attendance' in dir(attendance), 'Have you selected the attendance rows with `loc[]`, and assigned the resulting DataFrame to a variable called `attendance`?'

        if 'multi3' not in attendance.attendance.columns:
            local_attendance.columns = ['year', 'attendance']

        if attendance.attendance['attendance'].dtype == np.int64:
            local_attendance.loc[:, 'attendance'] = pd.to_numeric(local_attendance.loc[:, 'attendance'])

        assert attendance.attendance.equals(local_attendance), 'Have you selected the attendance rows with `loc[]`?'
    except ImportError:
        print('It looks as if `data.py` is incomplete.')

@pytest.mark.test_column_labels_module2
def test_column_labels_module2():
    assert 'attendance:columns:year:attendance' in get_assignments(attendance), 'Have you changed the column labels to `year` and `attendance`.'

@pytest.mark.test_convert_to_numeric_module2
def test_convert_to_numeric_module2():
    assert 'pd:to_numeric:attendance:loc:None:None:None:attendance' in get_calls(attendance), 'Convert the `attendance` column values from strings to numbers.'

@pytest.mark.test_plot_dataframe_module2
def test_plot_dataframe_module2():
    plot = False
    x = False
    y = False
    figsize = False
    kind = False
    for string in get_calls(attendance):
        if 'attendance:plot' in string:
            plot = True
        if 'x:year' in string:
            x = True
        if 'y:attendance' in string:
            y = True
        if 'figsize:15:7' in string:
            figsize = True
        if 'kind:bar' in string:
            kind = True

    assert plot, 'Are you calling `plot()` on the `attendance` DataFrame?'
    assert x, 'Does the call to `plot()` have a keyword argument of `x` set to `\'year\'`?'
    assert y, 'Does the call to `plot()` have a keyword argument of `y` set to `\'attendance\'`?'
    assert figsize, 'Does the call to `plot()` have a keyword argument of `figsize` set to `(15, 7)`?'
    assert kind, 'Does the call to `plot()` have a keyword argument of `kind` set to `\'bar\'`?'
    assert 'plt:show' in get_calls(attendance), 'Have you shown the plot?'

@pytest.mark.test_axis_labels_module2
def test_axis_labels_module2():
    assert 'plt:xlabel:Year' in get_calls(attendance), 'The x-axis label should be \'Year\'.'
    assert 'plt:ylabel:Attendance' in get_calls(attendance), 'The y-axis label should be \'Attendance\'.'

@pytest.mark.test_mean_line_module2
def test_mean_line_module2():
    axhline = False
    y = False
    label = False
    linestyle = False
    color = False
    for string in get_calls(attendance):
        if 'plt:axhline' in string:
            axhline = True
        if 'y:attendance:attendance:mean' in string:
            y = True
        if 'label:Mean' in string:
            label = True
        if 'linestyle:--' in string:
            linestyle = True
        if 'color:green' in string:
            color = True

    assert axhline, 'Are you calling `plt.axhline()`?'
    assert y, 'Does the call to `plt.axhline()` have a keyword argument of `y` set to `attendance[\'attendance\'].mean()`?'
    assert label, 'Does the call to `plt.axhline()` have a keyword argument of `label` set to `\'Mean\'`?'
    assert linestyle, 'Does the call to `plt.axhline()` have a keyword argument of `linestyle` set to `\'--\'`?'
    assert color, 'Does the call to `plt.axhline()` have a keyword argument of `color` set to `\'green\'`?'
