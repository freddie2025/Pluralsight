const assert = require("chai").assert;
const cheerio = require("cheerio");
const parse5 = require("parse5");
const helpers = require("../helpers");

describe("ProductPageComponent", () => {
  it("should contain the app-product-description element @app-product-description-selector", () => {
    const productPageFile = helpers.readFile(
      "src/app/product-page/product-page.component.html"
    );

    // parse html to test for description div inside ProductPage Component
    const productPageNodes = helpers.parseFile(productPageFile);
    const productPageMain = helpers.getHtmlTag("main", productPageNodes);
    const productPageContent = parse5.serialize(productPageMain[0]);
    let $ = cheerio.load(productPageContent);
    let descriptionDiv = $(".description");
    let rowDiv = $(".row");

    helpers.readFile(
      "src/app/product-page/product-page.component.ts",
      "The ProductPageComponent doesn't exist for some reason."
    );

    helpers.readFile(
      "src/app/product-page/product-page.component.html",
      "The ProductPageComponent HTML file doesn't exist for some reason."
    );

    assert(
      descriptionDiv.length === 0,
      "It looks like the ProductPageComponent still contains a `div` tag with a class of `description` - have you tried moving it yet?"
    );

    assert(
      rowDiv.length > 2,
      "The ProductPageComponent should have three `<div></div>` elements with a class of `row`."
    );

    assert(
      $(".row")
        .first()
        .children("app-product-description").length > 0,
      'You haven\'t added the `app-product-description` selector below the first `<div class="row"></div>` element in the ProductPageComponent.'
    );
  });
});
