const R = require('ramda');
const rewire = require('rewire');

describe('Read user data from `data.js`', () => {
  it('`data.js` should read user data @data-js-transition-const-users', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let users;
    let userData;
    try {
      const dataModule = rewire('../../src/data');
      userData = dataModule.__get__('userData');
      users = dataModule.__get__('users');
    } catch (err) {
      assert(userData !== undefined, 'Has the `userData` variable been created in `data.js`?');
      assert(users !== undefined, 'Has the `users` variable been created in `data.js`?');
    }
    assert(
      !Buffer.isBuffer(userData),
      'It is best if you specify an encoding like "utf8" when reading from a file (readFileSync function).'
    );
    const usersFound = R.allPass([R.has('name'), R.has('username'), R.has('phone'), R.has('email'), R.has('address')]);
    assert(usersFound(users[0]), 'The `users` variable does not contain the correct information.');
  });
});
