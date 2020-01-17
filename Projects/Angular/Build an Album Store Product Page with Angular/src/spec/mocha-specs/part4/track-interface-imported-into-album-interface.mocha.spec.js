let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Album Interface', function() {
  it(`should import the Track Interface @track-interface-imported-into-album-interface`, function () {
    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface doesn't exist yet.");
    }
    let re = /import\s*{([\w,\s]+)}\s*from\s*[\'|\"]\.\/track[\'|\"]\;?/
    let match = file.match(re);
    assert(Array.isArray(match), "The Track interface isn't being imported in the Album interface.");

    let arr = match[1].split(',');
    for (let i = 0; i < arr.length; i++) {
      arr[i] = arr[i].trim();
    }

    assert(Array.isArray(arr) && arr.includes('Track'), "The Track interface isn't being imported in the Album interface.");
  });
});