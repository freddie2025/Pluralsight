const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookItem.vue", () => {
  it("should show read or not read according to book.finishedReading @book-item-should-show-read", () => {
    const file = helpers.readFile("src/components/BookItem.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);

    const firstSpan = $("span");

    assert(
      $(firstSpan).length > 0,
      "It doesn't look like we are adding the `<span></span>` HTML element to the `BookList`'s template."
    );

    assert(
      !!firstSpan.attr()["v-if"],
      "The `BookItem`'s template does not have a `<span></span>` with a `v-if` directive."
    );

    assert(
      firstSpan.attr()["v-if"].match(/\s*book.finishedReading\s*$/),
      "The `BookItem`'s template does not have a `<span></span>` with a `v-if` directive containing `book.finishedReading` as its value."
    );

    assert(
      $(firstSpan)
        .text()
        .match(/\s*Read/gi),
      "The `BookItem`'s `<span></span>` with the `v-if` directive does not have a text of `Read`."
    );
  });
});
