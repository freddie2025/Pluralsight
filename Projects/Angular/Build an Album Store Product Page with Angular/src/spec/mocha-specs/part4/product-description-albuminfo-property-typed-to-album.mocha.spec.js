let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductDescription', function() {
  it(`should have an albumInfo property typed to Album @product-description-albuminfo-property-typed-to-album`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-description/product-description.component.ts').toString();
    } catch (e) {
      assert(false, "ProductDescriptionComponent doesn't exist yet.")
    }
    let re = /albumInfo\s*\:\s*(\w+)/
    let match = file.match(re);
    assert(Array.isArray(file.match(re)), "The `albumInfo` property doesn't have any type information declared yet.");

    let albumInfoType = match[1].trim();
    assert(albumInfoType.includes('Album'), "The `albumInfo` type isn't declared as `Album`.");
  });
});