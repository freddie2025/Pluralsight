let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductDescription', function() {
  it(`should call the ProductService's getAlbum() method from ngOnInit() @product-description-ngoninit-calls-getalbum`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-description/product-description.component.ts').toString();
    } catch (e) {
      assert(false, "ProductDescriptionComponent doesn't exist yet.")
    }
    let re = /ngOnInit\(\s*\)\s*\{\s*([\w\s\(\)\.\_\=\>]+)\;?\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductDescription `ngOnInit()` method body doesn't contain anything.")

    let callToGetAlbum = match[1].trim();

    if (callToGetAlbum.includes('subscribe')) {
      let re2 = /this\._productService\s*\.\s*getAlbum\(1\)\s*\.\s*subscribe\(([\w\s\=\.\>]+)\)/
      let match2 = match[1].match(re2)
      assert(Array.isArray(match2), "The ProductDescription's `ngOnInit()` method body isn't chaining the correct call to subscribe onto the end of the call to `getAlbum()`.")

      let variable_used_to_capture_response = match2[1].match(/\s*(\w+)\s*\=/);

      let expression = variable_used_to_capture_response[1] + "\\s*\\=\\>\\s*this\\.albumInfo\\s*\\=\\s*" + variable_used_to_capture_response[1]
      let regex = new RegExp(expression, 'g')

      assert(Array.isArray(match2[1].match(regex)), "The call to `getAlbum()` in ProductDescription's `ngOnInit()` method body isn't subscribing to the response and assigning it to `this.albumInfo`.")
    } else {
      let re2 = /this\._productService\s*\.\s*getAlbum\(1\)/
      assert(match[0].match(re2), "The ProductDescription `ngOnInit()` method body isn't making the correct call to the ProductService's `getAlbum` method.")
    }

  });

  it(`should define a class property named albumInfo @product-description-ngoninit-calls-getalbum`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-description/product-description.component.ts').toString();
    } catch (e) {
      assert(false, "ProductDescriptionComponent doesn't exist yet.")
    }
    let re = /ProductDescriptionComponent\s*implements\s*OnInit\s*\{\s*(\w+)/
    let match = file.match(re);
    assert(match[1] == 'albumInfo', "The ProductDescription doesn't have a class property named `albumInfo`.")
  });
});