import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let quizQuestionComponentExists = false;
let QuizQuestion;
try {
  QuizQuestion = require('../../QuizQuestion.js').default;
  quizQuestionComponentExists = true;
} catch (e) {
  quizQuestionComponentExists = false;
}

let fs = require('fs');

describe('QuizQuestion Component', () => {
  it('has a render method that returns the correct HTML  @quiz-question-component-has-render-method', () => {
    assert(quizQuestionComponentExists, "The QuizQuestion component hasn't been created yet.")

    let mock_prop = {
      instruction_text: "How many continents are there on Planet Earth?",
      answer_options: ["5", "6", "7", "8"]
    }
    let quiz;
    try {
      quiz = shallow(<QuizQuestion quiz_question={mock_prop} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestion component.")
    }

    let html = quiz.html()
    let div = document.createElement('div')
    div.innerHTML = html

    assert(div.querySelector('main') != null, "We can't find a `main` tag in the QuizQuestion component's JSX.")
    assert(div.querySelectorAll('main section') != null, "We can't find a `main` tag with a child of `section` in the QuizQuestion component's JSX.")
    assert(div.querySelectorAll('main section').length == 2, "We're finding some `section` tags that are children of a `main` tag, but we'd like there to be exactly *2* `section` tags - if you're having trouble, just copy the HTML that we've provided in the task instructions.")
    assert(div.querySelectorAll('main section')[0].querySelector('p'), "We can't find a `section` tag with a child of `p` in the QuizQuestion component's JSX.")
    assert(div.querySelectorAll('main section')[1].querySelector('ul'), "We can't find a `ul` tag with a child of `li` in the QuizQuestion component's JSX.")
  })
})