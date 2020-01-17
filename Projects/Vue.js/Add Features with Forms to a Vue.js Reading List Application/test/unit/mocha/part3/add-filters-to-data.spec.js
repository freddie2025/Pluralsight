const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a data function that returns a bookData object @book-list-contain-filters-property", () => {
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
      "The `BookList`'s `data()` method's `return` is not present."
    );

    const filters = esquery(data[0], "Property[key.name=filters]");

    assert(
      filters.length > 0,
      "The `BookList`'s `filters` property is not present inside the `data()` object."
    );

    assert.isArray(
      filters,
      'It doesn\'t look like that we are assigning `filters` to an array with `"bought"` and `"borrowed"` as values.'
    );

    const bought = esquery(
      filters[0],
      "Property[key.name=filters] > ArrayExpression .elements[value=bought]"
    );

    assert(
      bought.length > 0,
      "The `filters` array should have a value of `bought`."
    );

    const borrowed = esquery(
      filters[0],
      "Property[key.name=filters] > ArrayExpression .elements[value=borrowed]"
    );

    assert(
      borrowed.length > 0,
      "The `filters` array should have a value of `borrowed`."
    );
  });
});
