const fs = require('fs');
const path = require('path');
const ejs = require('ejs');

describe('Update `index` view', () => {
  it('should update the index view with account summaries @index-ejs-update-view', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/views/index.ejs'), 'utf8');
      ejs.compile(file);
    } catch (err) {
      assert(err.code !== 'ENOENT', 'The `index.ejs` view file does not exist.');
      const errorMessage = err.message.substring(0, err.message.indexOf('compiling ejs') - 1);
      assert(err.message.indexOf('compiling ejs') < -1, `${errorMessage} compiling index.ejs`);
    }
    assert(/<%-\s+include\(('|")header(\.ejs)?('|")\)(;)?\s*%>/.test(file), 'Have you included the `header` view?');

    assert(
      /<div\s+class\s*=\s*("|'|\s*)container(\s*|"|')>/.test(file),
      'The `div` with a class of `container` can not be found.'
    );
    assert(/<h1>\s*<%=\s*title\s*%>\s*<\/h1>/.test(file), 'The `title` variable seems to be missing.');
    assert(
      /<a\s+href=('|")?\/profile('|")?>\s*(P|p)rofile\s*<\/a>/.test(file),
      'The `profile` link seems to be missing.'
    );
    assert(
      /<%-\s+include\(('|")summary(\.ejs)?('|")\s*,\s*{\s*account:\s*accounts.savings\s*}\s*\)(;)?\s*%>/.test(file),
      'Have you included the `summary` view for the `savings` account?'
    );
    assert(
      /<%-\s+include\(('|")summary(\.ejs)?('|")\s*,\s*{\s*account:\s*accounts.checking\s*}\s*\)(;)?\s*%>/.test(file),
      'Have you included the `summary` view for the `checking` account?'
    );
    assert(
      /<%-\s+include\(('|")summary(\.ejs)?('|")\s*,\s*{\s*account:\s*accounts.credit\s*}\s*\)(;)?\s*%>/.test(file),
      'Have you included the `summary` view for the `credit` account?'
    );
    assert(
      /<a\s+href=('|")?(\/services)?\/transfer('|")?>\s*(T|t)ransfer\s*<\/a>/.test(file),
      'The `transfer` link seems to be missing.'
    );
    assert(/<%-\s+include\(('|")footer(\.ejs)?('|")\)(;)?\s*%>/.test(file), 'Have you included the `footer` view?');
  });
});
