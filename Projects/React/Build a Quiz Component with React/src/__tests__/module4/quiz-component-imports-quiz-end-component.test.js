import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require('babylon')

describe('Quiz Component', () => {
  it('imports QuizEnd from QuizEnd.js @quiz-component-imports-quiz-end-component', () => {

    let quizFile;
    try {
      quizFile = fs.readFileSync(__dirname + '/../../Quiz.js').toString();
    } catch (e) {
      assert(false, "The Quiz.js file hasn't been created yet.")
    }

    let quizEndFile;
    try {
      quizEndFile = fs.readFileSync(__dirname + '/../../QuizEnd.js').toString();
    } catch (e) {
      assert(false, "The QuizEnd.js file hasn't been created yet.")
    }

    let ast = babylon.parse(quizFile, { sourceType: "module", plugins: ["jsx"] })

    let quiz_end_import_found = false;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ImportDeclaration') {
        if (element.source.value == './QuizEnd.js' || element.source.value == './QuizEnd' || element.source.value == 'QuizEnd') {
          assert(element.specifiers[0].local.name == 'QuizEnd', "You're not importing the QuizEnd class from the QuizEnd.js file.")
          quiz_end_import_found = true
        }
      }
    })
    assert(quiz_end_import_found, "You're not importing the QuizEnd.js file.")
  });
})