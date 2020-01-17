import pandas as pd
import matplotlib.pyplot as plt

try:
    from data import games

    plays_frame = games.query("type == 'play' & event != 'NP'")
    plays_frame.columns = ['type', 'inning', 'team', 'player', 'count', 'pitches', 'event', 'game_id', 'year']

    info = games.query("type == 'info' & (multi2 == 'visteam' | multi2 == 'hometeam')")
    info = info.loc[:, ['year', 'game_id', 'multi2', 'multi3']]
    info.columns = ['year', 'game_id', 'team', 'defense']
    info.loc[info['team'] == 'visteam', 'team'] = '1'
    info.loc[info['team'] == 'hometeam', 'team'] = '0'
    info = info.sort_values(['year', 'game_id', 'team']).reset_index(drop=True)

    events = plays_frame.query("~(event.str.contains('^\d+') & ~event.str.contains('E'))")
    events = events.query("~event.str.contains('^(?:P|C|F|I|O)')")
    events = events.drop(['type', 'player', 'count', 'pitches'], axis=1)
    events = events.sort_values(['team', 'inning']).reset_index()
    replacements = {
      r'^(?:S|D|T).*': 'H',
      r'^HR.*': 'HR',
      r'^W.*': 'BB',
      r'.*K.*': 'SO',
      r'^HP.*': 'HBP',
      r'.*E.*\..*B-.*': 'RO',
      r'.*E.*': 'E',
    }
    event_type = events['event'].replace(replacements, regex=True)
    events = events.assign(event_type=event_type)
    events = events.groupby(['year', 'game_id', 'team', 'event_type']).size().reset_index(name='count')
except ImportError:
    print('It looks as if `data.py` is incomplete.')