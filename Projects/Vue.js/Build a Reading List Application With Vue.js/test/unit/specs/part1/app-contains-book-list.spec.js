import Vue from 'vue';

let App;
try {
  App = require('../../../../src/App.vue').default;
} catch (error) {
  App = false;
}

const assert = require('chai').assert;

// test for booklist import success
assert(typeof App === 'object', 'App.vue does not exist in the src folder');

describe('App.vue', () => {
  it('should render title in book-list through App component @app-will-render-title-all-books', () => {
    const Constructor = Vue.extend(App);
    const vm = new Constructor().$mount();
    assert(vm.$el.querySelector('h1'), 'No h1 was found in App.vue');
    assert(vm.$el.querySelector('h1').textContent === 'All Books', 'The h1 in App.vue was not set to all books');
  });
});
