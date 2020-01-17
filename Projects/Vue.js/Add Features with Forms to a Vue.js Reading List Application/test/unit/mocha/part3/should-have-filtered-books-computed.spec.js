const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should filter books based on bought or borrowed @add-filtered-books-computed-prop", () => {
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
    let importDeclaration = esquery(ast, "ImportDeclaration");

    assert(
      importDeclaration.length > 0,
      "The `BookList` component does not contain an `import` statement."
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

    assert(
      computed.length > 0,
      "The `BookList`'s `computed` declaration is not present."
    );

    let results = esquery(computed[0], 'Identifier[name="filteredBooks"]');

    assert(
      results.length > 0,
      "The `BookList`'s `computed` object is not defining an `filteredBooks` computed property."
    );

    let returnStatement = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement'
    );

    assert(
      returnStatement.length > 0,
      "The `filteredBooks` computed property is missing the `return` keyword."
    );

    let lodashCall = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="_"]'
    );

    assert(
      lodashCall.length > 0,
      "The `filteredBooks` property should be using `lodash`'s `filter` function. Make sure to add `_.filter()` after your `return` statement."
    );

    let filter = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="filter"]'
    );

    assert(
      filter.length > 0,
      "The `filteredBooks` property` should be using `lodash`'s `filter` function. Make sure to add `_.filter()` after your `return` statement."
    );

    let books = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > MemberExpression > Identifier[name="books"]'
    );

    assert(
      books.length > 0,
      "Inside the computed property `filteredBooks`, the lodash `_.filter` call should take `this.books` as its first argument."
    );

    let ownership = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > ArrayExpression > Literal[value="ownership"]'
    );

    assert(
      ownership.length > 0,
      'Inside the computed property `filteredBooks`, the lodash `_.filter` call should take `["ownership", this.holding]` as its second argument.'
    );

    let holding = esquery(
      computed[0],
      'Property[key.name="filteredBooks"] > FunctionExpression > BlockStatement > ReturnStatement > CallExpression > ArrayExpression > MemberExpression > Identifier[name="holding"]'
    );

    assert(
      holding.length > 0,
      'Inside the computed property `filteredBooks`, the lodash `_.filter` call should take `["ownership", this.holding]` as its second argument.'
    );
  });
});
