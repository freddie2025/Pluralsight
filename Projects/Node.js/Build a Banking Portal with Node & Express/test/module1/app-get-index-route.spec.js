const R = require('ramda');

describe('Default Route', () => {
  let stack;
  let handleSpy;

  before(() => {
    stack = routeStack('/', 'get');
    if (typeof stack === 'undefined') {
      handleSpy = {
        restore: () => { }
      };
    } else {
      handleSpy = sinon.spy(stack, 'handle');
    }
  });

  it('should contain the index route @app-get-index-route', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const req = mockReq();
    const res = mockRes();

    assert(typeof handleSpy === 'function', 'No routes have been created.');
    handleSpy(req, res);
    assert(res.render.called, 'The index route may have not been created.');
    assert(res.render.firstCall.args[0] === 'index', 'The index route does not seem to be rendering the `index` view.');
    assert(
      R.has('title')(res.render.firstCall.args[1]),
      'The index route maybe missing an object with a `title: "Index"` key value pair.'
    );
  });

  after(() => {
    handleSpy.restore();
  });
});
