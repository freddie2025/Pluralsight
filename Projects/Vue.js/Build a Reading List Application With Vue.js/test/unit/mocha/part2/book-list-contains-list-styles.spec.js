const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const cssom = require('cssom');


describe('BookList.vue', () => {
  it('should contain correct styles for unordered lists @book-list-contains-list-styles', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList.vue file does not exist');
    }

    // Parse document and retrieve the style section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const styles = nodes.filter(node => node.nodeName === 'style');
    const style = styles[0].childNodes[0].value;
    const parsed = cssom.parse(style);

    // Test for #app present in the styles
    const results = parsed.cssRules.find(node => node.selectorText === 'ul');
    assert(results, 'There is no `ul` present in in BookList\'s styles');

    // Test for one of the fonts present in font-family
    assert(results.style['list-style-type'] === 'none', 'The `ul` elements `list-style-type` is not set to `none` in BookList\'s styles');
    assert(results.style['padding'] === '0', 'The `ul` elements `padding` is not set to `0` in BookList\'s styles');

    if(!fs.existsSync(path.join(process.cwd(), 'src/components/BookItem.vue'))) {
      const results2 = parsed.cssRules.find(node => node.selectorText === 'li');
      assert(results2, 'There is no `li` present in in BookList\'s styles');

      // Test for one of the fonts present in font-family
      assert(results2.style['display'] === 'block', 'The `li` elements `display` is not set to `block` in BookList\'s styles');
      assert(results2.style['margin'] === '0 10px' || results2.style['margin'] === '0 10px 0 10px', 'The `li` elements `margin` is not set to `0 10px` in BookList\'s styles');
    }
  });
});