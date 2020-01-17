const assert = require("chai").assert;
const esquery = require("esquery");
const esprima = require("esprima");
const helpers = require("../helpers");

describe("BookList.vue", () => {
  it("should import lodash @add-lodash-import", () => {
    const file = helpers.readFile("src/components/BookList.vue");
    const nodes = helpers.parseFile(file);
    const script = helpers.getHtmlTag("script", nodes);
    let computed;

    if (script.length == 0) {
      assert(
        false,
        "We either didn't find a `script` tag, or any code in a `script` tag in the `BookList` component."
      );
    }

    const ast = esprima.parse(script[0].childNodes[0].value, {
      sourceType: "module"
    });

    try {
      computed = esquery(ast, "Property[key.name=computed]");
    } catch (e) {
      assert(
        false,
        "Something went wrong and we weren't able to check your code."
      );
    }

    let importDeclaration = esquery(ast, "ImportDeclaration");

    assert(
      importDeclaration.length > 0,
      "The `BookList` component does not contain an `import` statement."
    );

    assert(
      importDeclaration[0].specifiers[0].local.name == "_",
      'The `BookList` component is not importing `_ from "lodash";`. Add `import _ from "lodash";` at the top of the `<script></script>` tag. '
    );

    importDeclaration = esquery(
      ast,
      "ImportDeclaration > Literal[value=lodash]"
    );

    assert(
      importDeclaration.length > 0,
      'The `BookList` component is not importing `_ from "lodash";`. Add `import _ from "lodash";` at the top of the `<script></script>` tag. '
    );
  });
});
