const fs = require('fs');
const path = require('path');
const rewire = require('rewire');

describe('writeJSON function', () => {
  let writeFileSyncStub;
  let writeJSONSpy;

  before(() => {
    writeFileSyncStub = sinon.stub(fs, 'writeFileSync');
  });

  it('`writeJSON()` should write to `accounts.json` @data-js-write-json-function-body', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/data.js')), 'The `src/data.js` file does not exist.');
    let writeJSON;
    try {
      const dataModule = rewire('../../src/data');
      writeJSON = dataModule.__get__('writeJSON');
    } catch (err) {
      assert(writeJSON !== undefined, '`data.js` does not contain a function called `writeJSON`.');
    }
    assert(typeof writeJSON === 'function', '`writeJSON` is not a function.');

    writeJSONSpy = sinon.spy(writeJSON);
    writeJSONSpy();
    assert(
      writeFileSyncStub.called,
      '`writeFileSync` has not been called in your `writeJSON` function.'
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
      'It is best if you encode the string as utf8'
    );
  });

  after(() => {
    writeFileSyncStub.restore();
  });
});
