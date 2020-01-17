const fs = require('fs');
const path = require('path');
const rewire = require('rewire');

describe('Require `data.js` - services', () => {
  it('should contain `services` const @routes-services-js-require-data', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes/services.js')), 'The `src/routes/services.js` file does not exist.');
    let accounts;
    let writeJSON;
    try {
      const servicesModule = rewire('../../src/routes/services');
      accounts = servicesModule.__get__('accounts');
      writeJSON = servicesModule.__get__('writeJSON');
    } catch (err) {
      assert(accounts !== undefined, 'Has an `accounts` constant been created when requiring the `data` library in `src/routes/services.js`?');
      assert(writeJSON !== undefined, 'Has the `writeJSON` function been created when requiring the `data` library in `src/routes/services.js`?');
    }
    assert(typeof accounts === 'object', 'Is the `accounts` constant an object?');
    assert(typeof writeJSON === 'function', 'Is `writeJSON` a function?');
  });
});
