const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;


describe('Vue', () => {
  it('library should exist @vue-project-exists', () => {
    try {
      fs.readFileSync(path.join(process.cwd(), 'node_modules/vue/README.md'), 'utf8');
    } catch (e) {
      assert(false, 'Vue does not exist in node_modules, have you run `npm install` yet?');
    }
  });
});
