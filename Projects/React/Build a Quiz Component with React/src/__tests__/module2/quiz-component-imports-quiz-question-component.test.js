import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require('babylon')

describe('Quiz Component', () => {
  it('imports QuizQuestion from QuizQuestion.js @quiz-component-imports-quiz-question-component', () => {
    let quizFile;
    try {
      quizFile = fs.readFileSync(__dirname + '/../../Quiz.js').toString();
    } catch (e) {
      assert(false, "The Quiz.js file hasn't been created yet.")
    }

    let quizQuestionFile;
    try {
      quizQuestionFile = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }

    let ast = babylon.parse(quizFile, { sourceType: "module", plugins: ["jsx"] })

    let quiz_question_import_found = false;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ImportDeclaration') {
        if (element.source.value == './QuizQuestion.js' || element.source.value == './QuizQuestion' || element.source.value == 'QuizQuestion') {
          assert(element.specifiers[0].local.name == 'QuizQuestion', "You're not importing the QuizQuestion class from the QuizQuestion.js file.")
          quiz_question_import_found = true
        }
      }
    })
    assert(quiz_question_import_found, "You're not importing the QuizQuestion.js file.")
  });
})