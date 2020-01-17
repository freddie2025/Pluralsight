const fs = require('fs');
const path = require('path');

describe('`data.js` exists', () => {
  it('`data.js` should exist  @data-js-create-file', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/data.js')), 'The `src/data.js` file does not exist.');
  });
});
