let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should import the Http and Response classes @product-service-imports-http-and-response`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/product.service.ts').toString();
    } catch (e) {
      assert(false, "The ProductService doesn't exist yet.")
    }
    let re = /import\s*{([\w,\s]+)}\s*from\s*[\'|\"]@angular\/http[\'|\"]\;?/
    let match = file.match(re);
    assert(Array.isArray(match), "It doesn't look like anything has been imported from the `@angular/http` module yet.");

    let arr = match[1].split(',');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }
    assert(Array.isArray(arr) && arr.includes('Http'), "`Http` is not one of the classes that's been imported from `@angular/http`.")
    assert(Array.isArray(arr) && arr.includes('Response'), "`Response` is not one of the classes that's been imported from `@angular/http`.")
  });
});