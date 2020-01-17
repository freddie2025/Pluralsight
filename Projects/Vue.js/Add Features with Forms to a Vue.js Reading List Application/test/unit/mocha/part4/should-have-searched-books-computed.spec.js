const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should have a computer property that search for books results @search-input-produces-search-filter", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);
    let computed;

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a `script` tag in the `BookList` component."
      );
    }

    const ast = esprima.parse(script[0].childNodes[0].value, {
      sourceType: "module"
    });

    try {
      computed = esquery(ast, "Property[key.name=computed]");
    } catch (e) {
      assert(
        false,
        "Something went wrong and we weren't able to check your code."
      );
    }
    assert(
      computed.length > 0,
      "The `BookList`'s `computed` declaration is not present."
    );

    let results = esquery(computed[0], 'Identifier[name="searchedBooks"]');

    assert(
      results.length > 0,
      "The `BookList`'s `computed` object is not defining a `searchedBooks` computed property."
    );

    let constant = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration[kind="const"]'
    );

    assert(
      constant.length > 0,
      "Inside the computed property `searchedBooks`, we should have a `const searchFilter` declaration."
    );

    let searchFilter = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > Identifier[name="searchFilter"]'
    );

    assert(
      searchFilter.length > 0,
      "Inside the computed property `searchedBooks`, we should have a `const searchFilter` declaration."
    );

    let arrowFunction = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression '
    );

    assert(
      arrowFunction.length > 0,
      "We should be assigning an arrow function with a parameter of book `(book) => {}` to the `const searchFilter`."
    );

    let bookParam = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > Identifier[name=book]'
    );

    assert(
      bookParam.length > 0,
      "It doesn't look like we are passing `book` as a parameter to our arrow function like this `const searchFilter = (book) => {}`"
    );

    let returnStatement = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement'
    );

    assert(
      returnStatement.length > 0,
      "The `searchFilter` arrow function should have a `return` statement."
    );

    let book = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > CallExpression > MemberExpression Identifier[name=book]'
    );

    assert(
      book.length > 0,
      "It doesn't look like we are using `book.title.toLowerCase()` to return the searched book from inside the `searchedBooks` computed property."
    );

    let title = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > CallExpression > MemberExpression Identifier[name=title]'
    );

    assert(
      title.length > 0,
      "It doesn't look like we are using `book.title.toLowerCase()` to return the searched book from inside the `searchedBooks` computed property."
    );

    let firstLowerCase = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > CallExpression > MemberExpression > Identifier[name=toLowerCase]'
    );

    assert(
      firstLowerCase.length > 0,
      "It doesn't look like we are using `book.title.toLowerCase()` to return the searched book from inside the `searchedBooks` computed property."
    );

    let matchCall = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name=match]'
    );

    assert(
      matchCall.length > 0,
      "We should be chaning the `match` method after `book.title.toLowerCase()`."
    );

    let matchSearchInput = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression .arguments .property[name=searchInput]'
    );

    assert(
      matchSearchInput.length > 0,
      "The `match` method call should be taking `this.searchInput.toLowerCase()` as an argument."
    );

    let matchLowerCase = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > VariableDeclaration > VariableDeclarator > ArrowFunctionExpression > BlockStatement > ReturnStatement > CallExpression > CallExpression > MemberExpression > Identifier[name=toLowerCase]'
    );

    assert(
      matchLowerCase.length > 0,
      "We want to call the `lowerCase()` method in `this.searchInput`."
    );
  });
});
