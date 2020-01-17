import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../App';
import { shallow, mount, render } from 'enzyme';
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

describe('App Component', () => {
  it('renders Quiz component @app-component-displays-quiz-component', () => {
    assert(quizComponentExists, "The Quiz component hasn't been created yet.")

    let app = mount(<App />)
    assert(app.contains(<Quiz />), "The App component is not rendering the Quiz component.")
  });
})