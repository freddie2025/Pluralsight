const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');


describe('BookItem.vue', () => {
  it('should exist with correct framework @book-item-exists', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookItem.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookItem.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    assert(script.length > 0, 'No `script` tag exists in BookItem.vue');

    const template = nodes.filter(node => node.nodeName === 'template');
    assert(template.length > 0, 'No `template` tag exists in BookItem.vue');

    const style = nodes.filter(node => node.nodeName === 'style');
    assert(style.length > 0, 'No `style` tag exists in BookItem.vue');
  });
});
