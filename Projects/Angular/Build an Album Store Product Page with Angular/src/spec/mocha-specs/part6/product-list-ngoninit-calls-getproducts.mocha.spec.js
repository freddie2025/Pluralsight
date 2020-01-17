let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductList', function() {
  it(`should call the ProductService's getProducts() method from ngOnInit() @product-list-ngoninit-calls-getProducts`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-list/product-list.component.ts').toString();
    } catch (e) {
      assert(false, "ProductListComponent doesn't exist yet.")
    }
    let re = /ngOnInit\(\s*\)\s*\{\s*([\w\s\(\)\.\_\=\>]+)\;?\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductList `ngOnInit()` method body doesn't contain anything.")

    let callToGetAlbum = match[1].trim();

    if (callToGetAlbum.includes('subscribe')) {
      let re2 = /this\._productService\s*\.\s*getProducts\(\)\s*\.\s*subscribe\(([\w\s\=\.\>]+)\)/
      let match2 = match[1].match(re2)
      assert(Array.isArray(match2), "The ProductList's `ngOnInit()` method body isn't chaining the correct call to subscribe onto the end of the call to `getProducts()`.")

      let variable_used_to_capture_response = match2[1].match(/\s*(\w+)\s*\=/);

      let expression = variable_used_to_capture_response[1] + "\\s*\\=\\>\\s*this\\.products\\s*\\=\\s*" + variable_used_to_capture_response[1]
      let regex = new RegExp(expression, 'g')

      assert(Array.isArray(match2[1].match(regex)), "The call to `getProducts()` in ProductList's `ngOnInit()` method body isn't subscribing to the response and assigning it to `this.products`.")
    } else {
      let re2 = /this\._productService\s*\.\s*getProducts\(\)/
      assert(match[0].match(re2), "The ProductList `ngOnInit()` method body isn't making the correct call to the ProductService's `getProducts` method.")
    }

  });

  it(`should define a class property named products @product-list-ngoninit-calls-getProducts`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product-list/product-list.component.ts').toString();
    } catch (e) {
      assert(false, "ProductListComponent doesn't exist yet.")
    }
    let re = /ProductListComponent\s*implements\s*OnInit\s*\{\s*(\w+)/
    let match = file.match(re);
    assert(match[1] == 'products', "The ProductList doesn't have a class property named `products`.")
  });
});
