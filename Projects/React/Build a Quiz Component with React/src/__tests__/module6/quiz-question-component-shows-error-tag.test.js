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
  it('shows error paragraph tag if incorrectAnswer is true @quiz-question-component-shows-error-tag', () => {
    assert(quizQuestionComponentExists, "The QuizQuestion component hasn't been created yet.")

    let mock_prop = {
      instruction_text: "How many continents are there on Planet Earth?",
      answer_options: ["5", "6", "7", "8"]
    }

    let quizQuestion;
    try {
      quizQuestion = shallow(<QuizQuestion quiz_question={mock_prop} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestion component.")
    }

    let expectedState = {
      incorrectAnswer: false
    }

    assert(quizQuestion.state() != null, "The QuizQuestion component isn't starting with the correct default state declared in the constructor function.")

    assert(quizQuestion.state().incorrectAnswer != null && quizQuestion.state().incorrectAnswer == false, "The QuizQuestion component's state should start out with a key of `incorrectAnswer` set to `false`.")

    quizQuestion.setState({ incorrectAnswer: true })

    assert(quizQuestion.state().incorrectAnswer != null && quizQuestion.state().incorrectAnswer == true && quizQuestion.find('.error').length == 1, "When the QuizQuestion component's state has a key of `incorrectAnswer` with a value of `true`, a paragraph tag with the className `error` should be displayed.")

    assert(quizQuestion.find('.error').text() != '', "When the QuizQuestion component's state has a key of `incorrectAnswer` with a value of `true`, a paragraph tag with the className `error` with some error message text should be displayed.")
  })
})