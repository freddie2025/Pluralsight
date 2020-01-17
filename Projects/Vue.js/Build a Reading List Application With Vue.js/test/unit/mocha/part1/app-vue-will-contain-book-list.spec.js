const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');
const jsdom = require('jsdom');


const { JSDOM } = jsdom;


describe('App.vue', () => {
  it('should include booklist in App.vue @app-vue-will-use-book-list', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/App.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The App.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');

    // Test for correct import statement
    const ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });
    let importDeclaration = esquery(ast, 'ImportDeclaration');

    assert(importDeclaration.length > 0, "The App component does not contain an import statement");
    assert(importDeclaration[0].specifiers[0].local.name == 'BookList', 'App.vue isn\'t importing a class named `BookList`');

    let results = esquery(ast, 'ImportDeclaration[source.value="./components/BookList"]');
    assert(results.length > 0, '`./components/BookList` was not imported');

    // Test for bookList definition in the component key
    results = esquery(ast, 'Property[key.name=components] > ObjectExpression > Property[key.name=BookList]');
    assert(results.length > 0, '`BookList` is not defined as a value in an object in the `components` property');

    // Parse for HTML in template
    const template = nodes.filter(node => node.nodeName === 'template');
    const content = parse5.serialize(template[0].content);
    const dom = new JSDOM(content, { includeNodeLocations: true });
    const document = dom.window.document;

    // Test for booklist in the app div

    results = document.querySelectorAll('div#app book-list');
    assert(results.length == 1, "There doesn't appear to be a single `book-list` element with opening and closing tags inside of a div in App.vue.")
    assert(results, 'The `<book-list>` element does not exist inside of a div in App.vue.');
  });
});
