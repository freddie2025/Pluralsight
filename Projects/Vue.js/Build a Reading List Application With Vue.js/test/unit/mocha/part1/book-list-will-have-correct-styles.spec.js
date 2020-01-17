const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const cssom = require('cssom');

describe('BookList.vue', () => {
  it('should contain correct styles @book-list-vue-will-have-correct-styles', () => {
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

    if (styles.length == 0) {
      assert(false, "The BookList.vue file does not contain a style element.")
    }
    if (styles[0].childNodes.length == 0) {
      assert(false, "The BookList style tag does not contain any CSS rules.")
    }
    
    const style = styles[0].childNodes[0].value;
    const parsed = cssom.parse(style);

    const results = parsed.cssRules.find(node => node.selectorText);

    assert(results.selectorText, 'The `"h1, h2"` selector is not present in BookList\'s styles');

    let re = /h1\s*\,\s*h2/
    let match = results.selectorText.match(re)

    assert(match != null && match.length == 1, 'The `"h1, h2"` selector is not present in BookList\'s styles')
    
    // Test for one of the fonts present in font-family
    assert(results.style['font-weight'].includes('normal'), 'The `font-weight` is not set to `normal` in BookList\'s styles');
  });
});
