const R = require('ramda');

describe('Updated Index Route', () => {
  let spy;
  before(() => {
    if (typeof app === 'undefined') {
      spy = {
        restore: () => { }
      };
    } else {
      spy = sinon.spy(app, 'render');
    }
  });

  it('should contain the index route with accounts @app-update-index-route', done => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    request(app)
      .get('/')
      .expect(() => {
        assert(spy.called, 'The index route may have not been created.');
        assert(spy.firstCall.args[0] === 'index', 'The index route does not seem to be rendering the `index` view.');
        assert(
          R.propEq('title', 'Account Summary')(spy.firstCall.args[1]),
          'The index route object `title` key value pair was not updated.'
        );
        const accountsObjectFound = R.allPass([R.has('savings'), R.has('checking'), R.has('credit')]);
        assert(
          accountsObjectFound(spy.firstCall.args[1].accounts),
          'The index route object may be missing an `accounts: accounts"` key value pair.'
        );
      })
      .end(done);
  });

  after(() => {
    spy.restore();
  });
});
