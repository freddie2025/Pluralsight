const rewire = require('rewire');

describe('Require `fs` and `path` built-ins in data.js', () => {
  it('`data.js` should contain requires @data-js-require-built-ins', () => {
    let fs;
    let path;
    try {
      const dataModule = rewire('../../src/data');
      fs = dataModule.__get__('fs');
      path = dataModule.__get__('path');
    } catch (err) {
      assert(fs !== undefined, 'Has the `fs` built-in module been required in `data.js`?');
      assert(path !== undefined, 'Has the `path` built-in module been required in `data.js`?');
    }
  });
});
