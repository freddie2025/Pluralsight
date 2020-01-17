const fs = require('fs');
const path = require('path');

describe('Payment Feature', () => {
  let getStack;
  let getHandleSpy;
  let postStack;
  let postHandleOriginal;
  let postHandleSpy;
  let writeFileSyncStub;

  before(() => {
    getStack = routeStack('/payment', 'get') || routeStack('/services/payment', 'get');
    if (typeof getStack === 'undefined') {
      getHandleSpy = { restore: () => {} };
    } else {
      getHandleSpy = sinon.spy(getStack, 'handle');
    }
    postStack = routeStack('/payment', 'post') || routeStack('/services/payment', 'post');
    if (typeof postStack === 'undefined') {
      postHandleSpy = { restore: () => {} };
    } else {
      postHandleOriginal = postStack.handle;
      postHandleSpy = sinon.spy(postStack, 'handle');
    }
    writeFileSyncStub = sinon.stub(fs, 'writeFileSync');
  });

  it('should contain payment feature @app-payment-feature', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');

    assert(typeof getHandleSpy === 'function', 'The payment get route may not exist.');
    const getReq = mockReq({});
    const getRes = mockRes();
    getHandleSpy(getReq, getRes);

    assert(getRes.render.called, 'The payment get route may have not been created.');
    assert(getRes.render.calledWithExactly('payment', sinon.match.object), '`res.render` is not being called with the correct arguments.');

    assert(typeof postHandleSpy === 'function', 'The payment post route may not exist.');
    let accounts;
    try {
      accounts = appModule.__get__('accounts');
    } catch (err) {
      assert(accounts !== undefined, 'Has the `accounts` variable been created in `app.js`?');
    }
    const postRequest = { body: { amount: 325 } };
    const postReq = mockReq(postRequest);
    const postRes = mockRes();

    const { available } = accounts.credit;
    const { balance } = accounts.credit;
    postHandleSpy(postReq, postRes);
    const newAvailable = accounts.credit.available;
    const newBalance = accounts.credit.balance;

    if (fs.existsSync(path.join(process.cwd(), 'src/data.js'))) {
      assert(/writeJSON/.test(postHandleOriginal.toString()), 'The transfer post function does not include a call to `writeJSON`.');
    } else {
      assert(/accountsJSON/.test(postHandleOriginal.toString()), 'The payment post function does not include an `accountsJSON` const.');
      assert(/JSON.stringify/.test(postHandleOriginal.toString()), 'The payment post function does not include a call to `JSON.stringify`.');
      assert(postHandleOriginal.toString().match(/parseInt/).length >= 1, 'Make sure to use `parseInt`.');
    }

    assert(postRes.render.called, 'The payment post route may have not been created.');
    assert(
      postRes.render.calledWithExactly('payment', sinon.match.object),
      '`res.render` is not being called with the correct arguments.'
    );
    assert(
      balance - postRequest.body.amount === newBalance,
      'Your calculation for the credit balance seems to be incorrect.'
    );
    assert(
      available + postRequest.body.amount === newAvailable,
      'Your calculation for the available balance seems to be incorrect.'
    );
    assert(
      writeFileSyncStub.firstCall.args[0] === path.join(__dirname, '../../src/json/accounts.json'),
      'The path being passed to `writeFileSync` is incorrect.'
    );
    assert(
      typeof writeFileSyncStub.firstCall.args[1] === 'string',
      'The content being passed to `writeFileSync` is not a string.'
    );
    assert(
      writeFileSyncStub.firstCall.args[2].replace('-', '').toLowerCase() === 'utf8',
      'It is best if you encode the string as utf8.'
    );
  });

  after(() => {
    getHandleSpy.restore();
    postHandleSpy.restore();
    writeFileSyncStub.restore();
  });
});
