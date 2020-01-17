const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookForm.vue", () => {
  it("should contain a radio with a `v-model` directive @book-form-will-contain-radio", () => {
    const file = helpers.readFile("src/components/BookForm.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const radio = $("form input[type=radio]");

    assert(
      radio.length == 2,
      "The form doesn't have two `<input>` elements with a `type` of `radio`."
    );

    $(radio).each(function(i, elem) {
      if (i === 0) {
        assert(
          $(this).attr()["v-model"],
          "The first `BookForm` radio does not have a `v-model` directive containing `bookData.ownership` as its value."
        );

        assert(
          $(this)
            .attr()
            ["v-model"].match(/\s*bookData.ownership\s*$/),
          "The first `BookForm` radio does not have a `v-model` directive containing `bookData.finishedReading` as its value."
        );
      }
      if (i === 1) {
        assert(
          $(this).attr()["v-model"],
          "The second `BookForm` radio does not have a `v-model` directive containing `bookData.ownership` as its value."
        );

        assert(
          $(this)
            .attr()
            ["v-model"].match(/\s*bookData.ownership\s*$/),
          "The second `BookForm` radio does not have a `v-model` directive containing `bookData.finishedReading` as its value."
        );
      }
    });
  });
});
