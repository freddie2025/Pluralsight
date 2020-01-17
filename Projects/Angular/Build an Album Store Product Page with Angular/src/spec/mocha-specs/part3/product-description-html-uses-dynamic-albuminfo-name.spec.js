const assert = require("chai").assert;
const cheerio = require("cheerio");
const parse5 = require("parse5");
const helpers = require("../helpers");

describe("ProductDescription", () => {
  it("should use album name data from the albumInfo property in the HTML template @product-description-html-uses-dynamic-albuminfo-name", () => {
    const productPageFile = helpers.readFile(
      "src/app/product-description/product-description.component.html"
    );

    const productPageNodes = helpers.parseFile(productPageFile);
    const productPageMain = helpers.getHtmlTag("div", productPageNodes);
    const productPageContent = parse5.serialize(productPageMain[0]);
    let $ = cheerio.load(productPageContent);
    let albumNameDiv = $(".album-name");

    helpers.readFile(
      "src/app/product-description/product-description.component.html",
      "The ProductDescriptionComponent HTML file doesn't exist for some reason."
    );

    assert(
      albumNameDiv.length > 0,
      "Something happened and it looks like the ProductDescription HTML file does not contain a paragraph with a class of `album-name`."
    );

    assert(
      albumNameDiv.text().match(/{{\s*albumInfo\?\.album\.name\s*}}/),
      "We'd like you to query the `albumInfo` property directly for the album name, and we're not seeing that you're doing that."
    );
  });
});
