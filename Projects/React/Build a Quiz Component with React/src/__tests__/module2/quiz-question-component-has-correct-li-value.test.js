import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
import { assert } from 'chai';

let quizComponentExists = false;
let Quiz;
try {
  Quiz = require('../../Quiz.js').default;
  quizComponentExists = true;
} catch (e) {
  quizComponentExists = false;
}

let quizQuestionComponentExists = false;
let QuizQuestion;
try {
  QuizQuestion = require('../../QuizQuestion.js').default;
  quizQuestionComponentExists = true;
} catch (e) {
  quizQuestionComponentExists = false;
}

let fs = require('fs');
let quizData = require('../../quiz_data.json')

describe('QuizQuestion Component', () => {
  it('has li tag with correct value @quiz-question-component-has-correct-li-value', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")
    assert(quizQuestionComponentExists, "The QuizQuestion component hasn't been created yet.")

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
    assert(div.querySelectorAll('main section ul li').length != 0, "We can't find an `li` tag inside of the `ul` tag in the QuizQuestion component's JSX.")
    if (div.querySelectorAll('main section ul li button') != null) {
      // this runs after @quiz-question-component-displays-quiz-question-button-component
    } else {
      let li_contents = div.querySelectorAll('main section ul li')[0]
      assert(li_contents.innerHTML == quizData.quiz_questions[0].answer_options[0], "You're not displaying the correct data from the `quiz_question` prop in the QuizQuestion component's JSX.")
    }
  })
})