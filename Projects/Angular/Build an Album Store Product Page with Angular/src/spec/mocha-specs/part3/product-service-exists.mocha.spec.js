let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('ProductService', function() {
  it(`should exist @product-service-exists`, function () {
    assert(fs.existsSync(__dirname + '/../../../app/product.service.ts'), "The ProductService hasn't been created yet.");
  });

});