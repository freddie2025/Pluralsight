let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductTracklisting', function() {
  it(`should import the Album Interface @album-interface-imported-into-product-tracklisting`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-tracklisting/product-tracklisting.component.ts').toString();
    } catch (e) {
      assert(false, "ProductTracklistingComponent doesn't exist yet.")
    }
    let re = /import\s*{\s*Album\s*}\s*from\s*[\'|\"]\.\.\/album[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The Album Interface hasn't been imported into the ProductTracklistingComponent yet.");
  });
});