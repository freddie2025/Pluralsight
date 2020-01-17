const fs = require('fs');
const path = require('path');
const R = require('ramda');
const rewire = require('rewire');

describe('Move account routes', () => {
  it('`accounts.js` should contain routes @routes-accounts-js-move-routes', () => {
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
    assert(router.stack.length === 3, 'Were all three routes moved to `accounts.js` and added to the router?');

    const getRoutes = [];
    router.stack.forEach(routes => {
      if (routes.route.methods.get) {
        getRoutes.push(routes.route.path);
      }
    });

    assert(routeStack('/savings', 'get') === undefined, 'The savings route has not been removed from `app.js`.');
    assert(routeStack('/checking', 'get') === undefined, 'The checking route has not been removed from `app.js`.');
    assert(routeStack('/credit', 'get') === undefined, 'The credit route has not been removed from `app.js`.');

    assert(R.contains('/savings', getRoutes), 'The accounts router does not contain a savings route.');
    assert(R.contains('/checking', getRoutes), 'The accounts router does not contain a checking route.');
    assert(R.contains('/credit', getRoutes), 'The accounts router does not contain a credit route.');
  });
});
