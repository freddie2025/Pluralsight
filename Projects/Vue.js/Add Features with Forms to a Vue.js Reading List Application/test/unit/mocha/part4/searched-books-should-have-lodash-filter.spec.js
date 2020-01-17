const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should have a computer property that search for books results @searched-books-contain-lodash-filter", () => {
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

    let returnStatement = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > ReturnStatement'
    );

    assert(
      returnStatement.length > 0,
      "The `searchedBooks` computed property is missing its own `return` keyword."
    );

    let importDeclaration = esquery(ast, "ImportDeclaration");

    assert(
      importDeclaration.length > 0,
      "The `BookList` component does not contain an import statement."
    );

    assert(
      importDeclaration[0].specifiers[0].local.name == "_",
      'The `BookList` component is not importing `_ from "lodash".` Add `import _ from "lodash";` at the top of the `<script></script>` tag. '
    );

    importDeclaration = esquery(
      ast,
      "ImportDeclaration > Literal[value=lodash]"
    );

    assert(
      importDeclaration.length > 0,
      'The `BookList` component is not importing `_ from "lodash".` Add `import _ from "lodash";` at the top of the `<script></script>` tag. '
    );

    let lodashCall = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="_"]'
    );

    assert(
      lodashCall.length > 0,
      "The `searchedBooks()` computed property  should be using `lodash`'s `filter` function. Make sure to add `_.filter()` after the `return` statement."
    );

    let filter = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="filter"]'
    );

    assert(
      filter.length > 0,
      "The `searchedBooks()` computed property should be using `lodash`'s `filter` function. Make sure to add `_.filter()` after your `return` statement."
    );

    let books = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="books"]'
    );

    assert(
      books.length > 0,
      "Inside the computed property `searchedBooks`, the lodash `_.filter` call should take `this.books` as its first argument."
    );

    let searchFilter = esquery(
      computed[0],
      'Property[key.name="searchedBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > Identifier[name="searchFilter"]'
    );

    assert(
      searchFilter.length > 0,
      "Inside the computed property `searchedBooks`, the lodash `_.filter` call should take `searchFilter` as its second argument."
    );
  });
});
