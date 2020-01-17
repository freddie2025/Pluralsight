const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');


describe('BookList.vue', () => {
  it('should contain an array of books with titles and authors @book-list-will-contain-an-array-of-books', () => {
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
    const ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });

    // TODO: add extra assert to check if the array is in data() first, 
    // then after that assert passes let these tests run to check the properties.
    const results = esquery(ast, 'ExportDefaultDeclaration Property[key.name=data] Property[key.name="books"] Property[value.value="American Gods"]')
    assert(results.length > 0, 'A book with a title of American Gods is not in your books array');
  });
});
