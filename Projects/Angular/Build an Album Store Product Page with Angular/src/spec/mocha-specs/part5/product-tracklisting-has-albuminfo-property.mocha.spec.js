let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductTracklisting', function() {
  it(`should have a class property named albumInfo of type Album @product-tracklisting-has-albuminfo-property`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-tracklisting/product-tracklisting.component.ts').toString();
    } catch (e) {
      assert(false, "ProductTracklistingComponent doesn't exist yet.")
    }
    let re = /albumInfo/
    let match1 = file.match(re);
    assert(Array.isArray(file.match(re)), "The `albumInfo` property doesn't exist yet.");

    let re2 = /albumInfo\s*\:\s*(\w+)/
    let match2 = file.match(re2);
    assert(Array.isArray(file.match(re2)), "The `albumInfo` property doesn't have the correct type declaration.");

    let albumInfoType = match2[1].trim();
    assert(albumInfoType.includes('Album'), "The `albumInfo` type isn't declared as `Album`.");
  });
});