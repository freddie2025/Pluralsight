let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductTracklisting', function() {
  it(`should inject a private property named productService in the constructor @product-tracklisting-injects-product-service`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-tracklisting/product-tracklisting.component.ts').toString();
    } catch (e) {
      assert(false, "ProductTracklistingComponent doesn't exist yet.")
    }
    let re = /constructor\(([\w\s\_\:]+)\)/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductTracklisting constructor has no arguments.")

    let arg = match[1].trim();

    let re_arg = /\s*private\s+\_productService\s*\:\s*ProductService\s*/
    let arg_match = arg.match(re_arg);
    assert(Array.isArray(arg_match), "The ProductTracklisting constructor doesn't define a private `_productService` variable.");
  });
});