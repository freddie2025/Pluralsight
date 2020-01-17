import { TestBed, async, inject } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { BrowserModule } from '@angular/platform-browser';

import { Http, BaseRequestOptions, Response, ResponseOptions, RequestOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let productServiceExists = false;
let ProductService;
try {
  ProductService = require('../../app/product.service.ts').ProductService;
  productServiceExists = true;
} catch (e) {
  productServiceExists = false;
}

describe('ProductService', () => {

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
          deps: [MockBackend, BaseRequestOptions]
        }
      ]
    });
  }));

  beforeEach(inject([ProductService, MockBackend], (productService, mockBackend) => {
    product_service = productService;
    mock_backend = mockBackend;
  }));

  it(`should return contents of _productsUrl when getProducts() method called @product-service-getproducts-method-returns-products-json`, async(() => {
    mock_backend.connections.subscribe((connection: MockConnection) => {
      since('It looks like the `getProducts` method is not requesting the contents of the `products.json` file.').expect(connection.request.url).toEqual('../assets/products.json');
      since('It looks like the `getProducts` method is not sending a `GET` request.').expect(connection.request.method).toEqual(0);
      let options = new ResponseOptions({});
      connection.mockRespond(new Response(options));
    });
    if(product_service.getProducts == undefined) {
      since('The ProductService doesn\'t have a method named `getProducts` yet.').expect(0).toBe(1);
    } else if(product_service.getProducts != undefined && product_service.getAlbum.subscribe == undefined) {
      let ps = product_service.getProducts(null);
      since('It doesn\'t look like you\'re returning the result of calling `this._http.get()` and passing `this._productsUrl` as a parameter.').expect(product_service._http._backend.connectionsArray.length).toBeGreaterThan(0);
      since('It doesn\'t look like you\'re returning the result of calling `this._http.get()` and passing `this._productsUrl` as a parameter.').expect(product_service._http._backend.connectionsArray[0].request.url).toBe('../assets/products.json');        
    } else {
    }
  }));
});
