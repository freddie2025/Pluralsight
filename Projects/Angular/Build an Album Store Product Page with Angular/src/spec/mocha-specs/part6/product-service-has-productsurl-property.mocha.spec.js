let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should have a private class property named _productsUrl set to the correct value @product-service-has-productsurl-property`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /private\s+\_productsUrl\s*(:\s*string\s*)?\=\s*[\'|\"](\.\.\/assets\/products.json)[\'|\"]\s*\;?/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductService doesn't have a `_productsUrl` property with the correct definition or value.")
  });
});