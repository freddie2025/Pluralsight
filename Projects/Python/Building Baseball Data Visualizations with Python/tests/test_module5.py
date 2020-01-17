import pytest
import matplotlib
matplotlib.use('Agg')

from .utils import get_assignments, get_calls
from stats import defense

@pytest.mark.test_import_existing_dataframes_module5
def test_import_existing_dataframes_module5():
    assert 'games' in dir(defense), 'Have you imported `games` from `frames`?'
    assert 'info' in dir(defense), 'Have you imported `info` from `frames`?'
    assert 'events' in dir(defense), 'Have you imported `events` from `frames`?'

@pytest.mark.test_query_function_module5
def test_query_function_module5():
    try:
        from stats import frames
        query = False
        for string in get_assignments(defense):
            if 'plays:games:query' in string:
                query = True
        assert query, 'Use the `query()` function to select rows that have a `type` of `play`. Do not select any rows that have an event type of `NP`.'
        plays = defense.plays
        plays.columns = ['type', 'inning', 'team', 'player', 'count', 'pitches', 'event', 'game_id', 'year']
        assert frames.plays_frame.equals(plays), 'The `query()` function is not returning the correct data. Check your conditions.'
    except ImportError:
        print('It looks as if `data.py` is incomplete.')

@pytest.mark.test_column_labels_module5
def test_column_labels_module5():
    assert 'plays:columns:type:inning:team:player:count:pitches:event:game_id:year' in get_assignments(defense), 'The column labels of the `play` DataFrame have not been renamed.'

@pytest.mark.test_shift_dataframe_module5
def test_shift_dataframe_module5():
    assert 'pa:plays:loc:plays:player:shift:plays:player:year:game_id:inning:team:player' in get_assignments(defense), 'Are you using `shift()` to check for consecutive duplicates?'

@pytest.mark.test_group_plate_appearances_module5
def test_group_plate_appearances_module5():
    assert 'pa:pa:groupby:year:game_id:team:size:reset_index:name:PA' in get_assignments(defense), 'The plate appearances have not been group by `year` and `game_id`.'

@pytest.mark.test_set_the_index_module5
def test_set_the_index_module5():
    assert 'events:events:set_index:year:game_id:team:event_type' in get_assignments(defense), 'Ensure you have set the index of the `events` DataFrame to `year` and `game_id`.'

@pytest.mark.test_unstack_the_dataframe_module5
def test_unstack_the_dataframe_module5():
    assert 'events:events:unstack:fillna:0:reset_index' in get_assignments(defense), '`unstack()` the `events` DataFrame and chain on calls to `fillna()` and `reset_index()`.'

@pytest.mark.test_manage_column_labels_module5
def test_manage_column_labels_module5():
    assert 'events:columns:events:columns:droplevel' in get_assignments(defense), 'Make sure to drop a level of labels from the `events` DataFrame.'
    assert 'events:columns:year:game_id:team:BB:E:H:HBP:HR:ROE:SO' in get_assignments(defense), 'The column labels of the `events` DataFrame have not been renamed.'
    assert 'events:events:rename_axis:None:axis:columns' in get_assignments(defense), 'Have you renamed the index column using the `rename_axis()` function?'

@pytest.mark.test_merge_plate_appearances_module5
def test_merge_plate_appearances_module5():
    merge = False
    how = False
    left_on = False
    right_on = False

    for string in get_assignments(defense):
        if 'events_plus_pa:pd:merge:events:pa' in string:
            merge = True
        if 'how:outer' in string:
            how = True
        if 'left_on:year:game_id:team' in string:
            left_on = True
        if 'right_on:year:game_id:team' in string:
            right_on = True

    assert merge, 'Are you calling `pd.merge()` to merge the `events` and `pa` DataFrames?'
    assert how, 'Does the call to `pd.merge()` have a keyword argument of `how` set to `\'outer\'`?'
    assert left_on, "Does the call to `pd.merge()` have a keyword argument of `left_on` set to `\'['year', 'game_id', 'team']\'`?"
    assert right_on, "Does the call to `pd.merge()` have a keyword argument of `right_on` set to `\'['year', 'game_id', 'team']\'`?"

@pytest.mark.test_merge_team_module5
def test_merge_team_module5():
    assert 'defense:pd:merge:events_plus_pa:info' in get_assignments(defense), 'Have the `events_plus_pa` DataFrame and the `info` DataFrame been merged?'

@pytest.mark.test_calculate_der_module5
def test_calculate_der_module5():
    assert 'defense:loc:None:None:None:DER:1:defense:H:defense:ROE:defense:PA:defense:BB:defense:SO:defense:HBP:defense:HR' in get_assignments(defense), 'Are you using the `1 - ((H + ROE - HR) / (PA - BB - SO - HBP - HR))` formula to calculate the DER?'
    no_loc = 'defense:year:pd:to_numeric:defense:year' in get_assignments(defense)
    both_loc = 'defense:loc:None:None:None:year:pd:to_numeric:defense:loc:None:None:None:year' in get_assignments(defense)
    left_loc = 'defense:loc:None:None:None:year:pd:to_numeric:defense:year' in get_assignments(defense)
    right_loc = 'defense:year:pd:to_numeric:defense:loc:None:None:None:year' in get_assignments(defense)

    assert no_loc or both_loc or left_loc or right_loc, 'Make sure to convert the `year` column of the `defense` DataFrame to numeric.'

@pytest.mark.test_reshape_with_pivot_module5
def test_reshape_with_pivot_module5():
    assert 'der:Name:defense:Name:loc:Attribute:defense:Name:year:Str:Index:Subscript:GtE:1978:Num:Compare:year:Str:defense:Str:DER:Str:List:Tuple:Index:Subscript:Assign' in get_assignments(defense, include_type=True), 'Select just the rows of the `defense` DataFrame with a year greater than 1978.'

    pivot = False
    index = False
    columns = False
    values = False

    for string in get_assignments(defense):
        if 'der:der:pivot' in string:
            pivot = True
        if 'index:year' in string:
            index = True
        if 'columns:defense' in string:
            columns = True
        if 'values:DER' in string:
            values = True

    assert pivot, 'Are you calling `pivot()` on the `der` DataFrame?'
    assert index, 'Does the call to `pivot()` have a keyword argument of `index` set to `\'year\'`?'
    assert columns, 'Does the call to `pivot()` have a keyword argument of `columns` set to `\'defense\'`?'
    assert values, 'Does the call to `pivot()` have a keyword argument of `values` set to `\'DER\'`?'

@pytest.mark.test_plot_formatting_xticks_module5
def test_plot_formatting_xticks_module5():
    assert 'der:plot:x_compat:True:xticks:range:1978:2018:4:rot:45' in get_calls(defense), 'Did you plot the `der` DataFrame with the correct label format?'
    assert 'plt:show' in get_calls(defense), 'Did you show the plot?'
