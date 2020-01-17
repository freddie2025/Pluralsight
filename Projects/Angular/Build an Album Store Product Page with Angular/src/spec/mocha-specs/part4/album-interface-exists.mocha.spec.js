let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Album Interface', function() {
  it(`should exist @album-interface-exists`, function () {
    assert(fs.existsSync(__dirname + '/../../../app/album.ts'), "The Album interface hasn't been created yet.");

    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../../app/album.ts').toString();
    } catch (e) {
      assert(false, "The Album interface hasn't been created yet.")
    }
    
    let re = /export\s+interface\s+Album/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "There's an `album.ts` file, but it doesn't export an interface named `Album`.");
  });
});