let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should have a method named getAlbum() that takes one parameter @product-service-has-getalbum-method`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.");
    }
    let re = /getAlbum\s*\(\s*id\s*:\s*number\s*\)(\s*\:\s*Observable\<Album\>\s*)?\s*\{[\s\w\.\:\(\)\;=><]+\}/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductService hasn't defined a `getAlbum` method yet with the correct arguments.")
  });
});