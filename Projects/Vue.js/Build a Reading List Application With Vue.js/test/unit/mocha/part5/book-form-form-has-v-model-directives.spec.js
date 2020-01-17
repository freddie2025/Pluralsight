const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const jsdom = require('jsdom');

const { JSDOM } = jsdom;


describe('BookForm.vue', () => {
  it('should contain a form with v-model directives @book-list-will-contain-v-model-directives', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookForm.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookForm.vue file does not exist');
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

    // Test for for form existance
    const results = document.querySelector('form');
    assert(results != null, 'The BookForm template does not contain a `form` tag');
    assert(results.length > 0, 'The BookForm template does not contain a `form` tag');

    assert(results.innerHTML.includes('v-model="bookTitle"'), 'The BookForm template does not have an input with a `v-model` attribute for `bookTitle`');

    assert(results.innerHTML.includes('v-model="bookAuthor"'), 'The BookForm template does not have an input with a `v-model` attribute for `bookAuthor`');
  });
});
