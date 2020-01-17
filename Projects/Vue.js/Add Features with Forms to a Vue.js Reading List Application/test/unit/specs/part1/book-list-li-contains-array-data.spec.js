import Vue from 'vue';

let BookList;
try {
  BookList = require('../../../../src/components/BookList.vue').default;
} catch (error) {
  BookList = false;
}

const assert = require('chai').assert;

// test for booklist import success
assert(typeof BookList === 'object', 'BookList.vue does not exist in the components folder');

describe('BookList.vue', () => {
  it('should contain a list item with an author from the array @book-list-li-renders-author', () => {
    const Constructor = Vue.extend(BookList);
    const vm = new Constructor().$mount();
    assert(vm.$el.querySelector('ul'), 'No ul was found in BookList.vue');
    assert(vm.$el.querySelector('ul').textContent.includes('Ralph Waldo Emerson'), 'The correct content was not found inside of the li');
  });
});
