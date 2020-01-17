const fs = require('fs');
const path = require('path');
const ejs = require('ejs');

describe('Create `index` view', () => {
  it('should create the index view @index-ejs-create-view', () => {
    let file;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/views/index.ejs'), 'utf8');
      ejs.compile(file);
    } catch (err) {
      assert(err.code !== 'ENOENT', 'The `index.ejs` view file does not exist.');
      const errorMessage = err.message.substring(0, err.message.indexOf('compiling ejs'));
      assert(err.message.indexOf('compiling ejs') < -1, ` ${errorMessage}Error compiling index.ejs`);
    }
    assert(/<%-\s+include\(('|")header(\.ejs)?('|")\)(;)?\s*%>/.test(file), 'Have you included the `header` view?');
    assert(/<div\s+class\s*=\s*("|'|\s*)container(\s*|"|')>/.test(file), 'The `div` with a class of `container` can not be found.');
    assert(/<%=\s*title\s*%>/.test(file), 'The `title` variable seems to be missing.');
    assert(/<a\s+href=('|")?\/profile('|")?>\s*(P|p)rofile\s*<\/a>/.test(file), 'The `profile` link seems to be missing.');
    assert(/<a\s+href=('|")?(\/services)?\/transfer('|")?>\s*(T|t)ransfer\s*<\/a>/.test(file), 'The `transfer` link seems to be missing.');
    assert(/<%-\s+include\(('|")footer(\.ejs)?('|")\)(;)?\s*%>/.test(file), 'Have you included the `footer` view?');
  });
});
