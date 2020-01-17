import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require('babylon')

describe('Quiz Component', () => {
  it('requires quiz_data.json @quiz-component-requires-quiz-json', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../Quiz.js').toString();
    } catch (e) {
      assert(false, "The Quiz.js file hasn't been created yet.")
    }

    let ast = babylon.parse(file, { sourceType: "module", plugins: ["jsx"] })
    assert(ast.program.body, "We can't find any code in the Quiz.js file.")

    let quiz_data_loaded_correctly = false;

    ast['program']['body'].forEach(element => {
      if (element.type == 'VariableDeclaration') {
        if (element.kind == 'let') {
          if (element.declarations[0].id.name == 'quizData') {
            if (element.declarations[0].init.callee.name == 'require') {
              if (element.declarations[0].init.arguments[0].value == './quiz_data.json') {
                quiz_data_loaded_correctly = true
              } else {
                assert(false, "We found where you're trying to require a file in the `quizData` variable, but it doesn't look like you're requiring the correct file.")
              }
            } else {
              assert(false, "Make sure you're using the `require` function to load the `quiz_data.json` file into a variable.")
            }
          } else {
            assert(false, "We'd like you to name your variable `quizData`.")
          }
        } else {
          assert(false, "We can't find where you're creating a variable with the `let` keyword.")
        }
      }
    })
    assert(quiz_data_loaded_correctly, "We can't find where you're loading the quiz_data JSON into a variable named quizData.")
  });
})