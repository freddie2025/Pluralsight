let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should inject a private property named http in the constructor @product-service-injects-http`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /constructor\(([\w\s\_\:]+)\)/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductService constructor has no arguments.")
    
    let arg = match[1].trim();

    let re_arg = /\s*private\s+\_http\s*\:\s*Http\s*/
    let arg_match = arg.match(re_arg);
    assert(Array.isArray(arg_match), "The ProductService constructor doesn't define a private `_http` variable.")
  });
});