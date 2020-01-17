const fs = require('fs');
const path = require('path');
const proxyquire = require('proxyquire');

describe('App uses account routes', () => {
  let useSpy;
  before(() => {
    useSpy = sinon.spy();
    proxyquire('../../src/app', {
      express: sinon.stub().returns({
        get: sinon.spy(),
        post: sinon.spy(),
        set: sinon.spy(),
        use: useSpy,
        listen: sinon.stub().returns({})
      })
    });
  });

  it('should contain app.use for account routes @app-use-account-routes', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes/accounts.js')), 'The `src/routes/accounts.js` file does not exist.');
    const accountRoutes = require(path.join(process.cwd(), 'src/routes/accounts.js'));
    assert(useSpy.calledWithExactly('/account', accountRoutes), 'Are you using your account routes?');
  });
});
