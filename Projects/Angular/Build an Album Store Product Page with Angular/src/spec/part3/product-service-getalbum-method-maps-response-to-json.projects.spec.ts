import { TestBed, async, inject } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { Http, BaseRequestOptions, Response, ResponseOptions, RequestOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

import { Observable } from 'rxjs/Observable';

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

let json = require('../../assets/album.json');

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

  it(`should map the result of get request to json with rxjs map function @product-service-getalbum-method-maps-response-to-json`, async(() => {
    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });
    if (product_service.getAlbum == undefined) {
      since('The ProductService doesn\'t have a method named `getAlbum()` yet.').expect(0).toBe(1);
    } else if (product_service.getAlbum != undefined && product_service.getAlbum(1) == undefined) {
      since('The `getAlbum()` method exists, but it\'s not returning the result of a call to `this._http.get()` and passing `this._albumUrl` as a parameter.').expect(0).toBe(1);
    } else {
      product_service.getAlbum(null).subscribe((response) => {
        since('It looks like you\'re not returning the getAlbum method\'s response as JSON.').expect(response._body).toBeUndefined();
        since('Your `getAlbum` method is returning a JSON response, but not the correct JSON.  Are you sure your service class is setup correctly?').expect(response.id).toEqual(1);
        since('Your `getAlbum` method is returning a JSON response, but not the correct JSON.  Are you sure your service class is setup correctly?').expect(response.artist).toEqual('The Prependers');
        since('Your `getAlbum` method is returning a JSON response, but not the correct JSON.  Are you sure your service class is setup correctly?').expect(response.album.name).toEqual('Opacity Zero');
      }
      );
    }
  }));
});
