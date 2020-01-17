const fs = require('fs');

describe('Transfer post route to balance', () => {
  let stack;
  let handleSpy;
  let handleOriginal;
  let writeFileSyncStub;

  before(() => {
    stack = routeStack('/transfer', 'post') || routeStack('/services/transfer', 'post');
    if (typeof stack === 'undefined') {
      handleSpy = {
        restore: () => { }
      };
    } else {
      handleOriginal = stack.handle;
      handleSpy = sinon.spy(stack, 'handle');
    }
    writeFileSyncStub = sinon.stub(fs, 'writeFileSync');
  });

  it('should calculate `to` balance @app-post-transfer-route-to-balance', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(typeof handleSpy === 'function', 'The transfer post route may not exist.');
    const request = { body: { from: 'savings', to: 'checking', amount: 100 } };

    let accounts;
    try {
      accounts = appModule.__get__('accounts');
    } catch (err) {
      assert(accounts !== undefined, 'Has the `accounts` variable been created in `app.js`?');
    }

    const req = mockReq(request);
    const res = mockRes();

    assert(handleOriginal.toString().match(/parseInt/).length >= 1, 'Make sure to use `parseInt`.');

    const currentBalance = accounts[request.body.to].balance;
    handleSpy(req, res);
    const newBalance = accounts[request.body.to].balance;

    assert(
      currentBalance + request.body.amount === newBalance,
      'Your calculation for the new `to` account balance seems to be incorrect.'
    );
  });

  after(() => {
    handleSpy.restore();
    writeFileSyncStub.restore();
  });
});
