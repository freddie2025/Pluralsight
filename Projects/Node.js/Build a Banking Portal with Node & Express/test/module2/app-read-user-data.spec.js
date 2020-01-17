const fs = require('fs');
const path = require('path');
const rewire = require('rewire');
const R = require('ramda');

describe('Read user data', () => {
  it('should read user data @app-read-user-data', () => {
    assert(typeof app === 'function', '`app` const has not been created in `app.js`.');
    let users;
    let userData;
    try {
      if (fs.existsSync(path.join(process.cwd(), 'src/data.js'))) {
        const dataModule = rewire(path.join(process.cwd(), 'src/data.js'));
        userData = dataModule.__get__('userData');
        users = dataModule.__get__('users');
      } else {
        userData = appModule.__get__('userData');
        users = appModule.__get__('users');
      }
    } catch (err) {
      assert(userData !== undefined, 'Has the `userData` variable been created in `app.js`?');
      assert(users !== undefined, 'Has the `users` variable been created in `app.js`?');
    }
    assert(
      !Buffer.isBuffer(userData),
      'It is best if you specify an encoding like "utf8" when reading from a file (readFileSync function).'
    );
    assert(typeof users === 'object', 'The users variable does not contain the correct information.');
    const usersFound = R.allPass([R.has('name'), R.has('username'), R.has('phone'), R.has('email'), R.has('address')]);
    assert(usersFound(users[0]), 'The users variable does not contain the correct information.');
  });
});
