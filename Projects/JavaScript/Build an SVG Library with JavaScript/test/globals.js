const fs = require('fs');
const path = require('path');
const { assert } = require('chai');
const cheerio = require('cheerio');
const jscs = require('jscodeshift');
const dot = require('dot-object');

const source = fs.readFileSync(path.join(process.cwd(), 'js/sight.js'), 'utf8');
const ast = jscs(source);

jscs.registerMethods({
  findClass: function(name) {
    return this.find(jscs.ClassDeclaration).filter(path => (path.value.id.name === name));
  },
  findConditional: function() {
    return this.find(jscs.ConditionalExpression);
  },
  findCall: function(name) {
    return this.find(jscs.CallExpression).filter(path => {
        if (path.value.callee.property && path.value.callee.property.name === name) {
          return true;
        } else {
          return false;
        }
    });
  },
  findCalls: function() {
    return this.find(jscs.CallExpression);
  },
  findNew: function() {
    return this.find(jscs.NewExpression);
  },
  findVariableDeclarator: function(name) {
    return this.find(jscs.VariableDeclarator).filter(path => (path.value.id.name === name));
  },
});

jscs.registerMethods({
  findMethod: function(name) {
    return this.find(jscs.MethodDefinition).filter(path => (path.value.key.name === name));
  }
}, jscs.ClassDeclaration);

jscs.registerMethods({
  findParams: function() {
    return this.find(jscs.FunctionExpression).get().value.params;
  },
  findParamsMatch: function() {
    return this.find(jscs.FunctionExpression);
  },
  findAssignment: function(name, this_bool = false) {
    return this.find(jscs.AssignmentExpression).filter(path => {
      if (this_bool) {
        return (path.value.left.property.name === name && path.value.left.object.type === 'ThisExpression');
      } else {
        return path.value.left.property.name === name;
      }
    });
  },
  findAssignments: function() {
    return this.find(jscs.AssignmentExpression);
  },
  findReturn: function() {
    return this.find(jscs.ReturnStatement);
  },
  findForOf: function() {
    return this.find(jscs.ForOfStatement);
  },
  findVariable: function(name) {
    return this.find(jscs.VariableDeclaration, { declarations: [{ id: { name: name }}]});
  },
  findVariables: function() {
    return this.find(jscs.VariableDeclaration);
  }
}, jscs.MethodDefinition);

jscs.registerMethods({
  classVariable: function(name) {
    return this.find(jscs.MemberExpression).filter(path => (path.value.property.name === name && path.parent.value.type !== 'CallExpression'));
  }
}, jscs.AssignmentExpression);

const source_html = fs.readFileSync(path.join(process.cwd(), 'index.html'), 'utf8');
const $ = cheerio.load(source_html);
const script_ast = jscs($('script')[1].children[0].data);

const match = (obj, match_obj) => jscs.match(obj.get().value, dot.object(match_obj));


Object.assign(global, {
  assert,
  ast,
  dot,
  jscs,
  match,
  script_ast
});
