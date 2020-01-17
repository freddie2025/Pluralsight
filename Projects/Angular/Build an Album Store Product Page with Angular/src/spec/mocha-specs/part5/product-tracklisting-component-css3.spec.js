const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductTracklistingComponent", () => {
  it("should have CSS that contains an li selector @product-tracklisting-component-css3", () => {
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

    let liRule = _.find(styles.cssRules, {
      selectorText: "li"
    });

    assert(
      liRule,
      "There isn't a `li` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );
  });

  it(`should have CSS with a rule setting the display to block and the line-height to 30px on the li selector @product-tracklisting-component-css3`, () => {
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css"
    );
    const styles = cssom.parse(productTracklistingFile);

    let liRule = _.find(styles.cssRules, {
      selectorText: "li"
    });

    assert(
      liRule,
      "There isn't a `li` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );

    assert(
      liRule.style["display"] === "block",
      "Your `li` tag selector doesn't have a `display` property that's equal to `block`."
    );

    assert(
      liRule.style["line-height"] === "30px",
      "Your `li` tag selector doesn't have a `line-height` property that's equal to `30px`."
    );
  });
});
