describe('Module 01 - SVG Library - sight.js', () => {
  const svgelement = ast.findClass('SVGElement');
  const svgelement_constructor = (svgelement.length) ? svgelement.findMethod('constructor') : [];
  const svgelement_constructor_assignments = (svgelement_constructor.length) ? svgelement_constructor.findAssignments() : [];
  const attr = (svgelement.length) ? svgelement.findMethod('attr') : [];
  const for_of = (attr.length) ? attr.findForOf() : [];
  const append = (svgelement.length) ? svgelement.findMethod('append') : [];

  const sight = ast.findClass('Sight');
  const sight_constructor = (sight.length) ? sight.findMethod('constructor') : [];
  const draw = (sight.length) ? sight.findMethod('draw') : [];

  it('Should have an `SVGElement` class. @svgelement-class', () => {
    assert(svgelement.length, 'Have you created the `SVGElement` class?');
  });

  it('Should have an `SVGElement` class `constructor`. @svgelement-constructor', () => {
    assert(svgelement.length, 'Have you created the `SVGElement` class?');
    assert(svgelement_constructor.length, 'Does the `SVGElement` class have a `constructor`?');
    const params = svgelement_constructor.findParams();
    assert(params.length != 0 && params[0].name == 'type', 'Does the `SVGElement` class `constructor` have a parameter of `type`?');
  });

  it('Should set `constructor` properties. @svgelement-constructor-class-properties', () => {
    assert(svgelement_constructor_assignments.length, 'Are you setting the correct class properties in the constructor?');
    const type = svgelement_constructor_assignments.classVariable('type');
    const type_right = type.length ? type.get().parent.value.right.name : false;
    assert(type_right == 'type', 'Have you set `this.type` equal to `type`?');

    const namespace = svgelement_constructor_assignments.classVariable('namespace');
    const namespace_right = namespace.length ? namespace.get().parent.value.right.value : false;
    assert(namespace_right == 'http://www.w3.org/2000/svg', 'Have you set `this.namespace` equal to `http://www.w3.org/2000/svg`?');
  });

  it('Should create an NS element in the `SVGElement` `constructor`. @svgelement-constructor-createelementns', () => {
    assert(svgelement_constructor_assignments.length, 'Do you have an `SVGElement` class constructor?');
    const node = svgelement_constructor_assignments.classVariable('node').at(0);
    assert(node.length, 'Are you setting `this.node`?');

    const create_element = svgelement_constructor.findCall('createElementNS');
    const create_element_left = create_element.length ? create_element.get().parent.value.left : false;
    const create_element_right = create_element.length ? create_element.get().parent.value.right : false;

    assert(create_element_left && create_element_right &&
           create_element_left.object.type == 'ThisExpression' && 
           create_element_left.property.name == 'node' && 
           create_element_right.callee.object.name == 'document', 'Are you assigning `this.node` a call to `document.createElementNS()`?');

    assert(create_element_right.arguments.length >= 1 &&
           create_element_right.arguments[0].object.type == 'ThisExpression' &&
           create_element_right.arguments[0].property.name == 'namespace' &&
           create_element_right.arguments[1].object.type == 'ThisExpression' &&
           create_element_right.arguments[1].property.name == 'type', 'Are you passing `document.createElementNS()` the correct arguments?');

    assert(svgelement_constructor.length, 'Do you have an `SVGElement` class constructor?');
    const return_statement = svgelement_constructor.findReturn();
    const return_right = return_statement.length ? return_statement.get().value.argument.type : false;
    assert(return_right == 'ThisExpression', 'Does the `SVGElement` `constructor` `return this`?');
  });

  it('Should have an `SVGElement` `attr` class method. @svgelement-attr', () => {
    assert(svgelement.length, 'Have you created the `SVGElement` class?');
    assert(attr.length, 'Have you created an `attr` method in the `SVGElement` class?');
    
    const params = attr.findParams();
    assert(params.length != 0 && params[0].name == 'attrs', 'Does the `attr` method have a parameter of `attrs`?');

    const return_statement = attr.findReturn();
    const return_right = return_statement.length ? return_statement.get().value.argument.type : false;
    assert(return_right == 'ThisExpression', 'Does the `SVGElement` `attr` method `return this`?');
  });

  it('Should have a `forEach` loop. @svgelement-attr-for', () => {
    assert(for_of.length, 'Have you created a `for of` statement in the `attr` method?');
    const for_of_left = for_of.length ? for_of.get().value.left : false;
    const for_of_right = for_of.length ? for_of.get().value.right : false;
    const for_of_body = for_of.length ? for_of.get().value.body.body : false;
    assert(for_of_left &&
           for_of_left.declarations.length >= 1 &&
           for_of_left.declarations[0].id.type == 'ArrayPattern' &&
           for_of_left.declarations[0].id.elements[0].name == 'key' &&
           for_of_left.declarations[0].id.elements[1].name == 'value', 'Have you defined an array with `key` and `value` in the first part of the for loop?');

    assert(for_of_right && for_of_right.type == 'CallExpression' &&
           for_of_right.callee.object.name == 'Object' &&
           for_of_right.callee.property.name == 'entries' &&
           for_of_right.arguments.length >= 1 &&
           for_of_right.arguments[0].name == 'attrs', 'Have you called the `Objects.entries()` function with the correct arguments?');

    assert(for_of_body.length, 'Does the `forEach` loop have the correct statements?');
    assert(for_of_body[0].expression.callee.object.object.type == 'ThisExpression' &&
           for_of_body[0].expression.callee.object.property.name == 'node' &&
           for_of_body[0].expression.callee.property.name == 'setAttributeNS', 'In the body of the `for` loop do you have a call to `setAttributeNS()`?');

    assert(for_of_body[0].expression.arguments.length >= 1 &&
           for_of_body[0].expression.arguments[0].value == null  &&
           for_of_body[0].expression.arguments[1].name == 'key'  &&
           for_of_body[0].expression.arguments[2].name == 'value', 'Does the `setAttributeNS()` function have the correct arguments?');
  });
  
  it('Should have an `SVGElement` `append` class method. @svgelement-append', () => {
    assert(svgelement.length, 'Have you created the `SVGElement` class?');
    assert(append.length, 'Does the `SVGElement` class have a `append` method?');
    const params = append.findParams();
    assert(params.length != 0 && params[0].name == 'element', 'Does the `append` method have a parameter of `element`?');

    const return_statement = append.findReturn();
    const return_right = return_statement.length ? return_statement.get().value.argument.type : false;
    assert(return_right == 'ThisExpression', 'Does the `SVGElement` `append` `return this`?');
  });
  
  it('Should get parent element in `append` method. @svgelement-append-get-parent', () => {
    assert(append.length, 'Does the `SVGElement` class have a `append` method?');
    const parent_const = append.findVariable('parent');
    const parent_declarator = parent_const.length ? parent_const.get().value.declarations : false;
    assert(parent_declarator.length, 'Do you have the right declarations in the `append` method?');
    assert(parent_declarator[0].id.name == 'parent', 'Do you have a constant named `parent`?');

    const parent_conditional = parent_const.findConditional()
    const parent_test = parent_conditional.length ? parent_conditional.get().value.test : false;
    const parent_consequent = parent_conditional.length ? parent_conditional.get().value.consequent : false;
    const parent_alternate = parent_conditional.length ? parent_conditional.get().value.alternate : false;

    assert(parent_consequent &&
           parent_consequent.callee.object.name === 'document' &&
           parent_consequent.callee.property.name === 'querySelector', 'If the condition is `true` are you setting `parent` equal to a call to `document.querySelector()`?');

    assert(parent_consequent.arguments.length != 0 && parent_consequent.arguments[0].name === 'element', 'If the condition is `true`, are you passing `element` to a call to `document.querySelector()`?');
    assert(parent_alternate &&
           parent_alternate.object.name === 'element' &&
           parent_alternate.property.name === 'node', 'If the condition is `false` are you setting `parent` equal to `element.node`?');
    assert(parent_test.operator === '===', 'Are you using the strict equality operator `===`?');
    assert(parent_test.left.operator === 'typeof' && 
           parent_test.left.argument.name === 'element' && 
           parent_test.right.value === 'string', 'Does your conditional test if `element` is of the type `string`?');
  });
  
  it('Should `appendChild` in `append` method . @svgelement-append-appendchild', () => {
    assert(append.length, 'Does the `SVGElement` class have a `append` method?');
    const ac = append.findCall('appendChild');
    const ac_callee = ac.length ? ac.get().value.callee : false;
    const ac_arguments = ac.length ? ac.get().value.arguments[0] : false;

    assert(ac_callee, 'Are you calling the `appendChild` method?');
    assert(ac_callee.object.name === 'parent' &&
           ac_arguments.object.type === 'ThisExpression' &&
           ac_arguments.property.name === 'node', 'Are you appending `this.node` to `parent`?');
  });

  it('Should have a `Sight` class. @sight-class', () => {
    assert(sight.length, 'Have you created the `Sight` class?');
  });

  it('Should have a `Sight` class `constructor`. @sight-constructor', () => {
    assert(sight.length, 'Have you created the `Sight` class?');
    assert(sight_constructor.length, 'Does the `Sight` class have a `constructor`?');
    const params = sight_constructor.findParams();
    assert(params.length && params[0].name == 'selector', 'Does the `Sight` class `constructor` have a parameter of `selector`?');
    assert(params.length >= 2 && params[1].name == 'width', 'Does the `Sight` class `constructor` have a parameter of `width`?');
    assert(params.length >= 3 && params[2].name == 'height', 'Does the `Sight` class `constructor` have a parameter of `height`?');
  });

  it('Should create a `new` `SVGElement` instance. @sight-constructor-new', () => {
    assert(sight_constructor.length, 'Does the `Sight` class have a `constructor`?');
    const svg_assignment = sight_constructor.findAssignment('svg', true);
    assert(svg_assignment.length, 'Are you assigning the instance property `this.svg`?');
    const svg_new_match = {
      'type': 'NewExpression',
      'callee.name': 'SVGElement',
      'arguments.0.value': 'svg'
    };
    assert(svg_assignment.findNew().length && match(svg_assignment.findNew(), svg_new_match), 'Are you assigning `this.svg` a `new SVGElement()` instance and are you passing in `svg`?');

    const svg_attr_match = {
      'arguments.0.type': 'ObjectExpression',
      'arguments.0.properties.0.key.name': 'viewbox',
      'arguments.0.properties.0.value.expressions.0.name': 'width',
      'arguments.0.properties.0.value.expressions.1.name': 'height',
      'arguments.0.properties.0.value.quasis.0.value.raw': '0 0 '
    };
    assert(svg_assignment.findCall('attr').length && match(svg_assignment.findCall('attr'), svg_attr_match), 'Are you chaining a call to `attr` on `new SVGElement()`? Are you passing in an object with a `viewbox` key and a value of ``0 0 ${width} ${height}``?');

    const svg_append_match = {
      'callee.property.name': 'append',
      'arguments.0.name': 'selector'
    };
    assert(svg_assignment.findCall('append').length && match(svg_assignment.findCall('append'), svg_append_match), 'Are you chaining a call to `append` on `new SVGElement()`? Are you passing in `selector`?');
  });

  it('Should have a `Sight` `draw` class method. @sight-draw', () => {
    assert(sight.length, 'Have you created the `Sight` class?');
    assert(draw.length, 'Does the `Sight` class have a `draw` method?');
    const params = draw.findParams();
    assert(params.length && params[0].name == 'type', 'Does the `draw` method have a parameter of `type`?');
    assert(params.length  >= 2 && params[1].name == 'attrs', 'Does the `draw` method have a parameter of `attrs`?');

    assert(draw.length, 'Does the `Sight` class have a `draw` method?');
    const draw_return = draw.findReturn();

    assert(draw_return.length, 'Does the `draw` method have a return statement?');
    const return_new = draw_return.findNew();
    const return_new_match = {
      'type': 'NewExpression',
      'callee.name': 'SVGElement',
      'arguments.0.name': 'type'
    };
    assert(match(return_new, return_new_match), 'Are you returning a `new SVGElement()` instance and are you passing in `type`?');

    const return_new_attr_match = {
      'type': 'CallExpression',
      'callee.property.name': 'attr',
      'arguments.0.name': 'attrs',
    };
    assert(draw_return.findCall('attr').length && match(draw_return.findCall('attr'), return_new_attr_match), 'Are you chaining a call to `attr` on `new SVGElement()`? Are you passing in `attrs`?');

    const return_new_append_match = {
      'type': 'CallExpression',
      'callee.property.name': 'append',
      'arguments.0.object.type': 'ThisExpression',
      'arguments.0.property.name': 'svg',
    };
    assert(draw_return.findCall('append').length && match(draw_return.findCall('append'), return_new_append_match), 'Are you chaining a call to `append` on `new SVGElement()`? Are you passing in `this.svg`?');
  });

  it('Should create a `Sight` instance. @html-create-sight-instance', () => {
    const sight_new = script_ast.findVariableDeclarator('svg');
    assert(sight_new.length, 'Do you have a constant named `svg` in the HTML `script` block?');
    const sight_new_match = {
      'id.name': 'svg',
      'init.type': 'NewExpression',
      'init.callee.name': 'Sight'
    };
    assert(match(sight_new, sight_new_match), 'Are you assigning the `svg` constant a `new` `Sight` instance?');

    const sight_new_attr_match = {
      'init.arguments.0.value': '.svg',
      'init.arguments.1.value': 400,
      'init.arguments.2.value': 400
    };
    assert(match(sight_new, sight_new_attr_match), 'Are you passing the `Sight` instance the correct arguments?');
  });

  it('Should use the `draw` method. @html-draw-method', () => {
    const draw_call = script_ast.findCall('draw');
    assert(draw_call.length, 'Have you called the `draw` method in the HTML `script` block?');
    const arguments = draw_call.get().value.arguments;
    assert(arguments.length === 2, 'Are you passing the correct arguments to the `draw` method?');
    assert(arguments[0].value === 'circle', 'Is the first argument `circle`?');
    assert(arguments[1].properties.length === 3, 'Do you have 3 key value pairs in the object?');

    const properties = {};
    arguments[1].properties.map(property => {
      if (property.key.name) {
        properties[property.key.name] =  property.value.value;
      } else if (property.key.value) {
        properties[property.key.value] =  property.value.value;
      }
    });
    const matched = JSON.stringify(properties) === JSON.stringify({ cx: 50, cy: 50, r: 50 })
    assert(matched, 'Are you passing an object with the correct attributes?');
  });
});