const fs = require('fs');
const path = require('path');

describe('`data.js` exports', () => {
  it('`data.js` should export an object @data-js-exports-data', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/data.js')), 'The `src/data.js` file does not exist.');
    let localAccounts;
    let localUsers;
    let localWriteJSON;

    try {
      const data = require('../../src/data');
      localAccounts = data.accounts;
      localUsers = data.users;
      localWriteJSON = data.writeJSON;
    } catch (err) {
      assert(false, 'The `src/data.js` file does not exist.');
    }

    assert(localAccounts !== undefined && typeof localAccounts === 'object', '`data.js` is not exporting the `accounts` object.');
    assert(localUsers !== undefined && typeof localUsers === 'object', '`data.js` is not exporting the `users` object.');
    assert(localWriteJSON !== undefined && typeof localWriteJSON === 'function', '`data.js` is not exporting the `writeJSON` function.');
  });
});
