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
  it('has QuizQuestionButton component with correct prop @quiz-question-button-component-has-button-text-prop', () => {
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

    let expectedProps = {
      button_text: '5'
    }
    if (quizQuestion.find('QuizQuestionButton').length == 1) {
      assert(JSON.stringify(quizQuestion.find('QuizQuestionButton').props()) == JSON.stringify(expectedProps), "You're not passing the correct prop values to QuizQuestionButton.")
    } else if (quizQuestion.find('QuizQuestionButton').length == 4) {
      
    } else {
      assert(false, "We don't see the QuizQuestionButton element in the QuizQuestion component's JSX.")
    }

  })
})