const fs = require('fs');
const path = require('path');
const request = require('supertest');
const rewire = require('rewire');
const chai = require('chai');
const sinon = require('sinon');
const sinonChai = require('sinon-chai');
const { mockReq, mockRes } = require('sinon-express-mock');

const temp_app = fs.readFileSync(path.join(process.cwd(), 'src/app.js'), 'utf8');
const overwritten = temp_app.replace(/app\.listen\([\s\S]*\);?/g, 'app.listen(3000);')
fs.writeFileSync(path.join(process.cwd(), 'src/temp_app.js'), overwritten , 'utf8')
const appModule = rewire(path.join(process.cwd(), 'src/temp_app.js'));
fs.unlinkSync(path.join(process.cwd(), 'src/temp_app.js'))
chai.use(sinonChai);

let app;
try {
  app = appModule.__get__('app');
} catch (err) {
  app = undefined;
}

const getRouteMethods = route => {
  const methods = [];
  for (const method in route.methods) {
    if (method === '_all') {
      continue;
    }
    methods.push(method);
  }
  return methods;
};
const hasParams = value => {
  const regExp = /\(\?:\(\[\^\\\/]\+\?\)\)/g;
  return regExp.test(value);
};
const getAllStacks = (app, path, endpoints) => {
  if (typeof app === 'undefined') {
    return undefined;
  }

  const regExp = /^\/\^\\\/(?:(:?[\w\\.-]*(?:\\\/:?[\w\\.-]*)*)|(\(\?:\(\[\^\\\/]\+\?\)\)))\\\/.*/;
  const stack = app.stack || (app._router && app._router.stack);

  if (typeof stack === 'undefined') {
    return undefined;
  }

  endpoints = endpoints || [];
  path = path || '';

  stack.forEach(val => {
    if (val.route) {
      endpoints.push({
        path: getRouteMethods(val.route)[0] + ' ' + path + (path && val.route.path === '/' ? '' : val.route.path),
        stack: val.route.stack[0]
      });
    } else if (val.name === 'router' || val.name === 'bound dispatch') {
      let newPath = regExp.exec(val.regexp);

      if (newPath) {
        let parsedRegexp = val.regexp;
        let keyIndex = 0;

        while (hasParams(parsedRegexp)) {
          parsedRegexp = parsedRegexp.toString().replace(/\(\?:\(\[\^\\\/]\+\?\)\)/, ':' + val.keys[keyIndex].name);
          keyIndex++;
        }

        if (parsedRegexp !== val.regexp) {
          newPath = regExp.exec(parsedRegexp);
        }

        const parsedPath = newPath[1].replace(/\\\//g, '/');
        getAllStacks(val.handle, path + '/' + parsedPath, endpoints);
      } else {
        getAllStacks(val.handle, path, endpoints);
      }
    }
  });
  return endpoints;
};

const routeStack = (path, method) => {
  const allStacks = getAllStacks(app) || [];
  let found;
  allStacks.forEach(stack => {
    if (stack.path === method + ' ' + path) {
      found = stack.stack;
    }
  });
  return found || undefined;
};

Object.assign(global, {
  assert: chai.assert,
  expect: chai.expect,
  request,
  sinon,
  appModule,
  app,
  routeStack,
  mockReq,
  mockRes
});
