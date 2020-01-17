const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should refactor v-model to use BookData @book-form-will-use-bookData", () => {
    const file = helpers.readFile("src/components/BookForm.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const input1 = $("form")
      .find("input")
      .first();

    assert(
      !!input1.attr()["v-model"],
      "The first input element from the `BookForm` component does not have a `v-model` directive."
    );

    assert(
      input1.attr()["v-model"].match(/\s*bookData.bookTitle\s*$/),
      "The `v-model` of the first form `<input>` tag should update its value to `bookData.bookTitle`."
    );

    assert(
      !!input1.next().attr()["v-model"],
      "The second input element from the `BookForm` component does not have a `v-model` directive."
    );

    assert(
      input1
        .next()
        .attr()
        ["v-model"].match(/\s*bookData.bookAuthor\s*$/),
      "The `v-model` of the second form `<input>` tag should update its value to `bookData.bookAuthor`."
    );
  });
});
