const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should add ownership to books array through appendBook() @add-ownership-inside-appendBook", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);
    let methods;

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a script tag in the `BookList` component."
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
      "The `BookList`'s `methods` declaration is not present."
    );

    let results = esquery(methods[0], 'Identifier[name="appendBook"]');
    assert(
      results.length > 0,
      "The `BookList`'s `methods` object is not defining an `appendBook()` method."
    );

    results = esquery(
      methods[0],
      'MemberExpression > MemberExpression > Identifier[name="books"]'
    );
    assert(
      results.length > 0,
      "The `BookList`'s `appendBook()` method is not pushing anything to the array `books`."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="title"] > MemberExpression[object.name="bookData"][property.name="bookTitle"]'
    );
    assert(
      results.length > 0,
      "In the `BookList`'s `appendBook()` method, the `title` key is not being assigned to the `bookData.bookTitle` value."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="author"] > MemberExpression[object.name="bookData"][property.name="bookAuthor"]'
    );
    assert(
      results.length > 0,
      "In the `BookList`'s `appendBook()` method, the `author` key is not being assigned to the `bookData.bookAuthor` value."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="finishedReading"]'
    );
    assert(
      results.length > 0,
      "The `BookList`'s `appendBook()` method should be pushing a `finishedReading` property with the `bookData.finishedReading` value to the `books` data."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="finishedReading"] > MemberExpression[object.name="bookData"][property.name="finishedReading"]'
    );
    assert(
      results.length > 0,
      "In the `BookList`'s `appendBook()` method call, the `finishedReading` key is not being assigned to the `bookData.finishedReading` value."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="ownership"]'
    );
    assert(
      results.length > 0,
      "The `BookList`'s `appendBook()` method should be pushing an `ownership` property with the `bookData.ownership` value to the `books` data."
    );

    results = esquery(
      methods[0],
      'CallExpression > ObjectExpression > Property[key.name="ownership"] > MemberExpression[object.name="bookData"][property.name="ownership"]'
    );
    assert(
      results.length > 0,
      "In the `BookList`'s `appendBook()` method call, the `ownership` key is not being assigned to the `bookData.ownership` value."
    );
  });
});
