import pytest
import matplotlib
matplotlib.use('Agg')

from .utils import get_assignments, get_calls
from stats import offense

@pytest.mark.test_select_all_plays_module4
def test_select_all_plays_module4():
    assert 'games' in dir(offense), 'Have you imported `games` from `data`?'
    assert 'plays:games:games:type:play' in get_assignments(offense), 'Select the `play` rows of the `games` DataFrame.'
    assert 'plays:columns:type:inning:team:player:count:pitches:event:game_id:year' in get_assignments(offense), 'Set the correct column labels of the `games` DataFrame.'

@pytest.mark.test_select_only_hits_module4
def test_select_only_hits_module4():
    assert 'hits:plays:loc:plays:event:str:contains:^(?:S(?!B)|D|T|HR):inning:event' in get_assignments(offense), 'Refine the `games` DataFrame to contain only hits. Store the new DataFrame in a variable called `hits`.'

@pytest.mark.test_convert_column_type_module4
def test_convert_column_type_module4():
    assert 'hits:loc:None:None:None:inning:pd:to_numeric:hits:loc:None:None:None:inning' in get_assignments(offense), 'Change the data type of the `inning` column to numeric.'

@pytest.mark.test_replace_dictionary_module4
def test_replace_dictionary_module4():
    assert 'replacements:^S(.*):^D(.*):^T(.*):^HR(.*):single:double:triple:hr' in get_assignments(offense), '`replacements` is not a dictionary, doesn\'t exist, or contains the wrong values.'

@pytest.mark.test_replace_function_module4
def test_replace_function_module4():
    assert 'hit_type:hits:event:replace:replacements:regex:True' in get_assignments(offense), 'The `replace()` function is not used to replace the event column with the correct hit type.'

@pytest.mark.test_add_a_new_column_module4
def test_add_a_new_column_module4():
    assert 'hits:hits:assign:hit_type:hit_type' in get_assignments(offense), 'The new `hit_type` column has not been assign to the `hits` DataFrame.'

@pytest.mark.test_group_by_inning_and_hit_type_module4
def test_group_by_inning_and_hit_type_module4():
    assert 'hits:hits:groupby:inning:hit_type:size:reset_index:name:count' in get_assignments(offense), 'The `hits` DataFrame is not properly grouped.'

@pytest.mark.test_convert_hit_type_to_categorical_module4
def test_convert_hit_type_to_categorical_module4():
    assert 'hits:hit_type:pd:Categorical:hits:hit_type:single:double:triple:hr' in get_assignments(offense), 'The `hit_type` column is not `Categorical`.'

@pytest.mark.test_sort_values_module4
def test_sort_values_module4():
    assert 'hits:hits:sort_values:inning:hit_type' in get_assignments(offense), 'The `hits` DataFrame has not been sorted by `inning`.'

@pytest.mark.test_reshape_with_pivot_module4
def test_reshape_with_pivot_module4():
    pivot = False
    index = False
    columns = False
    values = False

    for string in get_assignments(offense):
        if 'hits:hits:pivot' in string:
            pivot = True
        if 'index:inning' in string:
            index = True
        if 'columns:hit_type' in string:
            columns = True
        if 'values:count' in string:
            values = True

    assert pivot, 'Are you calling `pivot()` on the `hits` DataFrame?'
    assert index, 'Does the call to `pivot()` have a keyword argument of `index` set to `\'inning\'`?'
    assert columns, 'Does the call to `pivot()` have a keyword argument of `columns` set to `\'strike_outs\'`?'
    assert values, 'Does the call to `pivot()` have a keyword argument of `values` set to `\'count\'`?'

@pytest.mark.test_stacked_bar_plot_module4
def test_stacked_bar_plot_module4():
    assert 'hits:plot:bar:stacked:True' in get_calls(offense), 'A stacked bar chart has not been plotted.'
    assert 'plt:show' in get_calls(offense), 'The plot has not been shown.'
