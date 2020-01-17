import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';
import sinon from 'sinon';

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
let babylon = require('babylon')

describe('QuizQuestion Component', () => {
  it('has a method named `handleClick` and a renders a QuizQuestionButton component with a `clickHandler` prop @quiz-question-has-onclick-handler', () => {
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

    assert(quizQuestion.find('QuizQuestionButton').length == quizData.quiz_questions[0].answer_options.length, "The number of QuizQuestionButton components that are rendered by the QuizQuestion component don't match the number of `answer_options` in the JSON data.")
    
    assert(quizQuestion.find('QuizQuestionButton').first().props().clickHandler != null, "The QuizQuestionButton tag in QuizQuestion's JSX doesn't have a `clickHandler` property.")

    assert(quizQuestion.find('QuizQuestionButton').first().props().clickHandler.name == 'bound handleClick', "The QuizQuestionButton tag in QuizQuestion's JSX has a `clickHandler` property, but the value isn't set to `this.handleClick.bind(this)`.")

  })
})