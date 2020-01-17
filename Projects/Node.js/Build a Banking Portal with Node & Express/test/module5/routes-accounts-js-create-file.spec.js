const fs = require('fs');
const path = require('path');

describe('`src/routes/accounts.js` exists', () => {
  it('`src/routes/accounts.js` should exist  @routes-accounts-js-create-file', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes')), 'The `routes` dir does not exist.');
    assert(
      fs.existsSync(path.join(process.cwd(), 'src/routes/accounts.js')),
      'The `src/routes/accounts.js` file does not exist.'
    );
  });
});
