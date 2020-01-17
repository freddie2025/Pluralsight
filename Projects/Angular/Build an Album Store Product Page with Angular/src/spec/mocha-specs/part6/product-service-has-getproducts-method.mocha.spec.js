let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should have a method named getProducts() that takes no parameters @product-service-has-getproducts-method`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /getProducts\s*\(\s*\)(\s*\:\s*Observable\<Product\[\]\>\s*)?\s*\{[\s\w\.\:\(\)\;=><\[\]]+\}/
    let match = file.match(re);
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The `getProducts()` method hasn't been added to the ProductService.");
  });
});