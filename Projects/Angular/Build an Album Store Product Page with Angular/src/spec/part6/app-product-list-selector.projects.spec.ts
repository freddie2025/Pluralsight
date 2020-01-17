import { TestBed, async } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { AppComponent } from '../../app/app.component';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let productListComponentExists = false;
let ProductListComponent;
try {
  ProductListComponent = require('../../app/product-list/product-list.component.ts').ProductListComponent;
  productListComponentExists = true;
} catch (e) {
  productListComponentExists = false;
}

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
    }).compileComponents();
  }));

  it(`should contain the app-product-list element @app-product-list-selector`, async(() => {
    since('The ProductListComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productListComponentExists).toBe(true);

    const fixture = TestBed.createComponent(AppComponent);
    const childNodes = fixture.debugElement.childNodes;

    let productPageFound = 0, productListFound = 0, routerOutletFound = 0;

    childNodes.forEach(element => {
      if (element.nativeNode.nodeType == 1) {
        if (element.nativeNode.localName == 'router-outlet') {
          routerOutletFound = routerOutletFound + 1;
        }
        if (element.nativeNode.localName == 'app-product-list') {
          productListFound = productListFound + 1;
        }
        if (element.nativeNode.localName == 'app-product-page') {
          productPageFound = productPageFound + 1;
        }
      }
    });


    if(!routerOutletFound) {
      if(productPageFound == 1 && productListFound == 0) {
        since('The `app-product-list` tag hasn\'t replaced the `app-product-page` tag yet.').expect(productListFound).toBe(1);
      } else if(productPageFound == 0 && productListFound == 1) {
        // test should pass
      }
    } else {
      since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageFound).toBe(1);
    }
  }));

});
