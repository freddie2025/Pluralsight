const assert = require("chai").assert;
const helpers = require("../helpers");

describe("Product Page Component HTML", () => {
  it("should contain the app-product-page element @app-product-page-selector", () => {
    const file = helpers.readFile("src/app/app.component.html");
    const nodes = helpers.parseFile(file);
    const h1 = helpers.getHtmlTag("h1", nodes);
    const appProductComponent = helpers.getHtmlTag("app-product-page", nodes);

    assert(
      h1.length === 0,
      "Let's make sure to replace the `<h1></h1>` element for the `<app-product-page></app-product-page>` component."
    );

    assert(
      appProductComponent.length > 0,
      "We couldn't find the ProductPageComponent - are you sure you added the right selector to the AppComponent?"
    );
  });
});
