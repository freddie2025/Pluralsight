let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('AppModule', function() {
  it(`should have a const array named appRoutes of type Routes @app-module-defines-approutes-array`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /const\s+appRoutes\s*\:\s*Routes\s*\=\s*\[[\w\s\:\'\"\,\{\}\/\;]*\]\;?\s*\@NgModule/
    let match = file.match(re);
    assert(Array.isArray(match), "There is currently no array of type Routes declared before `@NgModule`.");
  });
});