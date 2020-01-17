import { TestBed, async } from '@angular/core/testing';

const CSSOM = require('cssom');
const _ = require('lodash');

let productTracklistingCssFileExists = false;
let productTracklistingCssFile;
try {
  productTracklistingCssFile = require('../../app/product-tracklisting/product-tracklisting.component.css');
  productTracklistingCssFileExists = true;
} catch (e) {
  productTracklistingCssFileExists = false;
}

describe('ProductTracklisting', () => {

  it(`should have CSS that contains a ul selector @product-tracklisting-component-css2`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);
      since('There isn\'t a `ul` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: 'ul'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the list-style-type to none on the ul selector @product-tracklisting-component-css2`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);

      let ulRule = _.find(parsed.cssRules, { selectorText: 'ul' })

      since('There isn\'t a `ul` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(ulRule).not.toBeUndefined();
      since('There isn\'t a `ul` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(ulRule.style.parentRule.selectorText).toBe('ul');
      since('Your `ul` selector doesn\'t have a `list-style-type` property that\'s equal to `none`.').expect(ulRule.style['list-style-type']).toBe('none');
    }
  }));
  
});
