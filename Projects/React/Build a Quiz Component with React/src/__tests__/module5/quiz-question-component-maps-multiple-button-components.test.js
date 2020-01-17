import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
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
let babylon = require("babylon");

describe('QuizQuestion Component', () => {
  it('renders multiple button components by using the map function @quiz-question-component-maps-multiple-button-components', () => {
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

    let nodes = quizQuestion.find('QuizQuestionButton')
    assert(nodes.length == mock_prop.answer_options.length, "We're not seeing enough QuizQuestionButton components being rendered.  The number should match the amount of answer options for each question.")

    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }

    let re = /props.quiz_question.answer_options.map/g
    let match = file.match(re)
    assert(match != null, "It doesn't look like you're calling the map function on `this.props.quiz_question.answer_options` inside of the `ul` tag.")
    assert(match.length == 1, "It doesn't look like you're calling the map function on `this.props.quiz_question.answer_options` inside of the `ul` tag.")
})
})