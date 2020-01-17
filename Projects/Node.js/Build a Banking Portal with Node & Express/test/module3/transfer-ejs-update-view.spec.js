const fs = require('fs');
const path = require('path');
const ejs = require('ejs');
const cheerio = require('cheerio');

describe('Update `transfer` view', () => {
  it('should update the `transfer` view @transfer-ejs-update-view', () => {
    let file;
    let $;
    try {
      file = fs.readFileSync(path.join(process.cwd(), 'src/views/transfer.ejs'), 'utf8');
      ejs.compile(file);
      $ = cheerio.load(file);
      assert(
        $('#transferForm')
          .attr('method')
          .toLowerCase() === 'post',
        'The form is missing a `method` attribute.'
      );
    } catch (err) {
      assert(err.message.indexOf('compiling ejs') < -1, `Error compiling transfer.ejs`);
    }
    assert(
      $('#transferForm').attr('action') === '/services/transfer' || $('#transferForm').attr('action') === '/transfer',
      'The form is missing an `action` attribute.'
    );
    assert(
      $('select')
        .first()
        .attr('id') === 'from',
      'The first `select` is missing an `id` attribute or it has the wrong value.'
    );
    assert(
      $('select')
        .first()
        .attr('name') === 'from',
      'The first `select` is missing a `name` attribute or it has the wrong value.'
    );
    assert(
      $('select')
        .last()
        .attr('id') === 'to',
      'The second `select` is missing an `id` attribute or it has the wrong value.'
    );
    assert(
      $('select')
        .last()
        .attr('name') === 'to',
      'The second `select` is missing a `name` attribute or it has the wrong value.'
    );
  });
});
