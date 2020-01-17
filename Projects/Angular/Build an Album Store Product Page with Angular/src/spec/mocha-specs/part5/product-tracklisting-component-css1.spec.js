const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductTracklistingComponent", () => {
  it("should have CSS that contains a .tracklisting selector @product-tracklisting-component-css1", () => {
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
  });

  it(`should have CSS with a rule setting the font-size to 16px and the padding-top to 10px on the .tracklisting selector @product-tracklisting-component-css1`, () => {
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.css"
    );
    const styles = cssom.parse(productTracklistingFile);

    let tracklistingRule = _.find(styles.cssRules, {
      selectorText: ".tracklisting"
    });

    assert(
      tracklistingRule,
      "There isn't a `.tracklisting` selector with its correct value in the ProductTracklistingComponent's CSS file right now."
    );

    assert(
      tracklistingRule.style["font-size"] === "16px",
      "Your `.tracklisting` tag selector doesn't have a `font-size` property that's equal to `16px`."
    );

    assert(
      tracklistingRule.style["padding-top"] === "10px",
      "Your `.tracklisting` tag selector doesn't have a `padding-top` property that's equal to `10px`."
    );
  });
});
