import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow } from 'enzyme';
import { assert } from 'chai';

let fs = require('fs');
let babylon = require("babylon");

describe('QuizQuestion Component', () => {
  it('has a constructor function that accepts `props` as a parameter @quiz-question-component-has-constructor-function', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }

    let ast = babylon.parse(file, { sourceType: "module", plugins: ["jsx"] })

    let constructor_function_found = 0;

    ast['program']['body'].forEach(element => {
      if (element.type == 'ClassDeclaration') {
        if (element.id.name == 'QuizQuestion') {
          element.body.body.forEach(el => {
            if (el.kind == 'constructor') {
              if (el.params && el.params.length == 1) {
                assert(el.params[0].name == 'props', "The QuizQuestion constructor function should accept a single parameter named `props`.")
              } else {
                assert(false, "The QuizQuestion constructor function should accept a single parameter named `props`.")
              }
              constructor_function_found = constructor_function_found + 1;
            }
          })
        }
      }
    })

    assert(constructor_function_found > 0, "We couldn't find a constructor function in your class.")
    assert(constructor_function_found == 1, "We found more than one constructor function, but there's only supposed to be one.")
  })

  it('has a constructor function that calls `super(props)` @quiz-question-component-has-constructor-function', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }

    let ast = babylon.parse(file, { sourceType: "module", plugins: ["jsx"] })

    let call_to_super = 0
    let call_to_super_with_props_argument = 0

    ast['program']['body'].forEach(element => {
      if (element.type == 'ClassDeclaration' && element.id.name == 'QuizQuestion') {
        element.body.body.forEach(el => {
          el.body.body.forEach(el2 => {
            if (el2.type) {
              if (el2.type == 'ExpressionStatement') {
                if (el2.expression) {
                  if (el2.expression.callee) {
                    if (el2.expression.callee.type) {
                      if (el2.expression.callee.type == 'Super') {
                        call_to_super = call_to_super + 1
                      }
                    }
                  }
                }
              }
            }
            if (el2.type) {
              if (el2.type == 'ExpressionStatement') {
                if (el2.expression) {
                  if (el2.expression.arguments && el2.expression.arguments.length != 0) {
                    if (el2.expression.arguments[0].name) {
                      if (el2.expression.arguments[0].name == 'props') {
                        call_to_super_with_props_argument = call_to_super_with_props_argument + 1
                      }
                    }
                  }
                }
              }
            }
          })
        })
      }
    })

    assert(call_to_super > 0, "You're not calling `super()` inside of the QuizQuestion classes' constructor function.")
    assert(call_to_super == 1, "We see that you're calling `super()` more than once - please just call it once.")
    assert(call_to_super_with_props_argument == 1, "You're not passing `props` when you call `super()`.")
  })

})