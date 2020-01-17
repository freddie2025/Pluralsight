const fs = require('fs');

describe('Transfer post route redirect', () => {
  let stack;
  let handleSpy;
  let writeFileSyncStub;

  before(() => {
    stack = routeStack('/transfer', 'post') || routeStack('/services/transfer', 'post');
    if (typeof stack === 'undefined') {
      handleSpy = {
        restore: () => { }
      };
    } else {
      handleSpy = sinon.spy(stack, 'handle');
    }
    writeFileSyncStub = sinon.stub(fs, 'writeFileSync');
  });

  it('should contain the transfer route @app-post-transfer-route-redirect', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const request = {
      body: {
        from: 'savings',
        to: 'checking',
        amount: 100
      }
    };
    const req = mockReq(request);
    const res = mockRes();
    assert(typeof handleSpy === 'function', 'The transfer post route may not exist.');
    handleSpy(req, res);
    assert(res.render.calledWithExactly('transfer', { message: 'Transfer Completed' }), '`res.render` is not being called with the correct arguments.');
  });

  after(() => {
    handleSpy.restore();
    writeFileSyncStub.restore();
  });
});
