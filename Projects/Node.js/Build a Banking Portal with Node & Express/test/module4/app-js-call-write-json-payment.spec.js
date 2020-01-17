describe('Payment post route writeJSON', () => {
  it('should include writeJSON @app-js-call-write-json-payment', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    const stack = routeStack('/payment', 'post') || routeStack('/services/payment', 'post');
    assert(typeof stack !== 'undefined', 'Payment post route may not exist yet.');
    assert(/writeJSON/.test(stack.handle.toString()), 'The payment post function does not include a call to `writeJSON`.');
  });
});
