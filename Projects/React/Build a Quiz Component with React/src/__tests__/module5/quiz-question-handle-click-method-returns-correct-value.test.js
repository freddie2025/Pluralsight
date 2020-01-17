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

describe('QuizQuestion Component', () => {
  it('handleClick method has conditional that checks argument and called clickHandler @quiz-question-handle-click-method-returns-correct-value', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")
    assert(quizQuestionComponentExists, "The QuizQuestion component hasn't been created yet.")
    assert(quizQuestionButtonComponentExists, "The QuizQuestionButton component hasn't been created yet.")

    let quiz
    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")
    }

    let spy
    try {
      spy = sinon.spy(QuizQuestion.prototype, 'handleClick')
    } catch (e) {
      assert(false, "There's not a method named `handleClick()` in the QuizQuestion class.")
    }

    let spy2
    try {
      spy2 = sinon.spy(Quiz.prototype, 'showNextQuestion')
    } catch (e) {
      assert(false, "There's not a method named `showNextQuestion()` in the Quiz class.")
    }

    let mockedPropHandler = sinon.spy()

    let mock_prop = {
      instruction_text: "How many continents are there on Planet Earth?",
      answer_options: ["5", "6", "7", "8"],
      answer: "5"
    }

    let quizQuestion
    try {
      quizQuestion = shallow(<QuizQuestion quiz_question={mock_prop} showNextQuestionHandler={spy2} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestion component.")
    }

    quizQuestion.instance().handleClick('3')
    assert(spy2.called == false, "It doesn't appear that the conditional logic checking if the `buttonText` is equal to `this.props.quiz_question.answer` in the QuizQuestion's `handleClick` method.")
    try {
      quizQuestion.instance().handleClick('5')
    } catch (e) {
    }
    assert(spy2.called == true, "`this.props.showNextQuestionHandler()` is not being called when the `buttonText` is equal to `this.props.quiz_question.answer` in QuizQuestion's `handleClick` method.")
  })
})