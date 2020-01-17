const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should contain a select with a `v-model` directive @book-list-will-contain-h2", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const h2 = $("h2");
    const hr = $("hr");

    assert(
      hr.length > 1,
      "The `BookList`'s template should have two `<hr>` elements separating the `Filtered Books` section. One `<hr>` should be separating it from the list of books, and the other one should be separating it from the form."
    );

    assert(
      hr.prevUntil("book-item").length > 0,
      "It appears that the `<hr>` element has not been added after the `<ul>` element with the `<book-item>` component."
    );

    assert(
      h2.length > 0,
      "The `BookList`'s template does not have a `<h2>` element."
    );

    assert(
      $(h2)
        .text()
        .match(/\s*Filtered\s*Books\s*by\s*Ownership/gi),
      "The `BookList`'s `<h2></h2>` element does not have `Filtered Books by Ownership` as its text."
    );
  });
});
