const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');

const { JSDOM } = jsdom;


describe('BookList.vue', () => {
  it('should contain li with template interpolation @book-list-will-contain-li-with-interpolation', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList.vue file does not exist');
    }

    // Parse document
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;

    // Parse for HTML in template
    const template = nodes.filter(node => node.nodeName === 'template');
    const content = parse5.serialize(template[0].content);
    const dom = new JSDOM(content, { includeNodeLocations: true, SVG_LCASE: true });
    const document = dom.window.document;

    // Test for booklist in the app div
    const results = document.querySelector('ul');
    if (results == null) {
      assert(false, "There is no `ul` tag in the BookList template.")
    }

    if (results.innerHTML.includes('book-item')) {
      assert(true);
    } else {
      assert(results.innerHTML.includes('li'), 'The BookList template does not contain any `li` tags');
      let re = /\{\{\s*book\.title\s*\}\}\s*\:\s*\{\{\s*book\.author\s*\}\}/
      let match = results.innerHTML.match(re)
      assert(match != null && match.length == 1, 'The BookList template\'s `li` element does not contain these interpolated values `{{book.title}}\:{{book.author}}`');
    }
  });
});
