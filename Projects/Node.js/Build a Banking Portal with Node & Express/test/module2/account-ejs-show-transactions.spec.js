const fs = require('fs');
const path = require('path');
const ejs = require('ejs');

describe('Account Transactions', () => {
  it('should display account transactions @account-ejs-show-transactions', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/views/account.ejs'), 'utf8');
      ejs.compile(file);
    } catch (err) {
      assert(err.code !== 'ENOENT', 'The `account.ejs` view file does not exist.');
      const errorMessage = err.message.substring(0, err.message.indexOf('compiling ejs') - 1);
      assert(err.message.indexOf('compiling ejs') < -1, `${errorMessage} compiling account.ejs`);
    }
    assert(
      /<%-\s+include\(('|")transactions(\.ejs)?('|")\s*,\s*{\s*account:\s*account\s*}\s*\)(;)?\s*%>/.test(file),
      'Have you included the `transactions` view in `account.ejs`?'
    );
  });
});
