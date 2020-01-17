const proxyquire = require('proxyquire');

describe('Server created with app.listen', () => {
  let listenStub;
  before(() => {
    listenStub = sinon.stub().returns({});
    proxyquire('../../src/app', {
      express: sinon.stub().returns({
        get: sinon.spy(),
        post: sinon.spy(),
        set: sinon.spy(),
        use: sinon.spy(),
        listen: listenStub
      })
    });
  });

  it('should contain app.listen @app-listen-console-log', () => {
    assert(listenStub.calledOnce, '`app.listen` has not been called.');
    assert(
      listenStub.calledWithExactly(3000, sinon.match.func),
      '`app.listen` was not called with the correct arguments.'
    );
  });
});
