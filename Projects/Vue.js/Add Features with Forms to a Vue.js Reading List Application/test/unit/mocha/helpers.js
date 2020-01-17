const fs = require("fs");
const path = require("path");
const parse5 = require("parse5");

const readFile = function(filePath) {
  let file;
  try {
    file = fs.readFileSync(path.join(process.cwd(), filePath), "utf8");
  } catch (e) {
    assert(false, "The BookForm.vue file does not exist");
  }

  return file;
};

const parseFile = function(file) {
  const doc = parse5.parseFragment(file.replace(/\n/g, ""), {
    locationInfo: true
  });
  const nodes = doc.childNodes;

  return nodes;
};

const getHtmlTag = function(tagName, nodes) {
  return nodes.filter(node => node.nodeName === tagName);
};

module.exports.readFile = readFile;
module.exports.parseFile = parseFile;
module.exports.getHtmlTag = getHtmlTag;
