const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductDescriptionComponent", () => {
  it("should have CSS that contains an img selector @product-description-component-css2", () => {
    const productDescriptionFile = helpers.readFile(
      "src/app/product-description/product-description.component.css"
    );

    const styles = cssom.parse(productDescriptionFile);

    if (styles.cssRules.length == 0) {
      assert(
        false,
        "The ProductDescriptionComponent file does not contain any CSS rules or there is a CSS syntax error."
      );
    }

    let imgRule = _.find(styles.cssRules, { selectorText: "img" });

    assert(
      imgRule,
      "There isn't an image tag selector with its correct value in the ProductDescriptionComponent's CSS file right now."
    );

    assert(
      imgRule.style["width"] === "100%",
      "Your image tag selector doesn't have a `width` property that's equal to `100%`."
    );
  });
});
