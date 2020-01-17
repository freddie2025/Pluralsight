let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should import the Product Interface @product-interface-imported-into-product-service`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /import\s*{\s*Product\s*}\s*from\s*[\'|\"]\.\/product[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The Product Interface hasn't been imported into the ProductService yet.");
  });
});