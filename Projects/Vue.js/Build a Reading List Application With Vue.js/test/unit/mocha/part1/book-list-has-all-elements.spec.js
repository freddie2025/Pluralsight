const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');


describe('BookList.vue', () => {
  it('should contain all component elements @book-list-contains-elements', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList.vue file does not exist');
    }

    // Parse document
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;

    // Parse for template
    const template = nodes.filter(node => node.nodeName === 'template');
    assert(template.length > 0, 'The BookList.vue file does not contain a template element');

    // Parse for script
    const script = nodes.filter(node => node.nodeName === 'script');
    assert(script.length > 0, 'The BookList.vue file does not contain a script element');

    // Parse for style
    const style = nodes.filter(node => node.nodeName === 'style');
    assert(style.length > 0, 'The BookList.vue file does not contain a style element');
  });
});
