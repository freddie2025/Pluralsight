const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');

const { JSDOM } = jsdom;


describe('BookList.vue', () => {
  it('should contain book-form element with event call @book-list-contains-event-call-from-book-form', () => {
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
      assert(false, "The BookForm component does not contain a template tag")
    }

    const content = parse5.serialize(template[0].content);
    const dom = new JSDOM(content, { includeNodeLocations: true, SVG_LCASE: true });
    const document = dom.window.document;

    // Test for booklist in the app div
    const results = document.querySelector('div');
    assert(results.innerHTML.includes('book-form'), 'BookList does not contain any `book-form` tags');

    assert(results.innerHTML.includes('v-on:addbook="appendBook"') || results.innerHTML.includes('@addbook="appendBook"'), 'BookList does not contain a call to `appendBook` on the `addBook` event');
  });
});
