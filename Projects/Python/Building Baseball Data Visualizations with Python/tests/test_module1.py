import pytest

from .utils import get_assignments, get_calls, get_for_loops
from stats import data

@pytest.mark.test_import_builtin_libraries_module1
def test_import_builtin_libraries_module1():
    assert 'os' in dir(data), 'Have you imported the built-in `os` library?'
    assert 'glob' in dir(data), 'Have you imported the built-in `glob` library?'

@pytest.mark.test_import_pandas_module1
def test_import_pandas_module1():
    assert 'pd' in dir(data), 'Have you imported `pandas` as `pd`?'

@pytest.mark.test_python_file_management_module1
def test_python_file_management_module1():
    assert 'game_files:glob:glob:os:path:join:os:getcwd:games:*.EVE' in get_assignments(data), 'Do you have a `glob.glob()` function call with the correct arguments?'

@pytest.mark.test_sorting_file_names_module1
def test_sorting_file_names_module1():
    assert 'game_files:sort' in get_calls(data), 'Are you sorting the `game_files` in-place with `sort()`?'

@pytest.mark.test_read_csv_files_module1
def test_read_csv_files_module1():
    assert len(get_for_loops(data, 'dict')) != 0, 'Do you have a `for` loop that loops through the `game_files`?'
    assert get_for_loops(data, 'dict')[0]['target:id'] == 'game_file' and get_for_loops(data, 'dict')[0]['iter:id'] == 'game_files', 'Do you have a `for` loop that loops through the `game_files`?'
    assert get_for_loops(data, 'dict')[0]['body'].startswith('game_frame:pd:read_csv:game_file:names:type:multi2:multi3:multi4:multi5:multi6:event'), 'Is there a call to the `pd.read_csv()` function in the body of the for loop?'

@pytest.mark.test_append_event_frames_module1
def test_append_event_frames_module1():
    assert 'game_frames' in get_assignments(data), 'Has the `game_frames` list been created?'
    assert 'game_frames:append:game_frame' in get_calls(data), 'Are you appending the current `game_frame` to `game_frames`?'

@pytest.mark.test_concatenate_dataframes_module1
def test_concatenate_dataframes_module1():
    assert 'games:pd:concat:game_frames' in get_assignments(data), 'The `pd.concat()` function should be passed the argument `game_frames` and assigned to `games`.'

@pytest.mark.test_clean_values_module1
def test_clean_values_module1():
    assert 'games:loc:games:multi5:??:multi5' in get_assignments(data), 'Are you replacing the `??` value in the `multi5` column with an empty string `\'\'`?'

@pytest.mark.test_extract_identifiers_module1
def test_extract_identifiers_module1():
    assert 'identifiers:games:multi2:str:extract:(.LS(\\d{4})\\d{5})' in get_assignments(data), 'Have the `year` and `game_id` been extracted?'

@pytest.mark.test_forward_fill_identifiers_module1
def test_forward_fill_identifiers_module1():
    assert 'identifiers:identifiers:fillna:method:ffill' in get_assignments(data), 'Have the `game_id` and `year` columns been filled with the correct values?'

@pytest.mark.test_rename_columns_module1
def test_rename_columns_module1():
    assert 'identifiers:columns:game_id:year' in get_assignments(data), 'The column labels of the `identifiers` DataFrame should be `game_id` and `year`.'

@pytest.mark.test_concatenate_identifier_columns_module1
def test_concatenate_identifier_columns_module1():
    concat = False
    frames = False
    axis = False
    sort = False

    for string in get_assignments(data):
        if 'games:pd:concat' in string:
            concat = True
        if 'games:identifiers' in string:
            frames = True
        if 'axis:1' in string:
            axis = True
        if 'sort:False' in string:
            sort = True

    assert concat, 'Are you calling `pd.concat()`?'
    assert frames, 'Does the call to `pd.concat()` have a list of DataFrames to concatenate as the first argument? Make sure the frames are in the correct order.'
    assert axis, 'Does the call to `pd.concat()` have a keyword argument of `axis` set to `1`?'
    assert sort, 'Does the call to `pd.concat()` have a keyword argument of `sort` set to `False`?'


@pytest.mark.test_fill_nan_values_module1
def test_fill_nan_values_module1():
    assert 'games:games:fillna: ' in get_assignments(data), 'The `NaN` values in the `identifiers` DataFrames have not been fill with a space.'

@pytest.mark.test_categorical_event_type_module1
def test_categorical_event_type_module1():
    assert 'games:loc:None:None:None:type:pd:Categorical:games:loc:None:None:None:type' in get_assignments(data), 'Save some memory by making the `type` column Categorical.'

@pytest.mark.test_print_dataframe_module1
def test_print_dataframe_module1():
    assert 'print:games:head' in get_calls(data) or 'print:games:head:5' in get_calls(data), 'To check the `games` DataFrame, `print()` the first five rows with `head()`.'
