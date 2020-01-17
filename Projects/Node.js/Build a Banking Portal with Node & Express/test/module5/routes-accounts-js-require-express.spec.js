const fs = require('fs');
const path = require('path');
const rewire = require('rewire');

describe('Require Express and Create Router - accounts', () => {
  it('require express and create app const @routes-accounts-js-require-express', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes/accounts.js')), 'The `src/routes/accounts.js` file does not exist.');
    let express;
    let router;
    try {
      const accountsModule = rewire('../../src/routes/accounts');
      express = accountsModule.__get__('express');
      router = accountsModule.__get__('router');
    } catch (err) {
      assert(express !== undefined, 'Has the `express` framework been required in `src/routes/accounts.js`?');
      assert(router !== undefined, 'Has the express `router` been added to `src/routes/accounts.js`?');
    }
    assert(typeof router === 'function', 'Has the `router` const been set to the express router function?');
  });
});
