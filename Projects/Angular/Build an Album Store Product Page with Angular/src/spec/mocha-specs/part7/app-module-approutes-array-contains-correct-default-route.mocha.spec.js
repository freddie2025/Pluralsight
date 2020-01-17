let fs = require('fs');
let expect = require('chai').expect
let ts = require('typescript');
let assert = require('chai').assert

class ProductListComponent {

}

class ProductPageComponent {

}

let ar;

describe('AppModule', function() {
  it(`should have a const array named appRoutes where index 2 contains an object with the correct keys and values @app-module-approutes-array-contains-correct-default-route`, function () {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/app.module.ts').toString();
    } catch (e) {
      assert(false, "There is no `app.module.ts` file for some strange reason.")
    }
    let re = /(const\s+appRoutes\s*\:\s*Routes\s*\=\s*\[(?:[\w\s\:\'\"\,\{\}\/\;]*)\]\;?)\s*\@NgModule/
    let match = file.match(re);
    assert(match != undefined, "You haven't added an appRoutes array constant of type `Routes` in the correct place.");

    let match_trimmed = match[1].trim();

    let js = ts.transpile(match_trimmed)

    eval(js + "ar = appRoutes");
    
    if (ar.length > 2) {
      assert(ar[0].path == "products", "In the `appRoutes` array, the first object's `path` key is not set to `products`.")
      assert(ar[0].component.toString().match(/class ProductListComponent/), "In the `appRoutes` array, the first object's `component` key is not set to `ProductListComponent`.")
      assert(ar[1].path == "product/:id", "In the `appRoutes` array, the second object's `path` key is not set to `product/:id`.")
      assert(ar[1].component.toString().match(/class ProductPageComponent/), "In the `appRoutes` array, the second object's `component` key is not set to `ProductPageComponent`.")
      assert(ar[2].path == "", "In the `appRoutes` array, the third object's `path` key is not set to `\"\"`.")
      assert(ar[2].redirectTo == "products", "In the `appRoutes` array, the third object's `redirectTo` key is not set to `products`.")
      assert(ar[2].pathMatch == "full", "In the `appRoutes` array, the third object's `pathMatch` key is not set to `full`.")
    } else {
      assert(false, "You haven't added a third object with keys and values that represents a default route to the `appRoutes` array yet.");
    }
  });
});
