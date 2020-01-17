const assert = require("chai").assert;
const cheerio = require("cheerio");
const parse5 = require("parse5");
const helpers = require("../helpers");

describe("ProductDescription", () => {
  it("should use release date data from the albumInfo property in the HTML template @product-description-html-uses-dynamic-albuminfo-releasedate", () => {
    const productPageFile = helpers.readFile(
      "src/app/product-description/product-description.component.html"
    );

    const productPageNodes = helpers.parseFile(productPageFile);
    const productPageMain = helpers.getHtmlTag("div", productPageNodes);
    const productPageContent = parse5.serialize(productPageMain[0]);
    let $ = cheerio.load(productPageContent);
    let albumReleaseDate = $(".album-release-date");

    helpers.readFile(
      "src/app/product-description/product-description.component.html",
      "The ProductDescriptionComponent HTML file doesn't exist for some reason."
    );

    assert(
      albumReleaseDate.length > 0,
      "Something happened and it looks like the ProductDescription HTML file does not contain a paragraph with a class of `album-release-date`."
    );

    assert(
      albumReleaseDate
        .text()
        .match(/{{\s*albumInfo\?\.album\.releaseDate\s*}}/),
      "We'd like you to query the albumInfo property directly for the release date, and we're not seeing that you're doing that."
    );
  });
});
