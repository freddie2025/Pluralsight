const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');


describe('BookItem.vue', () => {
  it('should contain props @book-item-contains-props-with-book', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookItem.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookItem.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });

    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    if (script.length == 0) {
      assert(false, "We either didn't find a script tag, or any code in a script tag in the BookItem component.")
    }

    // Test for correct import statement
    const ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });

    let results = esquery(ast, 'ExportDefaultDeclaration Property[key.name="name"] Literal[value="BookItem"]');
    assert(results.length > 0, 'The BookItem component does not have a `name` property with the value of `BookItem` defined in the `export default` section');

    results = esquery(ast, 'ExportDefaultDeclaration Property[key.name="props"]');
    assert(results.length > 0 && results[0].value.type == 'ArrayExpression' && results[0].value.elements[0].value == 'book', 'The BookItem component does not have a `props` property with an array value containing the string `book` defined in the `export default` section');
  });
});
