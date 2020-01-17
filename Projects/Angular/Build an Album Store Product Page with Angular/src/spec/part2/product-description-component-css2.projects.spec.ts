import { TestBed, async } from '@angular/core/testing';

const CSSOM = require('cssom');
const _ = require('lodash');

let productDescriptionCssFileExists = false;
let productDescriptionCssFile;
try {
  productDescriptionCssFile = require('../../app/product-description/product-description.component.css');
  productDescriptionCssFileExists = true;
} catch (e) {
  productDescriptionCssFileExists = false;
}

describe('ProductDescriptionComponent', () => {

  it(`should have CSS that contains an img selector @product-description-component-css2`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);
      since('There isn\'t an image tag selector in the ProductDescriptionComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: 'img'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the width to 100% on the img selector @product-description-component-css2`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);
      let imgRule = _.find(parsed.cssRules, { selectorText: 'img' })

      since('There isn\'t an image tag selector in the ProductDescriptionComponent\'s CSS file right now.').expect(imgRule).not.toBeUndefined();
      since('There isn\'t an image tag selector in the ProductDescriptionComponent\'s CSS file right now.').expect(imgRule.style.parentRule.selectorText).toBe('img');
      since('Your image tag selector doesn\'t have a `width` property that\'s equal to `100%`.').expect(imgRule.style['width']).toBe('100%');
    }
  }));
  
});
