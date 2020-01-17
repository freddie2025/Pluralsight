const R = require('ramda');

describe('Checking and Credit Routes', () => {
  let creditStack;
  let checkingStack;
  let creditHandleSpy;
  let checkingHandleSpy;

  before(() => {
    creditStack = routeStack('/credit', 'get') || routeStack('/account/credit', 'get');
    if (typeof creditStack === 'undefined') {
      creditHandleSpy = { restore: () => { } };
    } else {
      creditHandleSpy = sinon.spy(creditStack, 'handle');
    }
    checkingStack = routeStack('/checking', 'get') || routeStack('/account/checking', 'get');
    if (typeof checkingStack === 'undefined') {
      checkingHandleSpy = { restore: () => { } };
    } else {
      checkingHandleSpy = sinon.spy(checkingStack, 'handle');
    }
  });

  it('should contain the credit and checking routes @app-get-other-account-routes', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const req = mockReq();
    const res = mockRes();

    assert(typeof creditHandleSpy === 'function', 'The credit get route has not been created.');
    creditHandleSpy(req, res);
    assert(res.render.called, 'The credit get route is not calling res.render.');
    assert(res.render.firstCall.args[0] === 'account', 'The credit route does not seem to be rendering the `account` view.');
    assert(typeof res.render.firstCall.args[1] === 'object', 'The credit route res.render may be missing arguments.');
    assert(
      R.has('account')(res.render.firstCall.args[1]),
      'The credit route maybe missing an object with a account key value pair.'
    );

    assert(typeof checkingHandleSpy === 'function', 'The checking get route has not been created.');
    checkingHandleSpy(req, res);
    assert(res.render.called, 'The checking get route is not calling res.render.');
    assert(res.render.firstCall.args[0] === 'account', 'The index route does not seem to be rendering the `index` view.');
    assert(typeof res.render.firstCall.args[1] === 'object', 'The checking route res.render may be missing arguments.');
    assert(
      R.has('account')(res.render.firstCall.args[1]),
      'The checking route may be missing an object with a account key value pair.'
    );
  });

  after(() => {
    creditHandleSpy.restore();
    checkingHandleSpy.restore();
  });
});
