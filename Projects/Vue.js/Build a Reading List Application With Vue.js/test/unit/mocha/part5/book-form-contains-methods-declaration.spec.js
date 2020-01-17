const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');

describe('BookForm.vue', () => {
  it('should contain a methods declaration @book-form-contains-methods-declaration', () => {
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

    let ast
    try {
      ast = esprima.parse(script[0].childNodes[0].value, { sourceType: 'module' });
    } catch (e) {
      assert(false, "Something went wrong and we weren't able to check your code.")
    }

    let methods
    try {
      methods = esquery(ast, 'Property[key.name=methods]');
    } catch(e) {
      assert(false, "Something went wrong and we weren't able to check your code.")
    }
    assert(methods.length > 0, 'The BookForm\'s `methods` declaration is not present');
  });
});
