const fs = require('fs');
const path = require('path');
const rewire = require('rewire');

describe('writeJSON function in data.js', () => {
  it('`data.js` should include a writeJSON function @data-js-write-json-function', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/data.js')), 'The `src/data.js` file does not exist.');
    let writeJSON;
    try {
      const dataModule = rewire('../../src/data');
      writeJSON = dataModule.__get__('writeJSON');
    } catch (err) {
      assert(writeJSON !== undefined, '`data.js` does not contain a function called `writeJSON`.');
    }
    assert(typeof writeJSON === 'function', '`writeJSON` is not a function.');
  });
});
