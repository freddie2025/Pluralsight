let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductList', function() {
  it(`should import the Product Interface @product-interface-imported-into-product-list`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-list/product-list.component.ts').toString();
    } catch (e) {
      assert(false, "ProductListComponent doesn't exist yet.")
    }
    let re = /import\s*{\s*Product\s*}\s*from\s*[\'|\"]\.\.\/product[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The Product Interface hasn't been imported into the ProductListComponent yet.");
  });
});