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

  it(`should have CSS that contains a button selector @product-tracklisting-component-css4`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);
      since('There isn\'t a `button` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: 'button'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the line-height to 1 on the button selector @product-tracklisting-component-css4`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);

      let buttonRule = _.find(parsed.cssRules, { selectorText: 'button' })

      since('There isn\'t a `button` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(buttonRule).not.toBeUndefined();
      since('There isn\'t a `button` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(buttonRule.style.parentRule.selectorText).toBe('button');
      since('Your `button` selector doesn\'t have a `line-height` property that\'s equal to `1`.').expect(buttonRule.style['line-height']).toBe('1');
    }
  }));
  
});
