const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should contain a data function that returns a bookData object @book-form-emits-bookData", () => {
    const file = helpers.readFile("src/components/BookForm.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);

    let methods, bookSubmitMethod, bookDataParam, emmitBookData;

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a `script` tag in the `BookForm` component."
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
      "The BookForm's `methods` declaration is not present"
    );

    bookSubmitMethod = esquery(methods[0], 'Identifier[name="bookSubmit"]');
    assert(
      bookSubmitMethod.length > 0,
      "The BookForm's `methods` object is not defining a `bookSubmit()` method"
    );

    bookDataParam = esquery(
      methods[0],
      'Property[key.name="bookSubmit"] > FunctionExpression > Identifier[name="bookData"]'
    );

    assert(
      bookDataParam.length > 0,
      "We are not passing `bookData` as a parameter of `bookSubmit()`"
    );

    emmitBookData = esquery(
      methods[0],
      'Property[key.name="bookSubmit"] > FunctionExpression > BlockStatement > .body > CallExpression .arguments[name="bookData"]'
    );

    assert(
      emmitBookData.length > 0,
      "We are not calling `this.$emit()` with `bookData` as its second argument."
    );
  });
});
