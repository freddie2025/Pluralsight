import { TestBed, async, inject } from '@angular/core/testing';

import { AppModule } from '../../app/app.module';

import { Http, BaseRequestOptions, Response, ResponseOptions, RequestOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

import { Observable } from 'rxjs/Observable';

import { Routes } from '@angular/router';

import { RouterTestingModule } from '@angular/router/testing';

let json = require('../../assets/album.json');

let productTracklistingExists = false;
let ProductTracklistingComponent;
try {
  ProductTracklistingComponent = require('../../app/product-tracklisting/product-tracklisting.component.ts').ProductTracklistingComponent;
  productTracklistingExists = true;
} catch (e) {
  productTracklistingExists = false;
}

let productServiceExists = false;
let ProductService;
try {
  ProductService = require('../../app/product.service.ts').ProductService;
  productServiceExists = true;
} catch (e) {
  productServiceExists = false;
}

describe('ProductTracklisting', () => {

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

  it(`should use track number data from the albumInfo.tracks property in the HTML template @product-tracklisting-html-uses-track-object-data`, async(() => {
    since('The ProductTracklistingComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productTracklistingExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductTracklistingFixture = TestBed.createComponent(ProductTracklistingComponent);
    ProductTracklistingFixture.detectChanges();

    if (ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-number').length > 1) {
      let tracksHtml = ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-number').forEach((element, index) => {
        since('The track number in your HTML template doesn\'t match the track number in the JSON response.').expect(element.innerText).toEqual(json.album.tracks[index].trackNumber.toString());
      });
    } else {
      since('The tracklisting data is not being populated by a JSON response yet.').expect(0).toBe(1);
    }
  }));

  it(`should use track name data from the albumInfo.tracks property in the HTML template @product-tracklisting-html-uses-track-object-data`, async(() => {
    since('The ProductTracklistingComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productTracklistingExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductTracklistingFixture = TestBed.createComponent(ProductTracklistingComponent);
    ProductTracklistingFixture.detectChanges();

    if (ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-number').length > 1) {
      let tracksHtml = ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-name').forEach((element, index) => {
        since('The track name in your HTML template doesn\'t match the track name in the JSON response.').expect(element.innerText).toEqual(json.album.tracks[index].trackName.toString());
      });
    } else {
      since('The tracklisting data is not being populated by a JSON response yet.').expect(0).toBe(1);
    }
}));

  it(`should use track time data from the albumInfo.tracks property in the HTML template @product-tracklisting-html-uses-track-object-data`, async(() => {
    since('The ProductTracklistingComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productTracklistingExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductTracklistingFixture = TestBed.createComponent(ProductTracklistingComponent);
    ProductTracklistingFixture.detectChanges();

    if (ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-number').length > 1) {
      let tracksHtml = ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-time').forEach((element, index) => {
        since('The track time in your HTML template doesn\'t match the track time in the JSON response.').expect(element.innerText).toEqual(json.album.tracks[index].trackLength.toString());
      });
    } else {
      since('The tracklisting data is not being populated by a JSON response yet.').expect(0).toBe(1);
    }
  }));

  it(`should use track price data from the albumInfo.tracks property in the HTML template @product-tracklisting-html-uses-track-object-data`, async(() => {
    since('The ProductTracklistingComponent doesn\'t exist - have you run the `ng` command to generate it yet?').expect(productTracklistingExists).toBe(true);

    mock_backend.connections.subscribe((connection: MockConnection) => {
      let options = new ResponseOptions({
        body: json
      });
      connection.mockRespond(new Response(options));
    });

    const ProductTracklistingFixture = TestBed.createComponent(ProductTracklistingComponent);
    ProductTracklistingFixture.detectChanges();

    if (ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.track-number').length > 1) {
      let tracksHtml = ProductTracklistingFixture.debugElement.nativeElement.querySelectorAll('.price-and-buy button').forEach((element, index) => {
        since('The track price in your HTML template doesn\'t match the track price in the JSON response.').expect(element.innerText).toEqual(json.album.tracks[index].trackPrice.toString());
      });
    } else {
      since('The tracklisting data is not being populated by a JSON response yet.').expect(0).toBe(1);
    }
  }));
  
});
