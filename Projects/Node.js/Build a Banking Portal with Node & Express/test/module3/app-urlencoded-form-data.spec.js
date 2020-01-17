describe('`urlencoded` added', () => {
  it('should add `urlencoded` @app-urlencoded-form-data', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    assert(typeof app._router !== 'undefined', 'No routes have been created.');
    assert(app._router.stack.some(layer => layer.name === 'urlencodedParser'), '`urlencoded` is not being used.');
  });
});
