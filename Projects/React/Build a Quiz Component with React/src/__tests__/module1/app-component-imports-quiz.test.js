import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require('babylon')

describe('App Component', () => {
  it('imports Quiz from Quiz.js @app-component-imports-quiz-component', () => {
    let appFile;
    try {
      appFile = fs.readFileSync(__dirname + '/../../App.js').toString();
    } catch (e) {
      assert(false, "The App.js file hasn't been created yet.")
    }

    let quizFile;
    try {
      quizFile = fs.readFileSync(__dirname + '/../../Quiz.js').toString();
    } catch (e) {
      assert(false, "The Quiz.js file hasn't been created yet.")
    }

    let ast = babylon.parse(appFile, { sourceType: "module", plugins: ["jsx"] })

    let quiz_import_found = false;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ImportDeclaration') {
        if (element.source.value == './Quiz.js' || element.source.value == './Quiz' || element.source.value == 'Quiz') {
          assert(element.specifiers[0].local.name == 'Quiz', "You're not importing the Quiz class from the Quiz.js file.")
          quiz_import_found = true
        }
      }
    })
    assert(quiz_import_found, "You're not importing the Quiz.js file.")
  });
})