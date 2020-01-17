import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';
import sinon from 'sinon';

let quizExists = false;
let Quiz;
try {
  Quiz = require('../../Quiz.js').default;
  quizExists = true;
} catch (e) {
  quizExists = false;
}

let quizEndExists = false;
let QuizEnd;
try {
  QuizEnd = require('../../QuizEnd.js').default;
  quizEndExists = true;
} catch (e) {
  quizEndExists = false;
}

let fs = require('fs');
let quizData = require('../../quiz_data.json')
let babylon = require('babylon')

describe('QuizEnd Component', () => {
  it('has a method named `handleResetClick` and a renders a QuizEnd component with a `resetClickHandler` prop @quiz-end-has-reset-click-handler', () => {
    assert(quizExists, "The Quiz component hasn't been created yet.")
    assert(quizEndExists, "The QuizEnd component hasn't been created yet.")

    let quiz;

    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")
    }

    quiz.setState({ quiz_position: quizData.quiz_questions.length + 1 })
    
    assert(quiz.instance().handleResetClick, "There doesn't appear to be a method named `handleResetClick()` in the Quiz component.")

    assert(quiz.find('QuizEnd').props().resetClickHandler != null, "The QuizEnd tag in Quiz's JSX doesn't have a `resetClickHandler` property set to the correct value.")

    assert(quiz.find('QuizEnd').props().resetClickHandler.name == 'bound handleResetClick', "The QuizEnd tag in Quiz's JSX has a `resetClickHandler` property, but the value isn't set to `this.handleResetClick.bind(this)`.")

  })
})