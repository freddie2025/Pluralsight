const fs = require('fs');
const path = require('path');

describe('`accounts.js` exports', () => {
  it('`accounts.js` should export router @routes-accounts-js-export-router', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes/accounts.js')), 'The `src/routes/accounts.js` file does not exist.');
    let localRouter;

    try {
      localRouter = require('../../src/routes/accounts');
    } catch (err) {
      assert(false, 'The `src/routes/accounts.js` file does not exist or can not be required.');
    }

    assert(localRouter !== undefined && typeof localRouter === 'function', '`src/routes/accounts.js` is not exporting the `router` function.');
  });
});
