describe('Module 01 - Kanban Board Events - kanban.js', () => {

  const create_item = ast.findFunction('create_item');
  const validate_if = create_item.findIf();
  const drop = ast.findCall('forEach').findLiteral('drop');
  const eventParamDrop = findEventParam(drop);

  it('Should create an `item` `div` element. @create-item', () => {
    const create_item_assignment = create_item.findVariable('item');
    const create_item_match = {
      'init.callee.object.name': 'document',
      'init.callee.property.name': 'createElement',
      'init.arguments.0.value': 'div'
    };
    assert(matchObj(create_item_assignment, create_item_match), 'Are you creating a variable called `item` and assigning it a call to the `createElement()` function');
  });

  it('Should set item attributes. @set-attributes', () => {
    const add_class = create_item.findCall('add');
    const add_class_match = {
      'callee.object.object.name': 'item',
      'callee.object.property.name': 'classList',
      'callee.property.name': 'add',
      'arguments.0.value': 'item'
    };
    assert(matchObj(add_class, add_class_match), 'Are you adding a class of `item` to the `item`?');

    const item_id = create_item.findPropertyAssignment('item', 'id');
    const item_id_match = {
      'right.type': 'BinaryExpression',
      'right.left.value': 'item-',
      'right.operator': '+',
      'right.right.name': 'order'
    };
    assert(matchObj(item_id, item_id_match), "Are you giving the `item` the `id` of `'item-' + order`?");

    const item_draggable = create_item.findPropertyAssignment('item', 'draggable');
    const item_draggable_match = {
      'operator': '=',
      'right.value': true
    };
    assert(matchObj(item_draggable, item_draggable_match), 'Are you making the `item` draggable?');
    item_draggable
  });

  it('Should register a listener for the `dragstart` event. @dragstart-event-listener', () => {
    const drag_start = create_item.findLiteral('dragstart');
    const drag_start_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'addEventListener',
    };
    assert(matchObj(drag_start, drag_start_match), 'Are you adding an event listener to `item` that listens for the `dragstart` event?');

    assert(matchObj(drag_start, { 'arguments.1.type': 'ArrowFunctionExpression' }) ||
           matchObj(drag_start, { 'arguments.1.type': 'FunctionExpression' }), 'Do you have a `dragend` handler function?');

    assert(paramLength(drag_start), 'Does your handler function have a single parameter?');

    const set_data = drag_start.findCall('setData');
    const set_data_match = {
      'callee.object.property.name': 'dataTransfer',
      'callee.property.name': 'setData',
      'arguments.0.value': 'text',
      'arguments.1.object.property.name': 'target',
      'arguments.1.property.name': 'id'
    };

    assert(matchParam(drag_start, set_data, true), 'Are you using the same `event` parameter name?');
    assert(matchObj(set_data, set_data_match), 'In the `dragstart` event handler are you setting the `text` of the `dataTransfer` to `event.target.id`?');

  });

  it('Should register a listener for the `dragend` event. @dragend-event-listener', () => {
    const drag_end = create_item.findLiteral('dragend');
    const drag_end_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'addEventListener',
    };
    assert(matchObj(drag_end, drag_end_match), 'Are you adding an event listener to `item` that listens for the `dragend` event?');

    assert(matchObj(drag_end, { 'arguments.1.type': 'ArrowFunctionExpression' }) ||
           matchObj(drag_end, { 'arguments.1.type': 'FunctionExpression' }), 'Do you have a `dragend` handler function?');

    assert(paramLength(drag_end), 'Does your handler function have a single parameter?');

    const clear_data = drag_end.findCall('clearData');
    const clear_data_match = {
      'callee.object.property.name': 'dataTransfer',
      'callee.property.name': 'clearData'
    };
    assert(matchParam(drag_end, clear_data), 'Are you using the same `event` parameter name?');
    assert(matchObj(clear_data, clear_data_match), 'In the `dragend` event handler are you setting clear all `dataTransfer` data?');
  });

  it('Should create an `input` element. @create-input', () => {
    const create_input_assignment = create_item.findVariable('input');
    const create_input_match = {
      'init.callee.object.name': 'document',
      'init.callee.property.name': 'createElement',
      'init.arguments.0.value': 'input'
    };
    assert(matchObj(create_input_assignment, create_input_match), 'Are you creating a variable called `input` and assigning it a call to the `createElement()` function');

    const append_child = create_item.findCall('appendChild');
    const append_child_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'appendChild',
      'arguments.0.name': 'input'
    };
    const append = create_item.findCall('append');
    const append_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'append',
      'arguments.0.name': 'input'
    };
    assert(matchObj(append_child, append_child_match) || matchObj(append, append_match), 'Are you appending `input` to `item`?');
  });

  it('Should create a save button. @create-save-btn', () => {
    const create_save_btn = create_item.findVariable('save_btn');
    const create_save_btn_match = {
      'init.callee.object.name': 'document',
      'init.callee.property.name': 'createElement',
      'init.arguments.0.value': 'button'
    };
    assert(matchObj(create_save_btn, create_save_btn_match), 'Are you creating a variable called `save_btn` and assigning it a call to the `createElement()` function');

    const save_btn_html = create_item.findPropertyAssignment('save_btn', 'innerHTML');
    const save_btn_text = create_item.findPropertyAssignment('save_btn', 'textContent');
    const save_btn_match = {
      'operator': '=',
      'right.value': 'Save'
    };
    assert(matchObj(save_btn_html, save_btn_match) || matchObj(save_btn_text, save_btn_match), "Are you setting the HTML of the button to `Save`?");
  });

  it('Should register a `save_btn` click event listener. @click-event-listener', () => {
    const save_btn_click = create_item.findLiteral('click');
    const save_btn_click_match = {
      'callee.object.name': 'save_btn',
      'callee.property.name': 'addEventListener',
    };
    assert(matchObj(save_btn_click, save_btn_click_match), 'Are you adding a `click` event listener to the `save_btn` button?');
    const save_btn_handler_arrow = { 'arguments.1.type': 'ArrowFunctionExpression' };
    const save_btn_handler_function = { 'arguments.1.type': 'FunctionExpression' };
    assert(matchObj(save_btn_click, save_btn_handler_arrow) || matchObj(save_btn_click, save_btn_handler_function), 'Do you have a `click` handler function?');
  });

  it('Should validate user input with an `if` statement. @valid-input-if', () => {
    const error_html = create_item.findPropertyAssignment('error', 'innerHTML');
    const error_text = create_item.findPropertyAssignment('error', 'textContent');
    const error_match = {
      'operator': '=',
      'right.value': ''
    };
    assert(matchObj(error_html, error_match) || matchObj(error_text, error_match), "Are setting the HTML of `error` to an empty string?");

    assert(jscs(validate_if).length, 'Have you created an `if` statement in the `save_btn` event handler?');
    assert((validate_if.test.operator === '!==' || validate_if.test.operator === '!=') &&
      ((validate_if.test.right.value === '' &&  (validate_if.test.left.object.name === 'input' &&  validate_if.test.left.property.name === 'value')) ||
      (validate_if.test.left.value === '' && (validate_if.test.right.object.name === 'input' && validate_if.test.right.property.name === 'value'))),
      'Do you have an `if` statement testing whether `input.value` is empty?');
  });

  it('Should have an `if` statement body. @valid-input-if-body', () => {
    assert(validate_if.consequent, 'Are you creating an `if` statement to check if `input.value` is empty?');
    const if_body = jscs(validate_if.consequent)

    const if_order_ap = if_body.findAssignment('order');
    const if_order_pp = if_body.findUpdate('order', '++');

    const if_order_ap_match = {
      'operator': '+=',
      'left.name': 'order',
      'right.value': 1
    };
    const if_order_pp_match = {
      'operator': '++',
      'argument.name': 'order'
    };
    const if_order_full_left_match = {
      'operator': '=',
      'left.name': 'order',
      'right.operator': '+',
      'right.left.name': 'order',
      'right.right.value': 1,
    };
    const if_order_full_right_match = {
      'operator': '=',
      'left.name': 'order',
      'right.operator': '+',
      'right.left.value': 1,
      'right.right.name': 'order',
    };
    assert(matchObj(if_order_ap, if_order_ap_match) ||
           matchObj(if_order_ap, if_order_full_left_match) ||
           matchObj(if_order_ap, if_order_full_right_match) ||
           matchObj(if_order_pp, if_order_pp_match), 'In the `if` statement, are adding `1` to `order`?');

    const if_item_html = if_body.findPropertyAssignment('item', 'innerHTML');
    const if_item_text = if_body.findPropertyAssignment('item', 'textContent');
    const if_item_match = {
      'operator': '=',
      'left.object.name': 'item',
      'right.object.name': 'input',
      'right.property.name': 'value',
    };
    assert(matchObj(if_item_html, if_item_match) || matchObj(if_item_html, if_item_text), 'Are you setting the HTML of `item` to the `input` value?');

    const if_adding = if_body.findAssignment('adding');
    const if_adding_match = {
      'operator': '=',
      'right.value': false
    };
    assert(matchObj(if_adding, if_adding_match), 'Are you setting the `adding` boolean to `false`?');
  });

  it('Should have an `else` statement. @valid-input-else', () => {
    assert(validate_if.alternate, 'Are you creating an `else` statement?');
    const else_body = jscs(validate_if.alternate)
    const error_html = else_body.findPropertyAssignment('error', 'innerHTML');
    const error_text = else_body.findPropertyAssignment('error', 'textContent');
    const error_match = {
      'operator': '=',
      'right.name': 'message',
    };
    assert(matchObj(error_html, error_match) || matchObj(error_text, error_match), 'In the `else` statement, are you setting the HTML of `error` to `message`?');
  });

  it('Should append `save_btn` and return `item`. @append-save-btn-return', () => {
    const append_child = create_item.findCall('appendChild').nodes()[1];
    const append = create_item.findCall('append').nodes()[1];
    assert(append_child || append, 'Are you appending `save_btn` to `item`?');

    const append_child_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'appendChild',
      'arguments.0.name': 'save_btn'
    };
    const append_match = {
      'callee.object.name': 'item',
      'callee.property.name': 'append',
      'arguments.0.name': 'save_btn'
    };
    assert(match(append_child, append_child_match) || match(append, append_match), 'Below the `save_btn` event listener, are you appending `save_btn` to `item`?');

    const return_item = create_item.find(jscs.ReturnStatement, { argument: { type: 'Identifier', name: 'item'}});
    const return_item_match = {
      'type': 'ReturnStatement',
      'argument.name': 'item'
    };
    assert(matchObj(return_item, return_item_match), 'Has `item` been returned from the `create_item` function?');
  });

  it('Should register a listener for the `drop` event. @drop-event-listener', () => {
    const drop_match = {
      'callee.object.name': 'element',
      'callee.property.name': 'addEventListener',
    };
    assert(matchObj(drop, drop_match), 'Are you adding an event listener to `element` that listens for the `drop` event?');
    assert(matchObj(drop, { 'arguments.1.type': 'ArrowFunctionExpression' }) ||
           matchObj(drop, { 'arguments.1.type': 'FunctionExpression' }), 'Do you have a `drop` handler function?');
    assert(paramLength(drop), 'Does your handler function have a single parameter?');

    const prevent_default = drop.findCall('preventDefault');
    const prevent_default_match = {
      'callee.object.name': eventParamDrop,
      'callee.property.name': 'preventDefault',
    };
    assert(matchObj(prevent_default, prevent_default_match), 'Are you calling `preventDefault` on `event` in the `drop` handler function?');
  });

  it('Should get the `id` from `dataTransfer`. @drop-data-transfer-id', () => {
    assert(drop.length, 'Are you adding an event listener to `element` that listens for the `drop` event?');
    const id_get_data = drop.findVariable('id');
    const id_get_data_match = {
      'id.name': 'id',
      'init.callee.property.name': 'getData',
      'init.callee.object.object.name': eventParamDrop,
      'init.callee.object.property.name': 'dataTransfer',
      'init.arguments.0.value': 'text',
    };
    assert(matchObj(id_get_data, id_get_data_match), "Are you creating a constant called `id` and setting it to a call to `event.dataTransfer.getData()` and passing in `'text'`?");
  });

  it('Should append element. @drop-append-element', () => {
    assert(drop.length, 'Are you adding an event listener to `element` that listens for the `drop` event?');
    const drop_append_child = drop.findCall('appendChild');
    const drop_append_child_match = {
      'callee.object.object.name': eventParamDrop,
      'callee.object.property.name': 'target',
      'callee.property.name': 'appendChild',
      'arguments.0.callee.object.name': 'document',
      'arguments.0.callee.property.name': 'getElementById',
      'arguments.0.arguments.0.name': 'id'
    };
    assert(matchObj(drop_append_child, drop_append_child_match), 'Are you appending the element with the `id` of `id` (use: `document.getElementById`) to the `event.target`?');
  });

  it('Should register a listener for the `dragover` event. @drag-over-event-listener', () => {
    const drag_over = ast.findCall('forEach').findLiteral('dragover');
    const eventParamDrag = findEventParam(drop);
    const drag_over_match = {
      'callee.object.name': 'element',
      'callee.property.name': 'addEventListener',
    };
    assert(matchObj(drag_over, drag_over_match), 'Are you adding an event listener to `element` that listens for the `dragover` event?');

    const drag_over_handler_arrow = { 
      'arguments.1.type': 'ArrowFunctionExpression',
      'arguments.1.params.0.name': eventParamDrag
    };
    const drag_over_handler_function = { 
      'arguments.1.type': 'FunctionExpression',
      'arguments.1.params.0.name': eventParamDrag
    };
    assert(matchObj(drag_over, drag_over_handler_arrow) || matchObj(drag_over, drag_over_handler_function), 'Do you have a `dragover` handler function?');

    const prevent_default = drag_over.findCall('preventDefault');
    const prevent_default_match = {
      'callee.object.name': eventParamDrag,
      'callee.property.name': 'preventDefault',
    };
    assert(matchObj(prevent_default, prevent_default_match), 'Are you calling `preventDefault` on `event` in the `dragover` handler function?');
  });

});
