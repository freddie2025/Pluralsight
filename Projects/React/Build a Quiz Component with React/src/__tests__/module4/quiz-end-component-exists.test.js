let fs = require('fs');
import { assert } from 'chai';

describe('QuizEnd Component', () => {
  it('exists @quiz-end-component-exists', () => {
    let file;
    try {
      file = fs.readFileSync(__dirname + '/../../QuizEnd.js').toString();
    } catch (e) {
      assert(false, "The QuizEnd.js file hasn't been created yet.")
    }
  });
})
