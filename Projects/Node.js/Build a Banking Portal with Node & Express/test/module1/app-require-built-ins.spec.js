describe('Require `fs` and `path` built-ins', () => {
  it('should contain requires @app-require-built-ins', () => {
    let fs;
    let path;
    try {
      fs = appModule.__get__('fs');
      path = appModule.__get__('path');
    } catch (err) {
      assert(fs !== undefined, 'Has the `fs` built-in module been required in `app.js`?');
      assert(path !== undefined, 'Has the `path` built-in module been required in `app.js`?');
    }
  });
});
