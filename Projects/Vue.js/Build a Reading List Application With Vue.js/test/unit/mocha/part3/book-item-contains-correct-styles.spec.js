const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const cssom = require('cssom');


describe('BookItem.vue', () => {
  it('should contain correct styles for list items @book-item-contains-list-styles', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookItem.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookItem.vue file does not exist');
    }

    // Parse document and retrieve the style section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const styles = nodes.filter(node => node.nodeName === 'style');
    if (styles.length == 0) {
      assert(false, "The BookItem.vue file does not contain a style element.")
    }
    if (styles[0].childNodes.length == 0) {
      assert(false, "The BookItem style tag does not contain any CSS rules.")
    }
    const style = styles[0].childNodes[0].value;
    const parsed = cssom.parse(style);

    // Test for #app present in the styles
    const results = parsed.cssRules.find(node => node.selectorText === 'li');
    assert(results, 'There is no `li` selector in BookItem\'s styles');

    // Test for one of the fonts present in font-family
    assert(results.style.display === 'block', 'The li `display` property is not set to `block` in BookItem\'s styles');
  });
});


describe('BookList.vue', () => {
  it('should not contain styles for list item @book-list-should-not-contain-li-styles', () => {
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
    const results = parsed.cssRules.find(node => node.selectorText === 'li');
    assert(results === undefined, 'There should not be any `li` rules present in BookList\'s styles');
  });
});
