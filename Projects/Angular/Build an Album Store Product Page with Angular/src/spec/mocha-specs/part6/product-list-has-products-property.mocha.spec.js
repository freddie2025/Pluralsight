let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductList', function() {
  it(`should have a class property named products of type Product[] @product-list-has-products-property`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-list/product-list.component.ts').toString();
    } catch (e) {
      assert(false, "ProductListComponent doesn't exist yet.")
    }
    let re = /products/
    let match1 = file.match(re);
    assert(Array.isArray(file.match(re)), "The `products` property doesn't exist yet.");

    let re2 = /products\s*\:\s*([\w\s\[\]]+)/
    let match2 = file.match(re2);
    assert(Array.isArray(file.match(re2)), "The `products` property doesn't have the correct type declaration.");

    let productsType = match2[1].trim();
    assert(productsType.includes('Product[]'), "The `products` type isn't declared as `Product[]`.");
  });
});