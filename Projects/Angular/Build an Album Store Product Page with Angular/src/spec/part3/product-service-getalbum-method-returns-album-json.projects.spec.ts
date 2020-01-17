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

class AProductService {
  getAlbum() {

  }
}

describe('ProductService', () => {

  let product_service;
  let ProvidedService;
  let mock_backend;

  if(productServiceExists) {
    ProvidedService = ProductService
  } else {
    ProvidedService = AProductService;
  }

  beforeEach(async(() => {
  
    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule.withRoutes([])],
      providers: [ProvidedService, MockBackend, BaseRequestOptions,
        {
          provide: Http,
          useFactory: (mockBackend: MockBackend, defaultOptions: RequestOptions) => {
            return new Http(mockBackend, defaultOptions);
          },
          deps: [MockBackend, BaseRequestOptions]
        }
      ]
    }).compileComponents();
  }));

  beforeEach(inject([ProvidedService, MockBackend], (providedService, mockBackend) => {
    product_service = providedService;
    mock_backend = mockBackend;
  }));

  it(`should return contents of _albumUrl when getAlbum method called @product-service-getalbum-method-returns-album-json`, async(() => {
    since('The ProductService hasn\'t been created yet.').expect(productServiceExists).toBe(true);
    mock_backend.connections.subscribe((connection: MockConnection) => {
      since('It looks like the `getAlbum` method is not requesting the contents of the `album.json` file.').expect(connection.request.url).toEqual('../assets/album.json');
      since('It looks like the `getAlbum` method is not sending a `GET` request.').expect(connection.request.method).toEqual(0);
      let options = new ResponseOptions({});
      connection.mockRespond(new Response(options));
    });
    if(product_service.getAlbum == undefined) {
      since('The ProductService doesn\'t have a method named `getAlbum()` yet.').expect(0).toBe(1);
    } else if(product_service.getAlbum != undefined && product_service.getAlbum.subscribe == undefined) {
      let ps = product_service.getAlbum(null);
      since('It doesn\'t look like you\'re returning the result of calling `this._http.get()` and passing `this._albumUrl` as a parameter.').expect(product_service._http._backend.connectionsArray.length).toBeGreaterThan(0);
      since('It doesn\'t look like you\'re returning the result of calling `this._http.get()` and passing `this._albumUrl` as a parameter.').expect(product_service._http._backend.connectionsArray[0].request.url).toBe('../assets/album.json');        
    } else {
    }
  }));
});
