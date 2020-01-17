const fs = require('fs');
const path = require('path');

describe('`src/routes/services.js` exists', () => {
  it('`src/routes/services.js` should exist  @routes-services-js-create-file', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/routes')), 'The `routes` dir does not exist.');
    assert(
      fs.existsSync(path.join(process.cwd(), 'src/routes/services.js')),
      'The `src/routes/accounts.js` file does not exist.'
    );
  });
});
