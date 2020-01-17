const fs = require('fs');
const path = require('path');
const rewire = require('rewire');
const R = require('ramda');

describe('Read account data', () => {
  it('should read account data @app-read-account-data', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let accounts;
    let accountData;
    try {
      if (fs.existsSync(path.join(process.cwd(), 'src/data.js'))) {
        const dataModule = rewire(path.join(process.cwd(), 'src/data.js'));
        accountData = dataModule.__get__('accountData');
        accounts = dataModule.__get__('accounts');
      } else {
        accountData = appModule.__get__('accountData');
        accounts = appModule.__get__('accounts');
      }
    } catch (err) {
      assert(accountData !== undefined, 'Has the `accountData` variable been created in `app.js`?');
      assert(accounts !== undefined, 'Has the `accounts` variable been created in `app.js`?');
    }
    assert(
      !Buffer.isBuffer(accountData),
      'It is best if you specify an encoding like "utf8" when reading from a file (readFileSync function).'
    );
    assert(typeof accounts === 'object', 'The accounts variable does not contain the correct information.');
    const accountsFound = R.allPass([R.has('savings'), R.has('checking'), R.has('credit')]);
    assert(accountsFound(accounts), 'The accounts variable does not contain the correct information. Check the accounts.json file.');
  });
});
