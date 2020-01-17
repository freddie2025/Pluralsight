let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Product Interface', function() {
  it(`should exist @product-interface-exists`, function () {
    assert(fs.existsSync(__dirname + '/../../../app/product.ts'), "The Product interface hasn't been created yet.");
    
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.ts').toString();
    } catch (e) {
      assert(false, "The Product interface hasn't been created yet.")
    }

    let re = /export\s+interface\s+Product/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "There's a `product.ts` file, but it doesn't export an interface named `Product`.");
  });
});