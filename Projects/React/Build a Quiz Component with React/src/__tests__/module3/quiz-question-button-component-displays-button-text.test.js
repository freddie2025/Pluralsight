import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
import { assert } from 'chai';

let quizQuestionButtonComponentExists = false;
let QuizQuestionButton;
try {
  QuizQuestionButton = require('../../QuizQuestionButton.js').default;
  quizQuestionButtonComponentExists = true;
} catch (e) {
  quizQuestionButtonComponentExists = false;
}

let fs = require('fs');
let quizData = require('../../quiz_data.json')

describe('QuizQuestionButton Component', () => {
  it('displays correct button text @quiz-question-button-component-displays-button-text', () => {
    assert(quizQuestionButtonComponentExists, "The QuizQuestionButton component hasn't been created yet.")

    let quizQuestionButton;

    let mock_prop = '5'
    try {
      quizQuestionButton = shallow(<QuizQuestionButton button_text={mock_prop} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestionButton component.")
    }

    let html = quizQuestionButton.html()
    let div = document.createElement('div')
    div.innerHTML = html

    assert(div.querySelector('li button') != null, "We can't find a `button` tag that's a child of a single `li` tag in the QuizQuestionButton component's JSX.")
    let button_contents = div.querySelectorAll('li button')[0]
    assert(button_contents.innerHTML == quizData.quiz_questions[0].answer_options[0], "You're not displaying the correct data from the `button_text` prop in the QuizQuestionButton component's JSX.")
  })
})