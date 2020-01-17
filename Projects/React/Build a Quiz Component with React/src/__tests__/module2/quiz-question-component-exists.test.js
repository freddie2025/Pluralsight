let fs = require('fs');
import { assert } from 'chai';

describe('QuizQuestion Component', () => {
  it('exists @quiz-question-component-exists', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizQuestion.js').toString();
    } catch (e) {
      assert(false, "The QuizQuestion.js file hasn't been created yet.")
    }
  });
})
