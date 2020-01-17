const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("ProductList", () => {
  it("should have an li element that contains the album name @product-list-ul-contains-li-with-albumnames", () => {
    let li, $, element;
    const productListFile = helpers.readFile(
      "src/app/product-list/product-list.component.html"
    );
    const productListNodes = helpers.parseFile(productListFile);

    helpers.readFile(
      "src/app/product-list/product-list.component.html",
      "The ProductListComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );

    try {
      productListNodes[0].attrs.find(attr => (list = attr.value.match(/list/)));
      const productListing = parse5.serialize(productListNodes[0]);
      $ = cheerio.load(productListing);
      li = $("li");
      element = productListNodes[0].tagName;
    } catch (e) {
      assert(
        "The ProductListComponent's HTML file doesn't contain an `ul` tag."
      );
    }

    assert(
      element !== "p",
      "It looks like you have not replaced the `<p></p>` element with an `ul` tag."
    );

    assert(
      element === "ul",
      "The ProductListComponent's HTML file doesn't contain an `ul` tag."
    );

    assert(
      li.length > 0,
      "It doesn't look like that there is a `<li></li>` element inside the ProductListComponent's HTML file."
    );

    assert(
      li.length < 2,
      "We shouldn't need more than one `<li></li>` element. We should be using the `ngFor` directive to generate the other list items."
    );

    assert(
      !!li.attr()["*ngfor"],
      "It doesn't look like that the ProductListComponent is using the `ngFor` directive."
    );

    assert(
      li.text().match(/\s*{{\s*product.albumName\s*}}\s*/),
      "The album names in your HTML template don't match the album names in the `products` JSON response."
    );
  });
});
