let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductDescription', function() {
  it(`should inject a private property named productService in the constructor @product-description-injects-product-service`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-description/product-description.component.ts').toString();
    } catch (e) {
      assert(false, "ProductDescriptionComponent doesn't exist yet.")
    }
    let re = /constructor\(([\w\s\_\:]+)\)/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductDescription constructor has no arguments.")

    let arg = match[1].trim();

    let re_arg = /\s*private\s+\_productService\s*\:\s*ProductService\s*/
    let arg_match = arg.match(re_arg);
    assert(Array.isArray(arg_match), "The ProductDescription constructor doesn't define a private `_productService` variable.");
  });
});