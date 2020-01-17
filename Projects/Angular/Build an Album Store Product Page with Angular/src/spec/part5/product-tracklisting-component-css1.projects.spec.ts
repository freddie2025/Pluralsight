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

  it(`should have CSS that contains a .tracklisting selector @product-tracklisting-component-css1`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);
      since('There isn\'t a `.tracklisting` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: '.tracklisting'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the font-size to 16px and the padding-top to 10px on the .tracklisting selector @product-tracklisting-component-css1`, async(() => {
    since('The ProductTracklistingComponent hasn\'t been created yet.').expect(productTracklistingCssFileExists).toBe(true);
    if(productTracklistingCssFileExists) {
      let parsed = CSSOM.parse(productTracklistingCssFile);

      let tRule = _.find(parsed.cssRules, { selectorText: '.tracklisting' })

      since('There isn\'t a `.tracklisting` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(tRule).not.toBeUndefined();
      since('There isn\'t a `.tracklisting` selector in the ProductTracklistingComponent\'s CSS file right now.').expect(tRule.style.parentRule.selectorText).toBe('.tracklisting');
      since('Your `.tracklisting` selector doesn\'t have a `font-size` property that\'s equal to `16px`.').expect(tRule.style['font-size']).toBe('16px');
      if (tRule.style['padding-top']) {
        since('Your `.tracklisting` selector isn\'t setting the top padding to be `10px`.').expect(tRule.style['padding-top']).toBe('10px');
      } else if (tRule.style['padding']) {
        let padding = tRule.style['padding'];
        since('Your `.tracklisting` selector isn\'t setting the top padding to be `10px`.').expect(tRule.style['padding']).toBe('10px 0 0 0');
      } else {
        since('It doesn\'t look like you\'re setting the padding property in your `.tracklisting` selector.').expect(1).toBe(0);
      }
    }
  }));
  
});
