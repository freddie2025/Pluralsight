import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let quizEndComponentExists = false;
let QuizEnd;
try {
  QuizEnd = require('../../QuizEnd.js').default;
  quizEndComponentExists = true;
} catch (e) {
  quizEndComponentExists = false;
}

let fs = require('fs');

describe('QuizEnd Component', () => {
  it('has a render method that returns the correct HTML  @quiz-end-component-has-render-method', () => {
    assert(quizEndComponentExists, "The QuizEnd component hasn't been created yet.")

    let quizEnd;
    try {
      quizEnd = shallow(<QuizEnd />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizEnd component.")
    }

    let html = quizEnd.html()
    let div = document.createElement('div')
    div.innerHTML = html

    assert(div.querySelector('div') != null, "We can't find a `div` tag in the QuizEnd component's JSX.")
    assert(div.querySelector('div p') != null, "We can't find a `p` tag that's a child of a `div` tag in the QuizEnd component's JSX.")
    assert(div.querySelector('div a') != null, "We can't find an `a` tag that's a child of a `div` tag in the QuizEnd component's JSX.")
    assert(div.querySelector('div p').innerHTML == "Thanks for playing!", "We found a paragraph tag in the QuizEnd component's JSX, but it has the incorrect text value.")
    assert(div.querySelector('div a').innerHTML == "Reset Quiz", "We found an anchor tag in the QuizEnd component's JSX, but it has the incorrect text value.")
    assert(div.querySelector('div a').getAttribute('href') == '', "We found a anchor tag in the QuizEnd component's JSX, but it has the incorrect value for the `href` attribute.")
  })
})