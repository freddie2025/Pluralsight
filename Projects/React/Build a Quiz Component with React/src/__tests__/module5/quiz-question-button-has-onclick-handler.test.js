import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';
import sinon from 'sinon';

let quizQuestionButtonComponentExists = false;
let QuizQuestionButton;
try {
  QuizQuestionButton = require('../../QuizQuestionButton.js').default;
  quizQuestionButtonComponentExists = true;
} catch (e) {
  quizQuestionButtonComponentExists = false;
}

describe('QuizQuestionButton Component', () => {
  it('has a button with an onClick handler @quiz-question-button-has-onclick-handler', () => {
    assert(quizQuestionButtonComponentExists, "The QuizQuestionButton component hasn't been created yet.")

    let spy
    try {
      spy = sinon.spy(QuizQuestionButton.prototype, 'handleClick')
    } catch (e) {
      assert(false, "There's not a method named `handleClick()` that's being called on button click in the QuizQuestionButton class.")
    }

    let mockedPropHandler = sinon.spy()

    let quizQuestionButton
    try {
      quizQuestionButton = shallow(<QuizQuestionButton button_text='5' clickHandler={mockedPropHandler} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizQuestionButton component.")
    }

    quizQuestionButton.find('button').simulate('click')
    assert(spy.calledOnce, "There's not a method named `handleClick()` that's being called on button click in the QuizQuestionButton class.")

  })
})