let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('AppModule', function() {
  it(`should import the ProductService @product-service-imported-into-app-module`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /import\s*{\s*ProductService\s*}\s*from\s*[\'|\"]\.\/product\.service[\'|\"]\;?/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "The ProductService hasn't been imported into the AppModule yet.")
  });
});