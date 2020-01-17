const assert = require("chai").assert;
const cheerio = require("cheerio");
const parse5 = require("parse5");
const helpers = require("../helpers");

describe("ProductPage", () => {
  it("should have moved the tracklisting div out of the product-page component @product-tracklisting-html-moved", () => {
    const productPageFile = helpers.readFile(
      "src/app/product-page/product-page.component.html"
    );

    // parse html to test for description div inside ProductPage Component
    const productPageNodes = helpers.parseFile(productPageFile);
    const productPageMain = helpers.getHtmlTag("main", productPageNodes);
    const productPageContent = parse5.serialize(productPageMain[0]);
    let $ = cheerio.load(productPageContent);
    let tracklistingDiv = $(".tracklisting");

    helpers.readFile(
      "src/app/product-page/product-page.component.ts",
      "The ProductPageComponent doesn't exist for some reason."
    );

    helpers.readFile(
      "src/app/product-page/product-page.component.html",
      "The ProductPageComponent HTML file doesn't exist for some reason."
    );

    helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.html",
      "The ProductTracklistingComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );

    assert(
      tracklistingDiv.length === 0,
      "It looks like the ProductPageComponent still contains a `div` tag with a class of `tracklisting` - have you tried moving it yet?"
    );

    assert(
      $(".row").length > 1,
      "The ProductPageComponent should have two `<div></div>` elements with a class of `row`."
    );

    assert(
      $(".row")
        .first()
        .next()
        .children(".col-sm-8").length > 0,
      "The ProductPageComponent second `<div></div>` with a class of `row` should have a `<div></div>` with a class of `col-sm-8`."
    );

    assert(
      $(".row")
        .first()
        .next()
        .children(".col-sm-8")
        .children("app-product-tracklisting").length > 0,
      'You haven\'t added the `app-product-tracklisting` selector below the second `<div class="col-sm-8"></div>` element in the ProductPageComponent.'
    );
  });
});

describe("ProductTracklisting", () => {
  it("should contain the app-product-tracklisting element @product-tracklisting-html-moved", () => {
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.html"
    );

    // parse html to test for tracklisting div inside Producttracklisting Component
    let tracklisting;
    const productTracklistingNodes = helpers.parseFile(productTracklistingFile);
    productTracklistingNodes[0].attrs.find(
      attr => (tracklisting = attr.value.match(/tracklisting/))
    );

    let element;
    try {
      element = productTracklistingNodes[0].tagName;
    } catch (e) {
      assert(
        "The ProductTracklistingComponent's HTML file doesn't contain a `div` tag with a class of `tracklisting`."
      );
    }
    assert(
      element !== "p",
      "It looks like you have not replaced the `<p></p>` element with a `div` tag with a class of `tracklisting`."
    );

    assert(
      element === "div",
      "The ProductTracklistingComponent's HTML file doesn't contain a `div` tag."
    );

    assert(
      !!tracklisting,
      "It looks like the ProductTracklistingComponent does not contain the `tracklisting` `<div></div>` from the ProductPageComponent."
    );
  });
});
