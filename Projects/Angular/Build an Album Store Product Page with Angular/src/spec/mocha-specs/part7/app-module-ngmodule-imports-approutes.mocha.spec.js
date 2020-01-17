let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('AppModule', function() {
  it(`should add the appRoutes to the NgModule imports section @app-module-ngmodule-imports-approutes`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /imports\s*\:\s*\[\s*([\w\s\(\)\.\,]+)\]/g
    let match = re.exec(file);
    assert(Array.isArray(match), "There isn't an `imports` array in the `@NgModule` for some reason.")

    let arr = match[1].split(',');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    assert(Array.isArray(arr) && arr.includes('RouterModule.forRoot(appRoutes)'), "The `appRoutes` array hasn't been added in the `@NgModule`'s `imports` array.");
  });
});
