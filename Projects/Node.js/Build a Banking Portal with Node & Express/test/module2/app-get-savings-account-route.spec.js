const R = require('ramda');

describe('Savings Route', () => {
  let stack;
  let handleSpy;

  before(() => {
    stack = routeStack('/savings', 'get') || routeStack('/account/savings', 'get');
    if (typeof stack === 'undefined') {
      handleSpy = {
        restore: () => {}
      };
    } else {
      handleSpy = sinon.spy(stack, 'handle');
    }
  });

  it('should contain the savings route @app-get-savings-account-route', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const req = mockReq();
    const res = mockRes();

    assert(typeof handleSpy === 'function', 'The savings get route has not been created.');
    handleSpy(req, res);
    assert(res.render.called, 'The index route may have not been created.');
    assert(
      res.render.firstCall.args[0] === 'account',
      'The savings route does not seem to be rendering the `account` view.'
    );
    assert(typeof res.render.firstCall.args[1] === 'object', 'res.render may be missing arguments');
    assert(
      R.has('account')(res.render.firstCall.args[1]),
      'The savings route may be missing an object with an account key value pair.'
    );
  });

  after(() => {
    handleSpy.restore();
  });
});
