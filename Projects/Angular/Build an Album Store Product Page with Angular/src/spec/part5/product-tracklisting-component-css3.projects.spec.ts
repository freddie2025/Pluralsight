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

  it(`should have CSS that contains an li selector @product-tracklisting-component-css3`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);
      since('There isn\'t an `li` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: 'li'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the display to block and the line-height to 30px on the li selector @product-tracklisting-component-css3`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);

      let liRule = _.find(parsed.cssRules, { selectorText: 'li' })

      since('There isn\'t an `li` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(liRule).not.toBeUndefined();
      since('There isn\'t an `li` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(liRule.style.parentRule.selectorText).toBe('li');
      since('Your `li` selector doesn\'t have a `display` property that\'s equal to `block`.').expect(liRule.style['display']).toBe('block');
      since('Your `li` selector doesn\'t have a `line-height` property that\'s equal to `30px`.').expect(liRule.style['line-height']).toBe('30px');
    }
  }));
  
});
