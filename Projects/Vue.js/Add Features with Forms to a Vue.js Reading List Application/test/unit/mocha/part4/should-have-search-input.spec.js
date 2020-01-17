const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should contain an input with a `v-model` directive @book-form-will-contain-search-input", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const input = $("input");

    assert(
      input.length > 0,
      "The `BookList`'s template does not have an `<input>` element."
    );

    assert(
      !!input.attr()["type"],
      "The `BookList`'s `<input>` does not have a `type` attribute containing `text` as its value."
    );

    assert(
      input.attr()["type"].match(/\s*text\s*$/),
      "The `BookList`'s `<input>` does not have a `type` attribute containing `text` as its value."
    );

    assert(
      !!input.attr()["placeholder"],
      "The `BookList`'s `<input>` does not have a `placeholder` attribute containing `Search Books` as its value."
    );

    assert(
      input.attr()["placeholder"].match(/\s*search\s*books\s*$/i),
      "The `BookList`'s `<input>` does not have a `placeholder` attribute containing `Search Books` as its value."
    );

    assert(
      !!input.attr()["v-model"],
      "The `BookList`'s `<input>` does not have a `v-model` directive containing `searchInput` as its value."
    );

    assert(
      input.attr()["v-model"].match(/\s*searchInput\s*$/),
      "The `BookList`'s `<input>` does not have a `v-model` directive containing `searchInput` as its value."
    );
  });
});
