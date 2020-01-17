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
  it('constructor sets the state key `incorrectAnswer` to `false` @quiz-question-component-constructor-sets-state', () => {
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

    assert(quizQuestion.state() != null, "We don't see that you're setting the state in the QuizQuestion component constructor.")
    assert(JSON.stringify(quizQuestion.state()) == JSON.stringify(expectedState), "We can see that you're setting the state in the QuizQuestion component constructor, but it's not setting the key `incorrectAnswer` to the value `false`.")
  })
})