let fs = require('fs');
let expect = require('chai').expect
let assert = require('chai').assert

describe('Track Interface', function() {
  it(`should exist @track-interface-exists`, function () {
    assert(fs.existsSync(__dirname + '/../../../app/track.ts'), "The Track interface hasn't been created yet.");

    let file
    try {
      file = fs.readFileSync(__dirname + '/../../../app/track.ts').toString();
    } catch (e) {
      assert(false, "The Track interface doesn't exist yet.")
    }
    let re = /export\s+interface\s+Track/
    assert(Array.isArray(file.match(re)) && file.match(re) != null, "There's a `track.ts` file, but it doesn't export an interface named `Track`.");
  });
});