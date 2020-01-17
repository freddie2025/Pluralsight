import Vue from 'vue';

Vue.config.productionTip = false;

const args = __karma__.config.args;
const part = args[0].part ? args[0].part : '';

const testsContext = require.context('./specs/', true, /\.spec$/);
testsContext.keys().filter(key => key.includes(part)).forEach(testsContext);

// require all src files except main.js for coverage.
// you can also change this to match only the subset of files that
// you want coverage for.
const srcContext = require.context('../../src', true, /^\.\/(?!main(\.js)?$)/);
srcContext.keys().forEach(srcContext);
