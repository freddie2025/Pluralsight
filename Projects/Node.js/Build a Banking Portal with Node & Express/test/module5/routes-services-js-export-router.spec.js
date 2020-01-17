const fs = require('fs');
const path = require('path');

describe('`services.js` exports', () => {
  it('`services.js` should export router @routes-services-js-export-router', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(
      fs.existsSync(path.join(process.cwd(), 'src/routes/services.js')),
      'The `src/routes/services.js` file does not exist.'
    );
    let localRouter;

    try {
      localRouter = require('../../src/routes/services');
    } catch (err) {
      assert(false, 'The `src/routes/services.js` file does not exist or can not be required.');
    }

    assert(
      localRouter !== undefined && typeof localRouter === 'function',
      '`src/routes/services.js` is not exporting the `router` function.'
    );
  });
});
