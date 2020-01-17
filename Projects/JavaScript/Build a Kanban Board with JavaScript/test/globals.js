const { assert } = require('chai');
const dot = require('dot-object');
const fs = require('fs');
const path = require('path');
const jscs = require('jscodeshift');

const source = fs.readFileSync(path.join(process.cwd(), 'js/kanban.js'), 'utf8');
const ast = jscs(source);

jscs.registerMethods({
  findFunction: function(name) {
    const element = this.find(jscs.Identifier, { name: name }).filter(path => {
      if(path.parent.value.type === 'VariableDeclarator') {
        if (path.parent.value.init.type === 'FunctionExpression' || 
            path.parent.value.init.type === 'ArrowFunctionExpression') {
          return true;
        }
        return false;
      } else if (path.parent.value.type === 'FunctionDeclaration') {
        return true;
      } else {
        return false;
      }
    });
    return (element.length) ? jscs(element.get().parent) : [];
  },
  findVariable: function(name) {
    return this.find(jscs.VariableDeclarator).filter(path => (path.value.id.name === name));
  },
  findPropertyAssignment: function(obj, property) {
    return this.find(jscs.AssignmentExpression).filter(path => {
      if (path.value.left.type === 'MemberExpression' &&
          path.value.left.object.name === obj &&
          path.value.left.property.name === property) {
        return true;
      } else {
        return false;
      }
    });
  },
  findAssignment: function(name) {
    return this.find(jscs.AssignmentExpression).filter(path => (path.value.left.type === 'Identifier' && path.value.left.name === name));
  },
  findUpdate: function(name, operator) {
    return this.find(jscs.UpdateExpression).filter(path => (path.value.argument.name === name && path.value.operator === operator));
  },
  findCall: function(name) {
    return this.find(jscs.CallExpression).filter(path => {
      let callee_name = '';
      if (path.value.callee.type === 'Identifier') {
        callee_name = path.value.callee.name;
      } if (path.value.callee.type === 'MemberExpression') {
        callee_name = path.value.callee.property.name;
      }
      return (callee_name === name) ? true : false;
    }); 
  },
  findIf: function() {
    const element = this.find(jscs.IfStatement);
    return (element.length) ? element.get().value : [];
  },
  findReturn: function() {
    return this.find(jscs.ReturnStatement);
  },
  findLiteral: function(name) {
    const element = this.find(jscs.Literal).filter(path => (path.value.value === name));
    return (element.length) ? jscs(element.get().parent) : [];
  }
});

const matchObj = (obj, match_obj) => ((obj.length) ? jscs.match(obj.get().value, dot.object(match_obj)) : false);
const paramLength = (obj) => ((obj.length) ? ((obj.get().value.arguments.length) ? true : false) : false);

const checkNested = (obj, level, ...rest) => {
  if (obj === undefined) return false
  if (rest.length == 0 && obj.hasOwnProperty(level)) return true
  return checkNested(obj[level], ...rest)
};

const mEqual = (...rest) => rest.every((v, i, a) => v === a[0] && v !== null);

const matchParam = (obj, other, level = false) => {
  if (obj.length && other.length) {
    const obj_value = obj.get().value;
    const other_value = other.get().value;
    if (checkNested(obj_value, 'arguments') &&
      obj_value.arguments.length >= 2 &&
      checkNested(other_value, 'callee', 'object', 'object', 'name') && 
      obj_value.arguments[1].params.length) {
      if (level) {
        if (checkNested(other_value.arguments[1], 'object', 'object', 'name')) {
          return mEqual(obj_value.arguments[1].params[0].name, other_value.callee.object.object.name, other_value.arguments[1].object.object.name);
        } else {
          return false;
        }
      } else {
        return mEqual(obj_value.arguments[1].params[0].name, other_value.callee.object.object.name);
      }
    } else {
      return false;
    }
  } else {
    return false;
  }
};

const findEventParam = (obj) => {
  if (obj.length) {
    const obj_value = obj.get().value;
    return (checkNested(obj_value, 'arguments') && obj_value.arguments.length >= 2 && obj_value.arguments[1].params.length) ? obj_value.arguments[1].params[0].name : false;
  } else {
    return false;
  }
};

const match = (obj, match_obj) => jscs.match(obj, dot.object(match_obj));

Object.assign(global, {
  assert,
  ast,
  dot,
  jscs,
  match,
  matchObj,
  matchParam,
  findEventParam,
  paramLength
});
