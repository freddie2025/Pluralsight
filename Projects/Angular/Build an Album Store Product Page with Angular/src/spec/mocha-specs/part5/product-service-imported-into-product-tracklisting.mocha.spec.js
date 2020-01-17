let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductTracklisting', function() {
  it(`should import the ProductService @product-service-imported-into-product-tracklisting`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-tracklisting/product-tracklisting.component.ts').toString();
    } catch (e) {
      assert(false, "The ProductTracklistingComponent hasn't been created yet.")
    }
    let re = /import\s*{\s*ProductService\s*}\s*from\s*[\'|\"]\.\.\/product\.service[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The ProductService hasn't been imported into the ProductTracklisting yet.");
  });
});