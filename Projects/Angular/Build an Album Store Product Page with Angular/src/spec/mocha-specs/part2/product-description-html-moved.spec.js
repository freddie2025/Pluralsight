const assert = require("chai").assert;
const cheerio = require("cheerio");
const parse5 = require("parse5");
const helpers = require("../helpers");

describe("ProductDescription", () => {
  it("should have moved the description div out of the product-page component @product-description-html-moved", () => {
    const productPageFile = helpers.readFile(
      "src/app/product-page/product-page.component.html"
    );
    const productDescriptionFile = helpers.readFile(
      "src/app/product-description/product-description.component.html"
    );

    // parse html to test for description div inside ProductPage Component
    const productPageNodes = helpers.parseFile(productPageFile);
    const productPageMain = helpers.getHtmlTag("main", productPageNodes);
    const productPageContent = parse5.serialize(productPageMain[0]);
    let $ = cheerio.load(productPageContent);
    let descriptionDiv = $(".description");

    // parse html to test for description div inside ProductDescription Component
    let description;
    const productDescriptionNodes = helpers.parseFile(productDescriptionFile);
    productDescriptionNodes[0].attrs.find(
      attr => (description = attr.value.match(/description/))
    );

    let element;
    try {
      element = productDescriptionNodes[0].tagName;
    } catch (e) {
      assert(
        "The ProductDescriptionComponent's HTML file doesn't contain a `div` tag with a class of `description`."
      );
    }

    helpers.readFile(
      "src/app/product-page/product-page.component.ts",
      "The ProductPageComponent doesn't exist for some reason."
    );

    helpers.readFile(
      "src/app/product-page/product-page.component.html",
      "The ProductPageComponent HTML file doesn't exist for some reason."
    );

    helpers.readFile(
      "src/app/product-description/product-description.component.html",
      "The ProductDescriptionComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );

    assert(
      descriptionDiv.length === 0,
      "It looks like the ProductPageComponent still contains a `div` tag with a class of `description` - have you tried moving it yet?"
    );

    assert(
      element !== "p",
      "It looks like you have not replaced the `<p></p>` element with a `div` tag with a class of `description`."
    );

    assert(
      element === "div",
      "The ProductDescriptionComponent's HTML file doesn't contain a `div` tag."
    );

    assert(
      !!description,
      "It looks like the ProductPageComponent still contains a `div` tag with a class of `description` - have you tried moving it yet?"
    );
  });
});
