const helpers = require("../helpers");

describe("Project", () => {
  it("should create the product list component @product-list-component-created", () => {
    helpers.readFile(
      "src/app/product-list/product-list.component.ts",
      "The ProductListComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );
  });
});
