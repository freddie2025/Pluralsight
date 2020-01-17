let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('AppModule', function() {
  it(`should have a const array named appRoutes where index 0 contains an object with the correct keys and values @app-module-approutes-array-contains-correct-object1`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /const\s+appRoutes\s*\:\s*Routes\s*\=\s*\[([\w\s\:\'\"\,\{\}\/\;]*)\]\;?\s*\@NgModule/
    let match = file.match(re);
    assert(match != undefined, "You haven't added an appRoutes array constant of type `Routes` in the correct place.");

    let match_trimmed = match[1].trim();    
    let re2 = /\{\s*(?:\w+)\s*\:\s*(?:\'|\")(?:[\w\/\:]+)(?:\'|\")\s*\,\s*(?:\w+)\s*\:\s*(?:\w+)\s*\}\,?/g
    let match2 = match_trimmed.match(re2);
    assert(Array.isArray(match2), "You haven't added any objects with keys and values to the `appRoutes` array yet.");

    let re3 = /\{\s*(\w+)\s*\:\s*(?:\'|\")([\w\/\:]+)(?:\'|\")\s*\,\s*(\w+)\s*\:\s*(\w+)\s*\}/
    let match3 = match2[0].match(re3)

    let key1 = match3[1];
    let value1 = match3[2];
    let key2 = match3[3];
    let value2 = match3[4];

    assert(key1 == 'path', "The first object in the `appRoutes` constant doesn't have a `path` key.");
    assert(value1 == 'products', "The first object in the `appRoutes` constant's path key doesn't have a value of `products`.");
    assert(key2 == 'component', "The first object in the `appRoutes` constant doesn't have a `component` key.");
    assert(value2 == 'ProductListComponent', "The first object in the `appRoutes` constant's component key doesn't have a value of `ProductListComponent`.");
  });
});