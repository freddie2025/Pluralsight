const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should pass bookData as an argument to appendBook() @refactor-appendBook-method", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);
    let methods;

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
      methods = esquery(ast, "Property[key.name=methods]");
    } catch (e) {
      assert(
        false,
        "Something went wrong and we weren't able to check your code."
      );
    }
    assert(
      methods.length > 0,
      "The `BookForm`'s `methods` declaration is not present."
    );

    let results = esquery(methods[0], 'Identifier[name="appendBook"]');
    assert(
      results.length > 0,
      "The `BookList`'s `methods` object is not defining an `appendBook()` method."
    );

    results = esquery(
      methods[0],
      'Property[key.name="appendBook"] > FunctionExpression > Identifier[name="bookData"]'
    );
    assert(
      results.length > 0,
      "The `appendBook()` method is not being called with `bookData` as its only argument."
    );
  });
});
