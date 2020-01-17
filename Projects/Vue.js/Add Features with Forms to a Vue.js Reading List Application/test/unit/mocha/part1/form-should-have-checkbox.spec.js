const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should contain a checkbox with a `v-model` directive @book-form-will-contain-checkbox", () => {
    const file = helpers.readFile("src/components/BookForm.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const checkbox = $("form input[type=checkbox]");

    assert(
      checkbox.length > 0,
      "The form doesn't have an `<input>` element with a `type` of `checkbox`."
    );

    assert(
      !!checkbox.attr()["v-model"],
      "The `BookForm` `checkbox` does not have a `v-model` directive containing `bookData.finishedReading` as its value."
    );

    assert(
      checkbox.attr()["v-model"].match(/\s*bookData.finishedReading\s*$/),
      "The `BookForm` `checkbox` does not have a `v-model` directive containing `bookData.finishedReading` as its value."
    );
  });
});
