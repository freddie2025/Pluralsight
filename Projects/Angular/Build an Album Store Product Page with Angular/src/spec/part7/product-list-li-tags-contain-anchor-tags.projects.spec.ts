import { TestBed, async, inject } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { Http, BaseRequestOptions, Response, ResponseOptions, RequestOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

import { Observable } from 'rxjs/Observable';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let json = require('../../assets/products.json');

let productListComponentExists = false;
let ProductListComponent;
try {
  ProductListComponent = require('../../app/product-list/product-list.component.ts').ProductListComponent;
  productListComponentExists = true;
} catch (e) {
  productListComponentExists = false;
}

let productServiceExists = false;
let ProductService;
try {
  ProductService = require('../../app/product.service.ts').ProductService;
  productServiceExists = true;
} catch (e) {
  productServiceExists = false;
}

describe('ProductListComponent', () => {

  let product_service;
  let mock_backend;

  beforeEach(async(() => {

    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
      providers: [ProductService, MockBackend, BaseRequestOptions,
        {
          provide: Http,
          useFactory: (mockBackend: MockBackend, defaultOptions: RequestOptions) => {
            return new Http(mockBackend, defaultOptions);
          },
          useClass: Http,
          deps: [MockBackend, BaseRequestOptions]
        }
      ]
    });
  }));

  beforeEach(inject([ProductService, MockBackend], (productService, mockBackend) => {
    product_service = productService;
    mock_backend = mockBackend;
  }));

  it(`should have anchor elements inside of li elements that contain the album name @product-list-li-tags-contain-anchor-tags`, async(() => {
    since('The ProductListComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productListComponentExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductListFixture = TestBed.createComponent(ProductListComponent);
    ProductListFixture.detectChanges();

    since('There aren\'t any list items with anchor tags as children in the ProductListComponent\'s template.').expect(ProductListFixture.nativeElement.querySelectorAll('li a').length).toBe(2);
    if (ProductListFixture.nativeElement.querySelectorAll('li a').length > 0) {
      since('The album name in the first anchor tag of your HTML template doesn\'t match the first album name in the `products` JSON response.').expect(ProductListFixture.nativeElement.querySelectorAll('li a')[0].innerHTML = 'Opacity Zero');
      since('The album name in the second anchor tag of your HTML template doesn\'t match the second album name in the `products` JSON response.').expect(ProductListFixture.nativeElement.querySelectorAll('li a')[1].innerHTML = 'Top, Right, Bottom, Left');
    }

  }));

});
