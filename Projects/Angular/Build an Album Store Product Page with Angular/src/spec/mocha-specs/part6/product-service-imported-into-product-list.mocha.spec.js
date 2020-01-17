let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductList', function() {
  it(`should import the ProductService @product-service-imported-into-product-list`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-list/product-list.component.ts').toString();
    } catch (e) {
      assert(false, "ProductListComponent doesn't exist yet.")
    }
    let re = /import\s*{\s*ProductService\s*}\s*from\s*[\'|\"]\.\.\/product\.service[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The ProductService hasn't been imported into the ProductListComponent yet.");
  });
});