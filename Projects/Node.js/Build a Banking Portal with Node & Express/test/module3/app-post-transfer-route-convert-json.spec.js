const fs = require('fs');
const path = require('path');

describe('Transfer post route writeJSON', () => {
  it('should include writeJSON @app-post-transfer-route-convert-json', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const stack = routeStack('/transfer', 'post') || routeStack('/services/transfer', 'post');
    assert(typeof stack !== 'undefined', 'The transfer post route may not exist yet.');
    if (fs.existsSync(path.join(process.cwd(), 'src/data.js'))) {
      assert(/writeJSON/.test(stack.handle.toString()), 'The transfer post function does not include a call to `writeJSON`.');
    } else {
      assert(/accountsJSON/.test(stack.handle.toString()), 'The transfer post function does not include an `accountsJSON` const.');
      assert(/JSON.stringify/.test(stack.handle.toString()), 'The transfer post function does not include a call to `JSON.stringify`.');
    }
  });
});
