const fs = require('fs');
const path = require('path');
const ejs = require('ejs');
const cheerio = require('cheerio');

describe('Update views', () => {
  it('should update all views @views-update-for-routes', () => {
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/index.ejs')), 'The `src/views/index.ejs` file does not exist.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/summary.ejs')), 'The `src/views/summary.ejs` file does not exist.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/transfer.ejs')), 'The `src/views/transfer.ejs` file does not exist.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/payment.ejs')), 'The `src/views/payment.ejs` file does not exist.');
    assert(fs.existsSync(path.join(process.cwd(), 'src/views/account.ejs')), 'The `src/views/account.ejs` file does not exist.');

    let indexFile;
    let summaryFile;
    let transferFile;
    let paymentFile;
    let accountFile;
    let $index;
    let $summary;
    let $transfer;
    let $payment;
    let $account;
    try {
      indexFile = fs.readFileSync(path.join(process.cwd(), 'src/views/index.ejs'), 'utf8');
      summaryFile = fs.readFileSync(path.join(process.cwd(), 'src/views/summary.ejs'), 'utf8');
      transferFile = fs.readFileSync(path.join(process.cwd(), 'src/views/transfer.ejs'), 'utf8');
      paymentFile = fs.readFileSync(path.join(process.cwd(), 'src/views/payment.ejs'), 'utf8');
      accountFile = fs.readFileSync(path.join(process.cwd(), 'src/views/account.ejs'), 'utf8');

      ejs.compile(indexFile);
      ejs.compile(summaryFile);
      ejs.compile(transferFile);
      ejs.compile(paymentFile);
      ejs.compile(accountFile);

      $index = cheerio.load(indexFile);
      $summary = cheerio.load(summaryFile);
      $transfer = cheerio.load(transferFile);
      $payment = cheerio.load(paymentFile);
      $account = cheerio.load(accountFile);
    } catch (err) {
      const errorMessage = err.message.substring(0, err.message.indexOf('compiling ejs') - 1);
      assert(err.message.indexOf('compiling ejs') < -1, `${errorMessage} compiling index.ejs`);
    }

    assert(typeof $index('a')['1'] !== 'undefined', 'The transfer link in `index.ejs` is missing.');
    assert($index('a')['1'].attribs.href === '/services/transfer', 'The transfer link in `index.ejs` has not been updated.');
    assert($summary('a').attr('href') === '/account/<%= account.unique_name %>', 'The account link in `summary.ejs` link has not been updated.');
    assert($transfer('#transferForm').attr('action') === '/services/transfer', 'The transfer form action attribute has not been updated.');
    assert($payment('#paymentForm').attr('action') === '/services/payment', 'The payment form action attribute has not been updated.');
    assert(typeof $index('a')['0'] !== 'undefined', 'The payment link has not been updated.');
    assert($account('a')['0'].attribs.href === '/services/payment', 'The payment link in `account.ejs` has not been updated.');
  });
});
