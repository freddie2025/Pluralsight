const assert = require("chai").assert;
const parse5 = require("parse5");
const cheerio = require("cheerio");
const helpers = require("../helpers");

describe("ProductTracklisting", () => {
  it("should use data from the albumInfo.tracks property in the HTML template @product-tracklisting-html-uses-track-object-data", () => {
    let tracklisting;
    let element;
    const productTracklistingFile = helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.html"
    );
    const productTracklistingNodes = helpers.parseFile(productTracklistingFile);
    productTracklistingNodes[0].attrs.find(
      attr => (tracklisting = attr.value.match(/tracklisting/))
    );
    const productListing = parse5.serialize(productTracklistingNodes[0]);
    let $ = cheerio.load(productListing);
    const li = $("li");
    const trackNumber = $(".track-number");
    const trackName = $(".track-name");
    const trackTime = $(".track-time");
    const trackPrice = $(".price-and-buy");

    helpers.readFile(
      "src/app/product-tracklisting/product-tracklisting.component.html",
      "The ProductTracklistingComponent doesn't exist - have you run the `ng` command to generate it yet?"
    );

    try {
      element = productTracklistingNodes[0].tagName;
    } catch (e) {
      assert(
        "The ProductTracklistingComponent's HTML file doesn't contain a `div` tag with a class of `tracklisting`."
      );
    }

    assert(
      element !== "p",
      "It looks like you have not replaced the `<p></p>` element with a `div` tag with a class of `tracklisting`."
    );

    assert(
      element === "div",
      "The ProductTracklistingComponent's HTML file doesn't contain a `div` tag."
    );

    assert(
      !!tracklisting,
      "It looks like the ProductTracklistingComponent does not contain the `tracklisting` `<div></div>` from the ProductPageComponent."
    );

    assert(
      li.length > 0,
      "It doesn't look like that there is a `<li></li>` element."
    );

    assert(
      li.length < 2,
      "We shouldn't need more than one `<li></li>` element. We should be using the `ngFor` directive to generate the other list items."
    );

    assert(
      !!li.attr()["*ngfor"],
      "It doesn't look like that the ProductTracklistingComponent is using the `ngFor` directive."
    );

    assert(
      li
        .attr()
        ["*ngfor"].match(/\s*let\s*track\s*of\s*albumInfo\?.album.tracks/),
      "The `ngFor` directive doesn't have `let track of albumInfo?.album.tracks` as its value."
    );

    assert(
      trackNumber.hasClass("track-number"),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-number`."
    );

    assert(
      trackNumber.text().match(/\s*{{\s*track.trackNumber\s*}}\s*/),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-number` with a text of `{{track.trackNumber}}`."
    );

    assert(
      trackName.hasClass("track-name"),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-name`."
    );

    assert(
      trackName.text().match(/\s*{{\s*track.trackName\s*}}\s*/),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-name` with a text of `{{track.trackName}}`."
    );

    assert(
      trackTime.hasClass("track-time"),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-time`."
    );

    assert(
      trackTime.text().match(/\s*{{\s*track.trackLength\s*}}\s*/),
      "The ProductTrackinglistComponent should have a `span` with a class of `track-time` with a text of `{{track.trackLength}}`."
    );

    assert(
      trackPrice.hasClass("price-and-buy"),
      "The ProductTrackinglistComponent should have a `span` with a class of `price-and-buy`."
    );

    assert(
      trackPrice.text().match(/\s*{{\s*track.trackPrice\s*}}\s*/),
      "The ProductTrackinglistComponent should have a `span` with a class of `price-and-buy` with a text of `{{track.trackPrice}}`."
    );
  });
});
