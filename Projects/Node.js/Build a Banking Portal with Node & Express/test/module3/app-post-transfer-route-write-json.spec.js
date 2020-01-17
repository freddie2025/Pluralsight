const fs = require('fs');
const path = require('path');

describe('Transfer post route write JSON', () => {
  let stack;
  let handleSpy;
  let writeFileSyncStub;

  before(() => {
    stack = routeStack('/transfer', 'post') || routeStack('/services/transfer', 'post');
    if (typeof stack === 'undefined') {
      handleSpy = {
        restore: () => {}
      };
    } else {
      handleSpy = sinon.spy(stack, 'handle');
    }
    writeFileSyncStub = sinon.stub(fs, 'writeFileSync');
  });

  it('should contain the index route @app-post-transfer-route-write-json', () => {
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
    assert(writeFileSyncStub.called, '`writeFileSync` was not called.');
    assert(
      writeFileSyncStub.firstCall.args[0] === path.join(__dirname, '../../src/json/accounts.json'),
      'The path being passed to `writeFileSync` is incorrect.'
    );
    assert(
      typeof writeFileSyncStub.firstCall.args[1] === 'string',
      'The content being passed to `writeFileSync` is not a string.'
    );
    assert(typeof writeFileSyncStub.firstCall.args[2] !== 'undefined', 'It is best if you encode the string as utf8.');
    assert(
      writeFileSyncStub.firstCall.args[2].replace('-', '').toLowerCase() === 'utf8',
      'It is best if you encode the string as utf8.'
    );
  });

  after(() => {
    handleSpy.restore();
    writeFileSyncStub.restore();
  });
});
