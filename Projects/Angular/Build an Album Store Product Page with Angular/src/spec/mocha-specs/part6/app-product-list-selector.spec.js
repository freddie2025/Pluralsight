const assert = require("chai").assert;
const helpers = require("../helpers");

describe("AppComponent", () => {
  it("should contain the app-product-list element @app-product-list-selector", () => {
    const file = helpers.readFile("src/app/app.component.html");
    const nodes = helpers.parseFile(file);
    const appProductComponent = helpers.getHtmlTag("app-product-page", nodes);
    const appProductList = helpers.getHtmlTag("app-product-list", nodes);

    assert(
      appProductComponent.length === 0,
      "The `app-product-list` tag hasn't replaced the `app-product-page` tag yet."
    );

    assert(
      appProductList.length > 0,
      "We couldn't find the `app-product-list` tag - are you sure you added the right selector to the AppComponent?"
    );
  });
});
