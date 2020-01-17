import pytest
import matplotlib
matplotlib.use('Agg')

from .utils import get_assignments, get_calls
from stats import pitching

@pytest.mark.test_select_all_plays_module3
def test_select_all_plays_module3():
    assert 'games' in dir(pitching), 'Have you imported `games` from `data`?'
    assert 'plays:games:games:type:play' in get_assignments(pitching), 'Are you selecting just the rows that have a `type` of `play`? Make sure you are using the shortcut method of selection.'

@pytest.mark.test_select_all_strike_outs_module3
def test_select_all_strike_outs_module3():
    assert 'strike_outs:plays:plays:event:str:contains:K' in get_assignments(pitching), 'Are you using `str.contains()` to select just the events that are strike outs?'

@pytest.mark.test_group_by_year_and_game_module3
def test_group_by_year_and_game_module3():
    assert 'strike_outs:strike_outs:groupby:year:game_id:size' in get_assignments(pitching), 'Make sure to group by both `year` and `game_id` and take the `size()` of the group.'

@pytest.mark.test_reset_index_module3
def test_reset_index_module3():
    assert 'strike_outs:strike_outs:reset_index:name:strike_outs' in get_assignments(pitching), 'Don\'t forget to reset the index and give the new column the name `strike_outs`.'

@pytest.mark.test_apply_an_operation_to_multiple_columns_module3
def test_apply_an_operation_to_multiple_columns_module3():
    assert 'strike_outs:strike_outs:loc:None:None:None:year:strike_outs:apply:pd:to_numeric' in get_assignments(pitching), 'Convert the values in the `strike_out` column to numeric.'

@pytest.mark.test_change_plot_formatting_module3
def test_change_plot_formatting_module3():
    plot = False
    x = False
    y = False
    kind = False
    legend = False
    for string in get_calls(pitching):
        if 'strike_outs:plot' in string:
            plot = True
        if 'x:year' in string:
            x = True
        if 'y:strike_outs' in string:
            y = True
        if 'kind:scatter' in string:
            kind = True
        if 'legend:Strike Outs' in string:
            legend = True

    assert plot, 'Are you calling `plot()` on the `strike_outs` DataFrame?'
    assert x, 'Does the call to `plot()` have a keyword argument of `x` set to `\'year\'`?'
    assert y, 'Does the call to `plot()` have a keyword argument of `y` set to `\'strike_outs\'`?'
    assert kind, 'Does the call to `plot()` have a keyword argument of `kind` set to `\'scatter\'`?'
    assert legend, 'Have you chained a call to `legend()`?'
    assert 'plt:show' in get_calls(pitching), 'Show the scatter plot.'
