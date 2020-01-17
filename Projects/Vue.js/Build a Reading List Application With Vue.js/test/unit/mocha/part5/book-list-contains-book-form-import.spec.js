const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');


describe('BookList.vue', () => {
  it('should contain BookForm component @book-list-vue-contains-book-form-import', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    if (script.length == 0) {
      assert(false, "We either didn't find a script tag, or any code in a script tag in the BookForm component.")
    }

    // Test for correct import statement
    let ast
    try {
      ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });
    } catch (e) {
      assert(false, "Something went wrong and we weren't able to check your code.")
    }

    let results = esquery(ast, 'ImportDeclaration[source.value="./BookForm"]');
    assert(results.length > 0, 'The `BookForm` class was not imported from `./BookItem` In BookList.vue');

    results = esquery(ast, 'Property[key.name=components] > ObjectExpression > Property[key.name=BookForm]');
    assert(results.length > 0, 'The value of the `components` property is not an object containing `BookForm` in BookList.vue');
  });
});
