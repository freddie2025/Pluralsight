import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount } from 'enzyme';
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
let quizData = require('../../quiz_data.json')
let babylon = require("babylon");

describe('Quiz Component', () => {
  it('renders QuizEnd or QuizQuestion component based on condition @quiz-component-has-conditional', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")

    let quiz;

    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")
    }

    if (yallReadyForThis()) {
      assert(quiz.find('QuizQuestion').length == 1 && quiz.find('QuizEnd').length == 0, "QuizQuestion should be displaying when isQuizEnd is false.")
      quiz.setState({quiz_position: quizData.quiz_questions.length+1 })
      assert(quiz.find('QuizQuestion').length == 0 && quiz.find('QuizEnd').length == 1, "QuizEnd should be displaying when isQuizEnd is true.")      
    } else {
      assert(false, "We couldn't find a const named `isQuizEnd`.")
    }

  })
})

function yallReadyForThis() {
  let file;
  try {
    file = fs.readFileSync(__dirname + '/../../Quiz.js').toString();
  } catch (e) {
    assert(false, "The Quiz.js file hasn't been created yet.")
  }

  let ast = babylon.parse(file, { sourceType: "module", plugins: ["jsx"] })

  let class_declaration_count = 0;
  let found_const = 0;
  let is_quiz_end_count = 0;

  ast['program']['body'].forEach(element => {
    if (element.type == 'ClassDeclaration') {
      if (element.id.name == 'Quiz') {
        element.body.body.forEach(el => {
          if (el.kind == 'method') {
            if (el.key.name == 'render') {
              el.body.body.forEach(el2 => {
                if (el2.kind == 'const') {
                  found_const = found_const + 1
                  el2.declarations.forEach(el3 => {
                    if (el3.id && el3.id.name == 'isQuizEnd') {
                      is_quiz_end_count = is_quiz_end_count + 1
                    }
                  })
                }
              })
            }
          }
        })
      }
      class_declaration_count = class_declaration_count + 1
    }
  })
  if (found_const == 1 && is_quiz_end_count == 1) {
    return true
  } else {
    return false
  }
}