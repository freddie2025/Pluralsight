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

let quizQuestionComponentExists = false;
let QuizQuestion;
try {
  QuizQuestion = require('../../QuizQuestion.js').default;
  quizQuestionComponentExists = true;
} catch (e) {
  quizQuestionComponentExists = false;
}

let fs = require('fs');
let quizData = require('../../quiz_data.json')

describe('Quiz Component', () => {
  it('renders a div with a className of `QuizQuestion`  @quiz-component-has-quiz-question-div', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")

    let quiz;
    try {
      quiz = shallow(<Quiz />)
    } catch (e) {
      assert(false, "We weren't able to mount the Quiz component.")      
    }
    
    if (quiz.containsMatchingElement(<div className="QuizQuestion"></div>)) {
      // this block will run after @quiz-component-has-quiz-question-div
      assert(quiz.containsMatchingElement(<div className="QuizQuestion"></div>), "The Quiz component isn't rendering a single div with the class `QuizQuestion`.")
    } else if ( quiz.find('.QuizQuestion').getElements().length == 1) {
      let el = quiz.find('.QuizQuestion').getElements()[0];
      if (el.props.className == 'QuizQuestion') {
        if (el.props.children == null) {
          assert(el.props.children == quizData.quiz_questions[0].instruction_text)
        }
      }
    } else if (quiz.containsMatchingElement(<div>Quiz</div>)) {
      // this block will run until @quiz-component-has-quiz-question-div
      assert(false, "The Quiz component isn't rendering a single div with the class `QuizQuestion`.")
    } else if (quizQuestionComponentExists) {
      if (quiz.containsMatchingElement(<QuizQuestion />)) {
        // this block will run after @quiz-question-component-has-render-method in module 2
      }
    } else {
      assert(false, "The Quiz component isn't rendering a single div with the class `QuizQuestion`.")
    }

  })
})