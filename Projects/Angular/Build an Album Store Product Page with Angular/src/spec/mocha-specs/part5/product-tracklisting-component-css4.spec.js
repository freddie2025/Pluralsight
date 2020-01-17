const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductTracklistingComponent", () => {
  it("should have CSS that contains a button selector @product-tracklisting-component-css4", () => {
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

    let buttonRule = _.find(styles.cssRules, {
      selectorText: "button"
    });

    assert(
      buttonRule,
      "There isn't a `button` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );
  });

  it(`should have CSS with a rule setting the line-height to 1 on the button selector @product-tracklisting-component-css4`, () => {
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css"
    );
    const styles = cssom.parse(productTracklistingFile);

    let buttonRule = _.find(styles.cssRules, {
      selectorText: "button"
    });

    assert(
      buttonRule,
      "There isn't a `button` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );

    assert(
      buttonRule.style["line-height"] === "1",
      "Your `button` tag selector doesn't have a `line-height` property that's equal to `1`."
    );
  });
});
