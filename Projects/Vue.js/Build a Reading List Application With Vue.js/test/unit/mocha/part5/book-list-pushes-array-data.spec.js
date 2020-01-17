const fs = require('fs');
const path = require('path');
const assert = require('chai').assert;
const parse5 = require('parse5');
const esquery = require('esquery');
const esprima = require('esprima');

describe('BookList.vue', () => {
  it('should contain a method that pushes to array contents @append-book-pushes-title-and-author', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/components/BookList.vue'), 'utf8');
    } catch (e) {
      assert(false, 'The BookList component does not exist');
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

    const methods = esquery(ast, 'Property[key.name=methods]');
    assert(methods.length > 0, 'The BookList\'s `methods` declaration is not present');

    let results = esquery(methods[0], 'Identifier[name="appendBook"]');
    assert(results.length > 0, 'The BookList\'s `methods` object is not defining an `appendBook()` method');

    results = esquery(methods[0], 'MemberExpression > MemberExpression > Identifier[name="books"]');
    assert(results.length > 0, 'The BookList\'s `appendBook()` method is not pushing anything to the array `books`');

    results = esquery(methods[0], 'CallExpression > ObjectExpression > Property[key.name="title"][value.name="bookTitle"]');
    assert(results.length > 0, 'In BookList\'s `appendBook()` method call, the `title` key is not sending the `bookTitle` argument');

    results = esquery(methods[0], 'CallExpression > ObjectExpression > Property[key.name="author"][value.name="bookAuthor"]');
    assert(results.length > 0, 'In BookList\'s `appendBook()` method call, the `author` key is not sending the `bookAuthor` argument');
  });
});
