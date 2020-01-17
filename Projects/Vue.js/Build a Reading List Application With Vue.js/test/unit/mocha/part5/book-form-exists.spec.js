const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');


describe('BookForm.vue', () => {
  it('should exist with correct framework @book-form-exists', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookForm.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookForm.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    assert(script, 'No `script` tag exists in BookForm.vue');

    const template = nodes.filter(node => node.nodeName === 'template');
    assert(template, 'No `template` tag exists in BookForm.vue');

    const style = nodes.filter(node => node.nodeName === 'style');
    assert(style, 'No `style` tag exists in BookForm.vue');
  });
});
