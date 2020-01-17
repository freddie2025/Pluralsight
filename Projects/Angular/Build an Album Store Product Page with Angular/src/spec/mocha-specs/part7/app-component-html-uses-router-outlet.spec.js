const assert = require("chai").assert;
const helpers = require("../helpers");

describe("AppComponent", () => {
  it("should only contain a single tag named router-outlet @app-component-html-uses-router-outlet", () => {
    const file = helpers.readFile("src/app/app.component.html");
    const nodes = helpers.parseFile(file);
    const appProductComponent = helpers.getHtmlTag("app-product-page", nodes);
    const appProductList = helpers.getHtmlTag("app-product-list", nodes);
    const routerOutlet = helpers.getHtmlTag("router-outlet", nodes);

    assert(
      appProductComponent.length === 0,
      "The AppComponent shouldn't have an `app-product-page` selector."
    );

    assert(
      appProductList.length === 0,
      "The `router-outlet` tag hasn't replaced the `app-product-list` tag yet."
    );

    assert(
      routerOutlet.length > 0,
      "There's currently no `router-outlet` tag in the AppComponent HTML file."
    );
  });
});
