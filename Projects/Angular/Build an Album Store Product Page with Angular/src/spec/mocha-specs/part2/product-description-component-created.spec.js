const fs = require("fs");
const helpers = require("../helpers");

describe("AppComponent", () => {
  it("should create the product description component @product-description-component-created", () => {
    helpers.readFile(
      "src/app/product-description/product-description.component.ts",
      "The ProductDescriptionComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );
  });
});
