import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
import { assert } from 'chai';
import sinon from 'sinon';

let quizComponentExists = false;
let Quiz;
try {
  Quiz = require('../../Quiz.js').default;
  quizComponentExists = true;
} catch (e) {
  quizComponentExists = false;
}

let quizEndComponentExists = false;
let QuizEnd;
try {
  QuizEnd = require('../../QuizEnd.js').default;
  quizEndComponentExists = true;
} catch (e) {
  quizEndComponentExists = false;
}

describe('QuizEnd Component', () => {
  it('handleResetClick method called resetClickHandler @quiz-end-handle-click-method-returns-correct-value', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")
    assert(quizEndComponentExists, "The QuizEnd component hasn't been created yet.")

    let quiz
    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")
    }

    let quiz_spy
    try {
      quiz_spy = sinon.spy(Quiz.prototype, 'handleResetClick')
    } catch (e) {
      assert(false, "There's not a method named `handleResetClick()` in the Quiz class.")
    }

    let quiz_end_spy
    try {
      quiz_end_spy = sinon.spy(QuizEnd.prototype, 'handleResetClick')
    } catch (e) {
      assert(false, "There's not a method named `handleResetClick()` in the QuizEnd class.")
    }

    let mockedPropHandler = sinon.spy()

    let mock_prop = {
      instruction_text: "How many continents are there on Planet Earth?",
      answer_options: ["5", "6", "7", "8"],
      answer: "5"
    }

    let quizEnd
    try {
      quizEnd = shallow(<QuizEnd resetClickHandler={mockedPropHandler} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizEnd component.")
    }

    quizEnd.find('a').simulate('click')
    assert(mockedPropHandler.called, "QuizEnd's `handleResetClick()` method isn't making a call to `this.props.resetClickHandler()`.")
  })
})