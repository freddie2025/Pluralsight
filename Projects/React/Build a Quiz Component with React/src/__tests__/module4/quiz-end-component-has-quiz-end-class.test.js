import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require("babylon");

describe('QuizEnd Component', () => {
  it('has a QuizEnd class that extends Component @quiz-end-component-has-quiz-end-class', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizEnd.js').toString();
    } catch (e) {
      assert(false, "The QuizEnd.js file hasn't been created yet.")
    }

    let ast = babylon.parse(file, { sourceType: "module", plugins: ["jsx"] })

    let class_declaration_count = 0;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ClassDeclaration') {
        if (element.id.name == 'QuizEnd') {
          if (element.superClass.name == 'Component') {
            
          } else {
            assert(false, "We found a class named QuizEnd, but it doesn't extend the Component class.")
          }
        }
        class_declaration_count = class_declaration_count + 1
      }
    })

    assert(class_declaration_count > 0, "We couldn't find any class declarations.")
    assert(class_declaration_count == 1, "We found more than one class declaration, but there's only supposed to be one class named QuizQuestionButton.")
  })

  it('exports the QuizEnd class as default @quiz-end-component-has-quiz-end-class', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizEnd.js').toString();
    } catch (e) {
      assert(false, "The QuizEnd.js file hasn't been created yet.")
    }

    let re = /\nexport default QuizEnd\;*\s*$/g
    let match = file.match(re)
    assert(match != null && match.length > 0, "We couldn't find `export default QuizEnd` at the end of your QuizEnd.js file.")
  })
})