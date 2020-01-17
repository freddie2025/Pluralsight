const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;


describe('BookList.vue', () => {
  it('exists @book-list-component-exists', () => {
    try {
      fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList.vue file does not exist');
    }
  });
});
