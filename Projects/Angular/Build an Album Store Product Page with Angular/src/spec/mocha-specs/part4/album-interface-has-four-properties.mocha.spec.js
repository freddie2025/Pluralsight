let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Album Interface', function () {
  it(`should have name property of type string @album-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Album\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's an `album.ts` file, but it doesn't export an interface named `Album`.");
    
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
    
    let nameKeyFound = false
      , nameValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'name') {
        nameKeyFound = true;
        if (properties[i].value.match(new RegExp(/string/))) {
          nameValueFound = true;
        }
      }
    }
    assert(nameKeyFound, "The Album Interface doesn't define a property named `name`.");
    assert(nameValueFound, "The Album Interface's `name` property isn't typed as `string`.");
  });

  it(`should have releaseDate property of type string @album-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Album\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's an `album.ts` file, but it doesn't export an interface named `Album`.");

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
    
    let releaseDateKeyFound = false
      , releaseDateValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'releaseDate') {
        releaseDateKeyFound = true;
        if (properties[i].value.match(new RegExp(/string/))) {
          releaseDateValueFound = true;
        }
      }
    }
    assert(releaseDateKeyFound, "The Album Interface doesn't define a property named `releaseDate`.");
    assert(releaseDateValueFound, "The Album Interface's `releaseDate` property isn't typed as `string`.");
  });

  it(`should have coverImage property of type string @album-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Album\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's an `album.ts` file, but it doesn't export an interface named `Album`.");

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
    
    let coverImageKeyFound = false
      , coverImageValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'coverImage') {
        coverImageKeyFound = true;
        if (properties[i].value.match(new RegExp(/string/))) {
          coverImageValueFound = true;
        }
      }
    }
    assert(coverImageKeyFound, "The Album Interface doesn't define a property named `coverImage`.");
    assert(coverImageValueFound, "The Album Interface's `coverImage` property isn't typed as `string`.");
  });
  it(`should have tracks property of type Track[] @album-interface-has-four-properties`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Album\s*\{\s*([\w\s\:\;\[\]]+)\s*\}/
    let match = file.match(re);
    assert(Array.isArray(match) && match != null, "There's an `album.ts` file, but it doesn't export an interface named `Album`.");

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
    
    let tracksKeyFound = false
      , tracksValueFound = false
    for (let i = 0; i < properties.length; i++) {
      if (properties[i].key == 'tracks') {
        tracksKeyFound = true;
        if (properties[i].value.match(new RegExp(/Track\[\]/))) {
          tracksValueFound = true;
        }
      }
    }
    assert(tracksKeyFound, "The Album Interface doesn't define a property named `tracks`.");
    assert(tracksValueFound, "The Album Interface's `tracks` property isn't typed as `Track[]`.");
  });
});