const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a data function that contains searchInput property object @book-list-contain-search-input-property", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a `script` tag in the `BookList` component."
      );
    }

    const ast = esprima.parse(script[0].childNodes[0].value, {
      sourceType: "module"
    });

    const data = esquery(ast, "Property[key.name=data]");
    assert(
      data.length > 0,
      "The `BookList`'s `data()` method's `return` keyword is not present."
    );

    const searchInput = esquery(data[0], "Property[key.name=searchInput]");

    assert(
      searchInput.length > 0,
      "The `BookList`'s `searchInput` property is not present inside the `data()` object."
    );

    let searchValue = esquery(
      searchInput[0],
      'Property[key.name=searchInput][value.value=""]'
    );

    assert(
      searchValue.length > 0,
      'The `searchInput` data should start with an empty string `""` as its value.'
    );
  });
});
