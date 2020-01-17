import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let quizComponentExists = false;
let Quiz;
try {
  Quiz = require('../../Quiz.js').default;
  quizComponentExists = true;
} catch (e) {
  quizComponentExists = false;
}

let fs = require('fs');

describe('Quiz Component', () => {
  it('constructor sets the state to the quiz_data JSON @quiz-component-constructor-sets-state', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")

    let quiz;
    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")      
    }

    let expectedState = {
      quiz_position: 1
    }

    assert(quiz.state() != null, "We don't see that you're setting the state in the Quiz component constructor.")
    assert(JSON.stringify(quiz.state()) == JSON.stringify(expectedState), "We can see that you're setting the state in the Quiz component constructor, but that state isn't a key `quiz_position` with a value of `1`.")
  })
})