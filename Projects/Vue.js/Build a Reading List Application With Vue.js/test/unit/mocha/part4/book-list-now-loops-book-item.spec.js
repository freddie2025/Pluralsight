const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');

const { JSDOM } = jsdom;


describe('BookList.vue', () => {
  it('should loop book-item @book-list-will-loop-book-item', () => {
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
    assert(results.innerHTML.includes('book-item'), 'The BookList template does not contain any `book-item` tags');
    assert(results.querySelectorAll('book-item').length == 1, "There doesn't appear to be a single `book-item` element with opening and closing tags inside of the ul tag in BookList.vue.")
    assert(results.innerHTML.includes('v-for="book in books"'), 'The `book-item` tag does not have a `v-for` statement containing `book in books`');
    assert(results.innerHTML.includes(':book="book"'), 'The `book-item` tag does not have a `book` prop with the value of `book`');
  });
});