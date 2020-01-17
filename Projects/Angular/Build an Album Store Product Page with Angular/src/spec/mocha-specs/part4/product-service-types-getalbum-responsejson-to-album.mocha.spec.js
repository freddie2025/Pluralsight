let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService getAlbum Method', function() {
  it(`should type response.json() to Album @product-service-types-getalbum-responsejson-to-album`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /return\s+this\.\_http\s*\.\s*get\(\s*this\.\_albumUrl\s*\)\s*\.\s*map\(([\w\s\(\)\=\>\.\<]+)\)/
    let match = file.match(re);
    assert(Array.isArray(file.match(re)), "The `getAlbum` method isn't returning the correct response.");
    
    let responseJsonPart = match[1];
    assert(responseJsonPart.includes('<Album>'), "You're not asserting that the type of `response.json()` is `Album`.");
  });
});