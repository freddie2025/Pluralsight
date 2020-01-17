const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');

describe('BookForm.vue', () => {
  it('should contain a methods call to bookSubmit @book-form-emits-add-book', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookForm.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookForm component does not exist');
    }
    const document = parse5.parseFragment(file.replace(/\n/g, ''), { locationInfo: true });
    const nodes = document.childNodes;
    const script = nodes.filter(node => node.nodeName === 'script');
    if (script.length == 0) {
      assert(false, "We either didn't find a script tag, or any code in a script tag in the BookForm component.")
    }

    const ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });
    const methods = esquery(ast, 'Property[key.name=methods]');
    assert(methods.length > 0, 'The BookForm\'s `methods` declaration is not present');

    let results = esquery(methods[0], 'Identifier[name="bookSubmit"]');
    assert(results.length > 0, 'The BookForm\'s `methods` object is not defining a `bookSubmit()` method');

    results = esquery(methods[0], 'CallExpression[arguments] > Literal[value="addBook"]');
    assert(results.length > 0, 'The `bookSubmit()` method is emitting a call to `addBook`');
  });
});
