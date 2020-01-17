const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should contain a 'book-item' element a v-for for filtered-books @book-item-v-for-has-filtered-books", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const tagName = helpers.getHtmlTag("template", nodes);
    const content = parse5.serialize(tagName[0].content);
    const $ = cheerio.load(content);
    const ul = $("ul");
    const bookItem = $("book-item");

    assert(
      ul.length > 1,
      "The BookList's template should have two `<ul></ul>` elements with a list of books."
    );

    assert(
      bookItem.length === 2,
      "For this task, make sure that you're writing `<book-item v-for='book in filteredBooks' :key='book.id' :book='book'></book-item>` in one line as you see in this message."
    );

    assert(
      bookItem.length > 1,
      "The `BookList`'s template does not contain two `<book-item></book-item>` components in it."
    );

    assert(
      !!bookItem.attr()["v-for"],
      "The `BookList's` `<book-item></book-item>` does not have a `v-for` directive containing 'book in filteredBooks' as its value."
    );

    let bookItem2;

    $("book-item").filter(function(i, el) {
      bookItem2 = $(this).attr("v-for") === "book in filteredBooks";
    });

    assert(
      !!bookItem2,
      "The second `BookList`'s `<book-item></book-item>` does not have a `v-for` directive containing 'book in filteredBooks' as its value."
    );
  });
});
