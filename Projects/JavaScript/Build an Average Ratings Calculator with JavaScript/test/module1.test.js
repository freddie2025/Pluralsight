describe('Module 01 - Calculate Average Rating', () => {

  const collect_ratings = ast.returnParent('collect_ratings');
  const event_listener = ast.findCall('addEventListener');
  const if_statement = (collect_ratings.length) ? jscs(collect_ratings.findIf()) : false;

  const for_each = ast.findCall('forEach');
  const elements_foreach_arrow = {
    'type': 'CallExpression',
    'callee.type': 'MemberExpression',
    'callee.object.name': 'elements',
    'callee.property.name': 'forEach',
    'arguments.0.type': 'ArrowFunctionExpression',
  };
  const elements_foreach_function =  Object.assign({}, elements_foreach_arrow, { 'arguments.0.type': 'FunctionExpression' })
  const for_each_matched = matchObj(for_each, elements_foreach_arrow) || matchObj(for_each, elements_foreach_function);

  const forEachParam = (for_each.length) ? findParam(for_each) : false;

  it('Should add a `<script>` element. @script-element', () => {
    const element = $('body').children().is('script[src="js/ratings.js"]');
    assert(element, 'Have you added a `<script>` element to the `index.html` file?');
  });

  it('Should have a `collect_ratings` function. @collect-ratings-function', () => {
    assert(ast.findIdentifierParent('collect_ratings'), 'Do you have a function called `collect_ratings()`?');
  });

  it('Should have a `ratings` object. @ratings-object', () => {
    assert(ast.findIdentifierParent('ratings'), 'Do you have a `const` called `ratings`?');
    const ratings = ast.findIdentifierParent('ratings').init;
    assert(ratings.type === 'ObjectExpression', 'Is the `ratings` constant an object?');

    const properties = {};
    ratings.properties.forEach(property => {
      if (property.key.name) {
        properties[property.key.name] =  property.value.value;
      } else if (property.key.value) {
        properties[property.key.value] =  property.value.value;
      }
    });

    const matched = JSON.stringify(properties) === JSON.stringify({ count: 0, sum: 0, average: 0 })
    assert(matched, 'Does the `ratings` object contain three key value pairs: `count: 0, sum: 0, average: 0`?');
  });

  it('Should have a `rating` variable. @ratings-variable', () => {
    const rating = ast.findIdentifierParent('rating');
    assert(rating, 'Do you have a `let` binding called `rating`?');
    assert(rating.init.value === 0, 'Is `rating` set to 0?');
  });

  it('Should collect HTML elements. @rating-elements', () => {
    const elements = ast.findIdentifierParent('elements');
    assert(elements, 'Do you have a `const` called `elements`?');
    const elements_match = {
      'type': 'CallExpression',
      'callee.object.name': 'document',
      'callee.property.name': 'querySelectorAll',
      'arguments.0.value': '.rating',
    };
    assert(match(elements.init, elements_match), 'Are you assigning the `elements` constant a call to `document.querySelectorAll()` with the correct parameters?');
  });

  it('Should loop through `elements`. @elements-foreach', () => {
    assert(for_each_matched, 'Are you using `forEach` to loop through `elements`? Are you passing an arrow function to `forEach`?');
  });

  it('Should set `rating`. @rating-value', () => {
    assert(for_each_matched, 'Does the `forEach` loop exist?');
    const rating_for_each = for_each.find(jscs.AssignmentExpression, { left: { type: 'Identifier', name: 'rating' }})
    const rating_for_each_right = rating_for_each.length ? rating_for_each.get().value.right : false;

    assert(rating_for_each.length, 'Are you setting the `ratings` variable?');
    assert(rating_for_each_right.callee.name === 'parseInt', 'Are you using `parseInt()` to convert the `element.id` to a number?');
    assert(rating_for_each_right.arguments.length >= 1, 'Are you passing `parseInt()` the correct number of parameters?');

    const replace_call_match = {
      'callee.object.object.name': forEachParam,
      'callee.object.property.name': 'id',
      'callee.property.name': 'replace',
      'arguments.0.value': 'star',
      'arguments.1.value': ''
    }
    assert(match(rating_for_each_right.arguments[0], replace_call_match), 'Are you passing `parseInt()` a call to the `replace` function on `element.id`? The `replace` function should replace `star` with nothing.');
  });

  it('Should set `ratings.count`. @ratings-count', () => {
    assert(for_each_matched, 'Does the `forEach` loop exist?');
    const ratings_count = for_each.findPropertyAssignment('ratings', 'count');
    assert(ratings_count.length, 'In the `forEach` loop are you setting `ratings.count`?');

    const ratings_count_ap_match = {
      'operator': '+=',
      'right.callee.name': 'parseInt',
      'right.arguments.0.object.name': forEachParam,
      'right.arguments.0.property.name': 'value'
    };
    const ratings_count_left_match = {
      'operator': '=',
      'right.left.object.name': 'ratings',
      'right.left.property.name': 'count',
      'right.operator': '+',
      'right.right.callee.name': 'parseInt',
      'right.right.arguments.0.object.name': forEachParam,
      'right.right.arguments.0.property.name': 'value'
    };
    const ratings_count_right_match = {
      'operator': '=',
      'right.right.object.name': 'ratings',
      'right.right.property.name': 'count',
      'right.operator': '+',
      'right.left.callee.name': 'parseInt',
      'right.left.arguments.0.object.name': forEachParam,
      'right.left.arguments.0.property.name': 'value'
    };

    assert(matchObj(ratings_count, ratings_count_ap_match) ||
           matchObj(ratings_count, ratings_count_left_match) ||
           matchObj(ratings_count, ratings_count_right_match),'Are you adding the converted `element.value` to `ratings.count`?');
  });

  it('Should set `ratings.sum`. @ratings-sum', () => {
    assert(for_each_matched, 'Does the `forEach` loop exist?');
    const ratings_sum = for_each.findPropertyAssignment('ratings', 'sum');
    assert(ratings_sum.length, 'In the `forEach` loop are you setting `ratings.sum`?');

    const ratings_sum_left_match = {
      'operator': '=',
      'right.left.object.name': 'ratings',
      'right.left.property.name': 'sum',
      'right.operator': '+'
    };
    const ratings_sum_right_match = {
      'operator': '=',
      'right.right.object.name': 'ratings',
      'right.right.property.name': 'sum',
      'right.operator': '+'
    };
    assert(matchObj(ratings_sum, { 'operator': '+=' }) ||
           matchObj(ratings_sum, ratings_sum_left_match) ||
           matchObj(ratings_sum, ratings_sum_right_match), 'Are you adding to the current value of `ratings.sum` to itself?');

    const sides = ratings_sum.findSides('*');
    const multiple_left_match = {
      'left.name': 'rating',
      'right.callee.name': 'parseInt',
      'right.arguments.0.object.name': forEachParam,
      'right.arguments.0.property.name': 'value'
    };
    const multiple_right_match = {
      'right.name': 'rating',
      'left.callee.name': 'parseInt',
      'left.arguments.0.object.name': forEachParam,
      'left.arguments.0.property.name': 'value'
    };

    assert(match(sides, multiple_left_match) ||
           match(sides, multiple_right_match), 'Are you calling `parseInt()` on `element.value` and multiplying it by `rating`?');
  });

  it('Should check if `ratings.count` is zero. @ratings-count-zero', () => {
    assert(collect_ratings.length, 'Do you have a function called `collect_ratings()`?');
    const if_value = if_statement.length ? if_statement.get().value : false;

    assert(if_value, 'Do you have an `if` statement testing whether `ratings.count` is zero?');
    assert((if_value.test.operator === '!==' || if_value.test.operator === '!=') &&
      ((if_value.test.right.value === 0 &&  (if_value.test.left.object.name === 'ratings' &&  if_value.test.left.property.name === 'count')) ||
      (if_value.test.left.value === 0 && (if_value.test.right.object.name === 'ratings' && if_value.test.right.property.name === 'count'))),
      'Do you have an `if` statement testing whether `ratings.count` is zero?');
  });

  it('Should calculate `ratings.average` in `if` statement. @calculate-ratings-average', () => {
    assert(collect_ratings.length, 'Do you have a function called `collect_ratings()`?');
    assert(if_statement.length, 'Do you have an `if` statement testing whether `ratings.count` is zero?');
    const ratings_average = if_statement.findPropertyAssignment('ratings', 'average');
    const ratings_average_expr = ratings_average.findBinary();
    const ratings_average_match = {
      'left.object.name': 'ratings',
      'left.property.name': 'sum',
      'operator': '/',
      'right.object.name': 'ratings',
      'right.property.name': 'count'
    };
    assert(matchObj(ratings_average_expr, ratings_average_match), 'Are you calculating the average in the `if` statement and assigning it to `ratings.average`?');
  });

  it('Should return `ratings` from `collect_ratings()`. @return-ratings', () => {
    assert(collect_ratings.length, 'Do you have a function called `collect_ratings()`?');
    const return_match = { 'argument.name': 'ratings' };
    assert(matchObj(collect_ratings.findReturn(), return_match), 'Are you returning `ratings`?');
  });

  it('Should register a `change` event listener. @change-event-listener', () => {
    assert(event_listener.length, 'Have you registered an event listener for the `change` event?');
    const event_listener_match = {
      'callee.type': 'MemberExpression',
      'callee.object.name': 'document',
      'callee.property.name': 'addEventListener',
      'arguments.0.value': 'change'
    };
    assert(matchObj(event_listener, event_listener_match), 'Have you created an event listener that listens for the `change` event?');
  });

  it('Should call `collect_ratings`. @call-collect-ratings', () => {
    assert(event_listener.length, 'Have you registered an event listener for the `change` event?');
    const call_collect_ratings = event_listener.findIdentifierParent('ratings');
    assert(call_collect_ratings, 'Are you calling `collect_ratings()` and assigning the result to `ratings`?');
  });

  it('Should set value of the `#average` element to `ratings.average`. @set-average', () => {
    assert(event_listener.length, 'Have you registered an event listener for the `change` event?');
    const query_selector_match = {
      'left.object.callee.object.name': 'document',
      'left.object.callee.property.name': 'querySelector',
      'left.object.arguments.0.value': '#average',
      'left.property.name': 'value',
      'operator': '=',
      'right.callee.object.object.name': 'ratings',
      'right.callee.object.property.name': 'average',
      'right.callee.property.name': 'toFixed',
      'right.arguments.0.value': 2
    }
    const query_selector = event_listener.find(jscs.AssignmentExpression, dot.object(query_selector_match));
    assert(query_selector.length, 'Are you setting the value of the HTML element with an id of `#average` to `ratings.average` fixed to 2 decimal places?')
  });

});
