const fs = require('fs');
const path = require('path');

describe('`index.ejs` exists', () => {
  it('`index.ejs` should exist  @index-ejs-create-view-file', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/index.ejs')), 'The `index.ejs` view file does not exist.');
  });
});
