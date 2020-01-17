const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');


describe('BookForm.vue', () => {
  it('should contain props @book-form-contains-props-with-books', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookForm.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookForm.vue file does not exist');
    }

    // Parse document and retrieve the script section
    const doc = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = doc.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    if (script.length == 0) {
      assert(false, "We either didn't find a script tag, or any code in a script tag in the BookForm component.")
    }

    // Test for correct import statement
    const ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });

    let results = esquery(ast, 'ExportDefaultDeclaration Property[key.name="name"] Literal[value="BookForm"]');
    assert(results.length > 0, 'The BookForm does not have a `name` property with the value of `BookForm`');

    results = esquery(ast, 'ExportDefaultDeclaration Property[key.name="props"] Literal[value="books"]');
    assert(results.length > 0, 'The BookForm does not have a `props` property with the value of `[\'books\']`');
  });
});
