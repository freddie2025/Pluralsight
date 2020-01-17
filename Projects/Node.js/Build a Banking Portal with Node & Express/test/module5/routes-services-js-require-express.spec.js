const fs = require('fs');
const path = require('path');
const rewire = require('rewire');

describe('Require Express and Create Router - services', () => {
  it('require express and create app const @routes-services-js-require-express', () => {
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
  });
});
