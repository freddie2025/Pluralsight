let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('AppModule', function() {
  it(`should import the RouterModule and Routes classes @app-module-imports-routermodule`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /import\s*{([\w,\s]+)}\s*from\s*[\'|\"]@angular\/router[\'|\"]\;?/
    let match = file.match(re);
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "Nothing from `@angular/router` has been imported into the AppModule yet.")
    
    let arr = match[1].split(',');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    assert(Array.isArray(arr) && arr.includes('RouterModule'), "`RouterModule` is not currently imported from `@angular/router`.");
    assert(Array.isArray(arr) && arr.includes('Routes'), "`Routes` is not currently imported from `@angular/router`.");
  });
});