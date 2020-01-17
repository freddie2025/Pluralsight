import { TestBed, async } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let productDescriptionComponentExists = false;
let ProductDescriptionComponent;
try {
  ProductDescriptionComponent = require('../../app/product-description/product-description.component.ts').ProductDescriptionComponent;
  productDescriptionComponentExists = true;
} catch (e) {
  productDescriptionComponentExists = false;
}

let productPageComponentExists = false;
let ProductPageComponent;
try {
  ProductPageComponent = require('../../app/product-page/product-page.component.ts').ProductPageComponent;
  productPageComponentExists = true;
} catch (e) {
  productPageComponentExists = false;
}

describe('ProductDescription', () => {
  beforeEach(async(() => {

    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
    }).compileComponents();
  }));

  it('should have moved the description div out of the product-page component @product-description-html-moved', async(() => {
    since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageComponentExists).toBe(true);
    since('The ProductDescriptionComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productDescriptionComponentExists).toBe(true);

    const ProductPageFixture = TestBed.createComponent(ProductPageComponent);

    since('It looks like the ProductPageComponent still contains a `div` tag with a class of `description` - have you tried moving it yet?').expect(ProductPageFixture.nativeElement.querySelector('div.row > div.description')).toBeNull();
  }));

  it(`should have moved the description div into the product-description component @product-description-html-moved`, async(() => {
    since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageComponentExists).toBe(true);
    since('The ProductDescriptionComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productDescriptionComponentExists).toBe(true);

    const ProductDescriptionFixture = TestBed.createComponent(ProductDescriptionComponent);

    since('The ProductDescriptionComponent\'s HTML file doesn\'t contain a `div` tag with a class of `description` - have you moved it over from the ProductPageComponent yet?').expect(ProductDescriptionFixture.nativeElement.querySelector('div.description')).not.toBeNull();
  }));
    
});