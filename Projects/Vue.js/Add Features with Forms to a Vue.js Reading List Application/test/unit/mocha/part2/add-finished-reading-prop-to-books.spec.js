const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a data function that returns a bookData object @book-list-contain-finishedReading-property", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a `script` tag in the `BookForm` component."
      );
    }

    const ast = esprima.parse(script[0].childNodes[0].value, {
      sourceType: "module"
    });

    const data = esquery(ast, "Property[key.name=data]");
    assert(
      data.length > 0,
      "The `BookList`'s `data()` method's return is not present."
    );

    const books = esquery(ast, "Property[key.name=books]");
    assert(books.length > 0, "The BookList's `books` array is not present.");

    let finishedReading = esquery(
      books[0],
      "Property[key.name=finishedReading]"
    );

    assert(
      finishedReading.length === 3,
      "The `book` array should have a `finishedReading` property in each book object."
    );

    let finishedReadingTrue = esquery(
      books[0],
      "Property[key.name=finishedReading]>[value=true]"
    );

    assert(
      finishedReadingTrue.length == 2,
      "The `book` array should have two `finishedReading` properties with `true` as their values."
    );

    let finishedReadingFalse = esquery(
      books[0],
      "Property[key.name=finishedReading]>[value=false]"
    );

    assert(
      finishedReadingFalse.length == 1,
      "The `book` array should have one `finishedReading` property with `false` as its value."
    );
  });
});
