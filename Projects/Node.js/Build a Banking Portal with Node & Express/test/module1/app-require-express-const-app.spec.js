describe('Require Express and Create `app` const', () => {
  it('require express and create app const @app-require-express-const-app', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let express;
    try {
      express = appModule.__get__('express');
    } catch (err) {
      assert(express !== undefined, 'Has the `express` framework been required in `app.js`?');
    }
  });
});
