let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Track Interface', function () {
  it(`should have trackNumber property of type number @track-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/track.ts').toString();
    } catch (e) {
      assert(false, "The Track interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Track\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's a `track.ts` file, but it doesn't export an interface named `Track`.");
    
    let arr = match[1].split('\n');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    let properties = [];    
    for (let i = 0; i < arr.length; i++) {
      if (arr[i].trim().length > 0) {
        let obj = {};
        obj['key'] = arr[i].trim().split(':')[0].trim();
        obj['value'] = arr[i].trim().split(':')[1].trim();
        properties[i] = obj;
      }
    }
    
    let trackNumberKeyFound = false
      , trackNumberValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'trackNumber') {
        trackNumberKeyFound = true;
        if (properties[i].value.match(new RegExp(/number/))) {
          trackNumberValueFound = true;
        }
      }
    }
    assert(trackNumberKeyFound, "The Track Interface doesn't define a property named `trackNumber`.");
    assert(trackNumberValueFound, "The Track Interface's `trackNumber` property isn't typed as `number`.");
  });

  it(`should have trackName property of type string @track-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/track.ts').toString();
    } catch (e) {
      assert(false, "The Track interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Track\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's a `track.ts` file, but it doesn't export an interface named `Track`.");
    
    let arr = match[1].split('\n');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    let properties = [];    
    for (let i = 0; i < arr.length; i++) {
      if (arr[i].trim().length > 0) {
        let obj = {};
        obj['key'] = arr[i].trim().split(':')[0].trim();
        obj['value'] = arr[i].trim().split(':')[1].trim();
        properties[i] = obj;
      }
    }
    
    let trackNameKeyFound = false
      , trackNameValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'trackName') {
        trackNameKeyFound = true;
        if (properties[i].value.match(new RegExp(/string/))) {
          trackNameValueFound = true;
        }
      }
    }
    assert(trackNameKeyFound, "The Track Interface doesn't define a property named `trackName`.");
    assert(trackNameValueFound, "The Track Interface's `trackName` property isn't typed as `string`.");
  });

  it(`should have trackLength property of type string @track-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/track.ts').toString();
    } catch (e) {
      assert(false, "The Track interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Track\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's a `track.ts` file, but it doesn't export an interface named `Track`.");
    
    let arr = match[1].split('\n');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    let properties = [];    
    for (let i = 0; i < arr.length; i++) {
      if (arr[i].trim().length > 0) {
        let obj = {};
        obj['key'] = arr[i].trim().split(':')[0].trim();
        obj['value'] = arr[i].trim().split(':')[1].trim();
        properties[i] = obj;
      }
    }
    
    let trackLengthKeyFound = false
      , trackLengthValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'trackLength') {
        trackLengthKeyFound = true;
        if (properties[i].value.match(new RegExp(/string/))) {
          trackLengthValueFound = true;
        }
      }
    }
    assert(trackLengthKeyFound, "The Track Interface doesn't define a property named `trackLength`.");
    assert(trackLengthValueFound, "The Track Interface's `trackLength` property isn't typed as `string`.");
  });
  it(`should have trackPrice property of type number @track-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/track.ts').toString();
    } catch (e) {
      assert(false, "The Track interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Track\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's a `track.ts` file, but it doesn't export an interface named `Track`.");
    
    let arr = match[1].split('\n');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    let properties = [];    
    for (let i = 0; i < arr.length; i++) {
      if (arr[i].trim().length > 0) {
        let obj = {};
        obj['key'] = arr[i].trim().split(':')[0].trim();
        obj['value'] = arr[i].trim().split(':')[1].trim();
        properties[i] = obj;
      }
    }
    
    let trackPriceKeyFound = false
      , trackPriceValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'trackPrice') {
        trackPriceKeyFound = true;
        if (properties[i].value.match(new RegExp(/number/))) {
          trackPriceValueFound = true;
        }
      }
    }
    assert(trackPriceKeyFound, "The Track Interface doesn't define a property named `trackPrice`.");
    assert(trackPriceValueFound, "The Track Interface's `trackPrice` property isn't typed as `number`.");
  });
});