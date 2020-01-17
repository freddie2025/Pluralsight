const assert = require("chai").assert;
const helpers = require("../helpers");
const cssom = require("cssom");
const _ = require("lodash");

describe("ProductDescriptionComponent", () => {
  it("should have correct CSS styles inside @product-description-component-css1", () => {
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

    let pRule = _.find(styles.cssRules, { selectorText: "p" });

    assert(
      pRule,
      "There isn't a paragraph selector in the ProductDescriptionComponent's CSS file right now."
    );

    assert(
      pRule.style["font-size"] === "16px",
      "Your paragraph selector doesn't have a `font-size` property that's equal to `16px`."
    );

    let fontRule;
    if (pRule.style["font"] != undefined) {
      fontRule = pRule.style["font"];
    } else if (pRule.style["font-family"] != undefined) {
      fontRule = pRule.style["font-family"];
    } else {
      assert(
        false,
        "Your paragraph selector doesn't have a properly declared `font-family` property."
      );
    }

    if (fontRule != undefined) {
      let split = fontRule.split(",");
      for (let i = 0; i < split.length; i++) {
        split[i] = split[i].trim();
      }
      assert(
        split[0] === "Helvetica",
        "Your paragraph selector doesn't have a `font-family` property that's equal to `Helvetica, Arial, sans-serif`."
      );

      assert(
        split[1] === "Arial",
        "Your paragraph selector doesn't have a `font-family` property that's equal to `Helvetica, Arial, sans-serif`."
      );

      assert(
        split[2] === "sans-serif",
        "Your paragraph selector doesn't have a `font-family` property that's equal to `Helvetica, Arial, sans-serif`."
      );

      assert(
        pRule.style["font-weight"] === "normal",
        "Your paragraph selector doesn't have a `font-weight` property that's equal to `normal`."
      );
    }
  });
});
