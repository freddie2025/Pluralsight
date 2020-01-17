import { TestBed, async } from '@angular/core/testing';

let productDescriptionComponentExists = false;
let ProductDescriptionComponent;
try {
  ProductDescriptionComponent = require('../../app/product-description/product-description.component.ts').ProductDescriptionComponent;
  productDescriptionComponentExists = true;
} catch (e) {
  productDescriptionComponentExists = false;
}

describe('AppComponent', () => {

  it(`should create the product description component @product-description-component-created`, async(() => {
    since('The ProductDescriptionComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productDescriptionComponentExists).toBe(true);
  }));

});
