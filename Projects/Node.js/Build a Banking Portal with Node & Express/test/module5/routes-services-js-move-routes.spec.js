const fs = require('fs');
const path = require('path');
const R = require('ramda');
const rewire = require('rewire');

describe('Move services routes', () => {
  it('`services.js` should contain routes @routes-services-js-move-routes', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes/services.js')), 'The `src/routes/services.js` file does not exist.');
    let express;
    let router;
    try {
      const servicesModule = rewire('../../src/routes/services');
      express = servicesModule.__get__('express');
      router = servicesModule.__get__('router');
    } catch (err) {
      assert(express !== undefined, 'Has the `express` framework been required in `src/routes/services.js`?');
      assert(router !== undefined, 'Has the express `router` been added to `src/routes/services.js`?');
    }
    assert(typeof router === 'function', 'Has the `router` const been set to the express router function?');
    assert(router.stack.length === 4, 'Were all four routes moved to `services.js`?');

    const getRoutes = [];
    router.stack.forEach(routes => {
      if (routes.route.methods.get) {
        getRoutes.push(routes.route.path);
      }
    });

    const postRoutes = [];
    router.stack.forEach(routes => {
      if (routes.route.methods.get) {
        postRoutes.push(routes.route.path);
      }
    });

    assert(routeStack('/transfer', 'get') === undefined, 'The transfer get route has not been removed from `app.js`.');
    assert(routeStack('/transfer', 'post') === undefined, 'The transfer post route has not been removed from `app.js`.');
    assert(routeStack('/payment', 'get') === undefined, 'The payment get route has not been removed from `app.js`.');
    assert(routeStack('/payment', 'post') === undefined, 'The payment post route has not been removed from `app.js`.');

    assert(R.contains('/transfer', getRoutes), 'The services router does not contain a transfer get route.');
    assert(R.contains('/transfer', postRoutes), 'The services router does not contain a transfer post route.');
    assert(R.contains('/payment', getRoutes), 'The services router does not contain a payment get route.');
    assert(R.contains('/payment', postRoutes), 'The services router does not contain a payment post route.');
  });
});
