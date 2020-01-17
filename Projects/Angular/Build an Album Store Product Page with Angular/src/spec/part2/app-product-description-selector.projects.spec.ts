import { TestBed, async } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { AppComponent } from '../../app/app.component';

import { BrowserModule } from '@angular/platform-browser';

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

let productDescriptionComponentExists = false;
let ProductDescriptionComponent;
try {
  ProductDescriptionComponent = require('../../app/product-description/product-description.component.ts').ProductDescriptionComponent;
  productDescriptionComponentExists = true;
} catch (e) {
  productDescriptionComponentExists = false;
}

describe('ProductPageComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
    }).compileComponents();
  }));

  it(`should contain the app-product-description element @app-product-description-selector`, async(() => {
    since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageComponentExists).toBe(true);
    since('The ProductDescriptionComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productDescriptionComponentExists).toBe(true);

    const ProductPageFixture = TestBed.createComponent(ProductPageComponent);
    ProductPageFixture.detectChanges();

    since('You haven\'t added the `app-product-description` selector yet.').expect(ProductPageFixture.nativeElement.querySelector('app-product-description')).not.toBe(null);
  }));

  it(`should contain the app-product-description element as a child of the first element with a class of row @app-product-description-selector`, async(() => {
    since('The ProductPageComponent doesn\'t exist for some reason.').expect(productPageComponentExists).toBe(true);
    since('The ProductDescriptionComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productDescriptionComponentExists).toBe(true);

    const ProductPageFixture = TestBed.createComponent(ProductPageComponent);
    ProductPageFixture.detectChanges();
    
    if (ProductPageFixture.nativeElement.querySelector('div.row').querySelector('app-product-description')) {
      since('You haven\'t added the `app-product-description` selector in the right spot in the ProductPageComponent.').expect(ProductPageFixture.nativeElement.querySelector('div.row').querySelector('app-product-description').nodeName).toBe('APP-PRODUCT-DESCRIPTION');
    } else {
      since('You haven\'t added the `app-product-description` selector yet.').expect(0).toBe(1);
    }
  }));

});
