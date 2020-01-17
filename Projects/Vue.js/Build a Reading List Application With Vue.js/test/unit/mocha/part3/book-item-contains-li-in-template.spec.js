const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');
const NW = require('nwmatcher');

const { JSDOM } = jsdom;


describe('BookItem.vue', () => {
  it('should contain li in template with book data @book-item-contains-li-with-book-data', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookItem.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookItem.vue file does not exist');
    }

    // Parse document
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;

    // Parse for HTML in template
    const template = nodes.filter(node => node.nodeName === 'template');
    if (template.length == 0) {
      assert(false, "The BookItem component does not contain a template tag")
    }

    const content = parse5.serialize(template[0].content);
    const dom = new JSDOM(content, { includeNodeLocations: true, SVG_LCASE: true });
    const document = dom.window.document;
    const nwmatcher = NW({ document });
    nwmatcher.configure({ ESCAPED: true });

    // Test for booklist in the app div
    const results = document.querySelector('li');
    if (results == null) {
      assert(false, "There is no `li` tag in the BookItem template.")
    }

    assert(results.innerHTML.includes('book.title'), 'The BookItem template does not contain an `li` tag');
  });
});
