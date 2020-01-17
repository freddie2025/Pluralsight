const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');


describe('BookList.vue', () => {
  it('should contain BookItem component @book-list-vue-contains-book-item-import', () => {
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

    // Test for correct import statement

    let ast
    try {
      ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });
    } catch (e) {
      assert(false, "It looks like your import statement is missing a semicolon at the end.")      
    }
    
    let results = esquery(ast, 'ImportDeclaration[source.value="./BookItem"]');
    assert(results.length > 0, 'The `BookItem` class was not imported from `./BookItem` In BookList.vue');

    results = esquery(ast, 'ImportSpecifier');
    assert(results.length == 0, 'Your import statement currently uses curly braces `{ }` to import the `BookItem` component - please replace that with just the name `BookItem`');
  });
});
