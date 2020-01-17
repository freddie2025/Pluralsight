describe('Require accounts routes', () => {
  it('require express and create app const @app-require-account-routes', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let accountRoutes;
    try {
      accountRoutes = appModule.__get__('accountRoutes');
    } catch (err) {
      assert(accountRoutes !== undefined, 'Has the `accountRoutes` const been created `app.js`?');
    }
    assert(typeof accountRoutes === 'function', 'Has the router been exported in `src/routes/accounts.js`?');
  });
});
