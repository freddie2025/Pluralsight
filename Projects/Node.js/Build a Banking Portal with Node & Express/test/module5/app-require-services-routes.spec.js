describe('Require services routes', () => {
  it('require express and create app const @app-require-services-routes', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let servicesRoutes;
    try {
      servicesRoutes = appModule.__get__('servicesRoutes');
    } catch (err) {
      assert(servicesRoutes !== undefined, 'Has the `servicesRoutes` const been created `app.js`?');
    }
    assert(typeof servicesRoutes === 'function', 'Has the router been exported in `src/routes/services.js`?');
  });
});
