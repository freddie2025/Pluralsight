let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService getAlbum Method', function () {
  it(`should import Observable from rxjs  @product-service-getalbum-method-returns-typed-observable`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /import\s*\{\s*Observable\s*\}\s*from\s*(\"|\')rxjs\/Observable(\"|\')\s*\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The Observable type hasn't been imported from rxjs yet.");
  });

  it(`should return an Observable typed to Album @product-service-getalbum-method-returns-typed-observable`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService hasn't been created yet.")
    }
    let re = /getAlbum\s*\(\s*id\s*:\s*number\s*\)\s*([\w\s\<\>\:]+)\{/
    let match = file.match(re);
    assert(Array.isArray(match), "The ProductService hasn't defined a `getAlbum` method yet with the correct arguments.")
    
    let the_type = match[1].trim();

    let re2 = /\s*\:\s*Observable\<Album\>/
    let match2 = the_type.match(re2);

    assert(Array.isArray(match2), "The `getAlbum` method doesn't have the correct return type.")
    assert(match2[0].includes('Observable<Album>'), "The `getAlbum` method doesn't have the correct return type.")
  });
});