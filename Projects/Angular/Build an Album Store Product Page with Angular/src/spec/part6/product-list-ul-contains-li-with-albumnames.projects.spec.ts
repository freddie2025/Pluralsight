import { TestBed, async, inject } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { Http, BaseRequestOptions, Response, ResponseOptions, RequestOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

import { Observable } from 'rxjs/Observable';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let json = require('../../assets/products.json');

let html;
try {
  html = require('../../app/product-list/product-list.component.html');
} catch (e) { }

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

let findComments = function(el) {
    var arr = [];
    for(var i = 0; i < el.childNodes.length; i++) {
        var node = el.childNodes[i];
        if(node.nodeType === 8) {
            arr.push(node);
        } else {
            arr.push.apply(arr, findComments(node));
        }
    }
    return arr;
};


describe('ProductList', () => {

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

  it(`should have an li element that contains the album name @product-list-ul-contains-li-with-albumnames`, async(() => {
    since('The ProductListComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productListComponentExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductListFixture = TestBed.createComponent(ProductListComponent);
    ProductListFixture.detectChanges();

    since('The ProductListComponent doesn\'t have an unordered list with multiple list items.  Have you tried adding the `ngFor` directive to the `li` tag in the template yet?').expect(ProductListFixture.nativeElement.querySelectorAll('ul li').length).toBeGreaterThan(1);
    
    since('The album names in your HTML template don\'t match the album names in the `products` JSON response.').expect(ProductListFixture.nativeElement.querySelectorAll('ul li')[0].innerHTML).toContain('Opacity Zero');
    since('The album names in your HTML template don\'t match the album names in the `products` JSON response.').expect(ProductListFixture.nativeElement.querySelectorAll('ul li')[1].innerHTML).toContain('Top, Right, Bottom, Left');
  }));

});
