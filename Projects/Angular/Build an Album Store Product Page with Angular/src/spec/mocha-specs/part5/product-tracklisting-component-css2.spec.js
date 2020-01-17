const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductTracklistingComponent", () => {
  it("should have CSS that contains a ul selector @product-tracklisting-component-css2", () => {
    helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css",
      "The ProductTracklistingComponent CSS file doesn't exist - have you run the `ng` command to generate it yet?"
    );

    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css"
    );

    const styles = cssom.parse(productTracklistingFile);

    if (styles.cssRules.length == 0) {
      assert(
        false,
        "The ProductTracklistingComponent file does not contain any CSS rules or there is a CSS syntax error."
      );
    }

    let ulRule = _.find(styles.cssRules, {
      selectorText: "ul"
    });

    assert(
      ulRule,
      "There isn't an `ul` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );
  });

  it(`should have CSS with a rule setting the list-style-type to none on the ul selector @product-tracklisting-component-css2`, () => {
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css"
    );
    const styles = cssom.parse(productTracklistingFile);

    let ulRule = _.find(styles.cssRules, {
      selectorText: "ul"
    });

    assert(
      ulRule,
      "There isn't an `ul` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );

    assert(
      ulRule.style["list-style-type"] === "none",
      "Your `ul` tag selector doesn't have a `list-style-type` property that's equal to `none`."
    );
  });
});
