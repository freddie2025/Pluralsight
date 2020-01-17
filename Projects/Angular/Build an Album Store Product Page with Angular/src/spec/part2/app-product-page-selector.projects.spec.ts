import { TestBed, async } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { AppComponent } from '../../app/app.component';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let productPageComponentExists = false;
let ProductPageComponent;
try {
  ProductPageComponent = require('../../app/product-page/product-page.component.ts').ProductPageComponent;
  productPageComponentExists = true;
} catch (e) {
  productPageComponentExists = false;
}

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
    }).compileComponents();
  }));

  it(`should contain the app-product-page element @app-product-page-selector`, async(() => {
    since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageComponentExists).toBe(true);

    const fixture = TestBed.createComponent(AppComponent);
    const childNodes = fixture.debugElement.childNodes;

    let productPageFound = 0, routerOutletFound = 0, productListFound = 0;
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

    if (!routerOutletFound && !productListFound) {
      since('We couldn\'t find the ProductPageComponent - are you sure you added the right selector to the AppComponent?').expect(productPageFound).toBe(1);
    }
  }));

});
