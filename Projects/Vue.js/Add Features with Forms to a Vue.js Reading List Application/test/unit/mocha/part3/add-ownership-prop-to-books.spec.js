const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a data function that returns a bookData object @book-list-contain-ownership-property", () => {
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
      "The `BookList`'s `data()` property is not present."
    );

    const books = esquery(ast, "Property[key.name=books]");
    assert(books.length > 0, "The `BookList`'s `books` array is not present.");

    let ownership = esquery(books[0], "Property[key.name=ownership]");
    assert(
      ownership.length === 3,
      "The `books` array should have an `ownership` property in each object of the `books` array."
    );

    let ownershipBorrowed = esquery(
      books[0],
      "Property[key.name=ownership]>[value=borrowed]"
    );

    assert(
      ownershipBorrowed.length == 2,
      "The `books` array should have two `ownership` properties with `borrowed` as one of its values."
    );

    let ownershipBought = esquery(
      books[0],
      "Property[key.name=ownership]>[value=bought]"
    );

    assert(
      ownershipBought.length == 1,
      "The `books` array should have one `ownership` property with `bought` as one of  its value."
    );
  });
});
