let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should have a private class property named _albumUrl set to the correct value @product-service-has-albumurl-property`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /private\s+\_albumUrl\s*(:\s*string\s*)?\=\s*[\'|\"](\.\.\/assets\/album.json)[\'|\"]\s*\;?/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductService doesn't have an `_albumUrl` property with the correct definition or value.")
  });
});