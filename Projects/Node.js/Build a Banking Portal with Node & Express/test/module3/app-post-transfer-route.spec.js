describe('Transfer post route', () => {
  let stub;
  let stack;
  before(() => {
    stack = routeStack('/transfer', 'post') || routeStack('/services/transfer', 'post');
    if (typeof stack === 'undefined') {
      stub = { restore: () => {} };
    } else {
      stub = sinon.stub(stack, 'handle');
    }
  });

  it('should contain the post transfer route @app-post-transfer-route', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const request = { body: { from: 'savings', to: 'checking', amount: 100 } };
    const req = mockReq(request);
    const res = mockRes();
    assert(typeof stub === 'function', 'The transfer post route may not exist.');
    stub(req, res);
    assert(stub.called, 'The transfer post route may have not been created yet.');
  });

  after(() => {
    stub.restore();
  });
});
