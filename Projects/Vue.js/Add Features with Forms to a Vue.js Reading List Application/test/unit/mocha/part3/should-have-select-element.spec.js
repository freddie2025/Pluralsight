const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a select with a `v-model` directive @book-form-will-contain-select-tag", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const h2 = $("h2");
    const select = $("select");

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

    assert(
      select.length > 0,
      "The `BookList`'s template does not have a `<select>` element."
    );

    assert(
      !!select.attr()["v-model"],
      "The `BookList`s `<select></select>` element does not have a `v-model` directive containing `holding` as its value."
    );

    assert(
      select.attr()["v-model"].match(/\s*holding\s*$/),
      "The `BookList`'s `<select></select>` tag does not have a `v-model` directive containing `holding` as its value."
    );
  });
});
