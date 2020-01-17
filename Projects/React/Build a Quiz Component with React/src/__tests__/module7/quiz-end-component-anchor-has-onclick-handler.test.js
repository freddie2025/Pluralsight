import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';
import sinon from 'sinon';

let quizEndComponentExists = false;
let QuizEnd;
try {
  QuizEnd = require('../../QuizEnd.js').default;
  quizEndComponentExists = true;
} catch (e) {
  quizEndComponentExists = false;
}

describe('QuizEnd Component', () => {
  it('has an anchor tag with an onClick handler @quiz-end-component-anchor-has-onclick-handler', () => {
    assert(quizEndComponentExists, "The QuizEnd component hasn't been created yet.")

    let spy
    try {
      spy = sinon.spy(QuizEnd.prototype, 'handleResetClick')
    } catch (e) {
      assert(false, "There's not a method named `handleResetClick()` that's being called on anchor click in the QuizEnd class.")
    }

    let mockedPropHandler = sinon.spy()

    let quizEnd
    try {
      quizEnd = shallow(<QuizEnd resetClickHandler={mockedPropHandler} />)
    } catch (e) {
      assert(false, "We weren't able to mount the QuizEnd component.")
    }

    quizEnd.find('a').simulate('click')
    assert(spy.calledOnce, "There's not a method named `handleResetClick()` that's being called on the anchor click in the QuizEnd class.")

  })
})