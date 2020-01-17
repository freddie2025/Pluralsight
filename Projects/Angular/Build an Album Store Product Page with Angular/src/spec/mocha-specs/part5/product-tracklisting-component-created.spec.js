const helpers = require("../helpers");

describe("Project", () => {
  it("should create the product tracklisting component @product-tracklisting-component-created", () => {
    helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.ts",
      "The ProductTracklistingComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );
  });
});
