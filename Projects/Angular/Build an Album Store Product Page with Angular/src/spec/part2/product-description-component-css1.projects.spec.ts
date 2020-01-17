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

  it(`should have CSS that contains a paragraph selector @product-description-component-css1`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);
      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(_.find(parsed.cssRules, {selectorText: 'p'})).not.toBeUndefined();
    }
  }));

  it(`should have CSS with a rule setting the font-size to 16px on the paragraph selector @product-description-component-css1`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);

      let pRule = _.find(parsed.cssRules, { selectorText: 'p' })

      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule).not.toBeUndefined();
      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule.style.parentRule.selectorText).toBe('p');
      since('Your paragraph selector doesn\'t have a `font-size` property that\'s equal to `16px`.').expect(pRule.style['font-size']).toBe('16px');
    }
  }));

  it(`should have CSS with a rule setting the font-family to Helvetica, Arial, sans-serif on the paragraph selector @product-description-component-css1`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);

      let pRule = _.find(parsed.cssRules, { selectorText: 'p' })

      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule).not.toBeUndefined();
      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule.style.parentRule.selectorText).toBe('p');
      let fontRule;
      if (pRule.style['font'] != undefined) {
        fontRule = pRule.style['font'];
      } else if (pRule.style['font-family'] != undefined) {
        fontRule = pRule.style['font-family'];
      } else {
        since('Your paragraph selector doesn\'t have a `font-family` property.').expect(0).toBe(1);
      }

      if (fontRule != undefined) {
        let split = fontRule.split(',');
        for (let i = 0; i < split.length; i++) {
          split[i] = split[i].trim();
        }
        since('Your paragraph selector doesn\'t have a `font-family` property that\'s equal to `Helvetica, Arial, sans-serif`.').expect(split[0]).toBe('Helvetica');
        since('Your paragraph selector doesn\'t have a `font-family` property that\'s equal to `Helvetica, Arial, sans-serif`.').expect(split[1]).toBe('Arial');
        since('Your paragraph selector doesn\'t have a `font-family` property that\'s equal to `Helvetica, Arial, sans-serif`.').expect(split[2]).toBe('sans-serif');        
      }
    }
  }));

  it(`should have CSS with a rule setting the font-weight to normal on the paragraph selector @product-description-component-css1`, async(() => {
    since('The ProductDescriptionComponent hasn\'t been created yet.').expect(productDescriptionCssFileExists).toBe(true);
    if(productDescriptionCssFileExists) {
      let parsed = CSSOM.parse(productDescriptionCssFile);

      let pRule = _.find(parsed.cssRules, { selectorText: 'p' })

      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule).not.toBeUndefined();
      since('There isn\'t a paragraph selector in the ProductDescriptionComponent\'s CSS file right now.').expect(pRule.style.parentRule.selectorText).toBe('p');
      since('Your paragraph selector doesn\'t have a `font-weight` property that\'s equal to `normal`.').expect(pRule.style['font-weight']).toBe('normal');
    }
  }));

});
