import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require('babylon')

describe('QuizQuestion Component', () => {
  it('imports QuizQuestionButton from QuizQuestionButton.js @quiz-question-component-imports-quiz-question-button-component', () => {

    let quizQuestionFile;
    try {
      quizQuestionFile = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }

    let quizQuestionButtonFile;
    try {
      quizQuestionButtonFile = fs.readFileSync(__dirname + '/../../QuizQuestionButton.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestionButton.js file hasn't been created yet.")
    }

    let ast = babylon.parse(quizQuestionFile, { sourceType: "module", plugins: ["jsx"] })

    let quiz_question_button_import_found = false;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ImportDeclaration') {
        if (element.source.value == './QuizQuestionButton.js' || element.source.value == './QuizQuestionButton' || element.source.value == 'QuizQuestionButton') {
          assert(element.specifiers[0].local.name == 'QuizQuestionButton', "You're not importing the QuizQuestionButton class from the QuizQuestionButton.js file.")
          quiz_question_button_import_found = true
        }
      }
    })
    assert(quiz_question_button_import_found, "You're not importing the QuizQuestionButton.js file.")
  });
})