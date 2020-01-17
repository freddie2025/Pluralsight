const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');

const { JSDOM } = jsdom;


describe('BookList.vue', () => {
  it('should contain title interpolation in h1 @book-list-will-contain-title-interpolation', () => {
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
    
    if (template.length == 0) {
      assert(false, "The BookList component does not contain a template tag")
    }
    
    const content = parse5.serialize(template[0].content);
    const dom = new JSDOM(content, { includeNodeLocations: true });
    const document = dom.window.document;

    // Test for booklist in the app div
    const results = document.querySelector('h1');
    assert(results != null, "The BookList template does not contain an h1 tag")
    let re = /\{\{\s*title\s*\}\}/g
    let match = results.innerHTML.match(re)
    assert(match != null && match.length == 1, 'The BookList template does not contain the `{{title}}` in an `h1`');
  });
});
