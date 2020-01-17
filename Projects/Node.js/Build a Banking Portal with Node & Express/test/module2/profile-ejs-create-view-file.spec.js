const fs = require('fs');
const path = require('path');

describe('`profile.ejs` exists', () => {
  it('`profile.ejs` should exist  @profile-ejs-create-view-file', () => {
    try {
      fs.readFileSync(path.join(process.cwd(), 'src/views/profile.ejs'), 'utf8');
    } catch (err) {
      assert(err.code !== 'ENOENT', 'The `index.ejs` view file does not exist.');
    }
  });
});
