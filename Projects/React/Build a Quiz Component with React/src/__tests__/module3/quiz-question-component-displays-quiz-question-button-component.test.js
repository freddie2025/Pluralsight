import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
import { assert } from 'chai';

let quizQuestionComponentExists = false;
let QuizQuestion;
try {
  QuizQuestion = require('../../QuizQuestion.js').default;
  quizQuestionComponentExists = true;
} catch (e) {
  quizQuestionComponentExists = false;
}

let quizQuestionButtonComponentExists = false;
let QuizQuestionButton;
try {
  QuizQuestionButton = require('../../QuizQuestionButton.js').default;
  quizQuestionButtonComponentExists = true;
} catch (e) {
  quizQuestionButtonComponentExists = false;
}

let fs = require('fs');

describe('QuizQuestion Component', () => {
  it('renders QuizQuestionButton component @quiz-question-component-displays-quiz-question-button-component', () => {
    assert(quizQuestionComponentExists, "The QuizQuestion component hasn't been created yet.")
    assert(quizQuestionButtonComponentExists, "The QuizQuestionButton component hasn't been created yet.")

    let quizQuestion;

    let mock_prop = {
      instruction_text: "How many continents are there on Planet Earth?",
      answer_options: ["5", "6", "7", "8"]
    }
    try {
      quizQuestion = shallow(<QuizQuestion quiz_question={mock_prop} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestion component.")
    }

    let html = quizQuestion.html()
    let div = document.createElement('div')
    div.innerHTML = html

    assert(div.querySelector('main') != null, "We can't find a `main` tag in the QuizQuestion component's JSX.")
    let ul_contents = div.querySelectorAll('main section ul')[0]
    assert(ul_contents.querySelector('li button') != null, "You're not rendering the correct HTML tags from the QuizQuestionButton component in the QuizQuestion's render method.")
  })
})