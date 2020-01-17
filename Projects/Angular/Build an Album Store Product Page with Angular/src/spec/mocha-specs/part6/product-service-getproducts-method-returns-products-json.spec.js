const assert = require("chai").assert;
const helpers = require("../helpers");
const { readFileSync } = require("fs");
const { tsquery } = require("@phenomnomnominal/tsquery");

describe("ProductService", () => {
  it("should return contents of _productsUrl when getProducts() method called @product-service-getproducts-method-returns-products-json", () => {
    const fileName = "src/app/product.service.ts";

    helpers.readFile(
      fileName,
      "The ProductService hasn't been created yet. - have you run the `ng` command to generate it yet?"
    );

    //https://medium.com/@phenomnominal/easier-typescript-tooling-with-tsquery-d74f04f2b29d
    const ast = tsquery.ast(readFileSync(fileName).toString());

    const privateDeclaration = tsquery(
      ast,
      "PropertyDeclaration PrivateKeyword"
    );

    assert(
      privateDeclaration.length > 0,
      "It doesn't look like you are declaring `private _productsUrl` keyword and assigning the contents of the `products.json` file to it."
    );

    const productsUrlDeclaration = tsquery(
      ast,
      "PropertyDeclaration Identifier[name=_productsUrl]"
    );

    assert(
      productsUrlDeclaration.length > 0,
      "It doesn't look like you are declaring `private _productsUrl` keyword and assigning the contents of the `products.json` file to it."
    );

    const productsJsonFile = tsquery(
      ast,
      "PropertyDeclaration StringLiteral[value='../assets/products.json']"
    );

    assert(
      productsJsonFile.length > 0,
      "It doesn't look like you are declaring `private _productsUrl` keyword and assigning the contents of the `products.json` file to it."
    );

    const getProductsMethod = tsquery(ast, 'Identifier[name="getProducts"]');

    assert(
      getProductsMethod.length > 0,
      "The ProductService doesn't have a method named `getProducts()` yet."
    );

    const returnStatement = tsquery(
      ast,
      "MethodDeclaration:has( Identifier[name=getProducts] ) ReturnStatement"
    );

    assert(
      returnStatement.length > 0,
      "The `getProducts()` doesn't have a `return` statement yet."
    );

    const thisStatement = tsquery(
      ast,
      "MethodDeclaration:has( Identifier[name=getProducts] ) CallExpression ThisKeyword"
    );

    assert(
      thisStatement.length > 0,
      "It doesn't look like you're returning the result of calling `this._http.get()` and passing `this._productsUrl` as a parameter."
    );

    const httpGet = tsquery(
      ast,
      "MethodDeclaration:has( Identifier[name=getProducts] ) CallExpression PropertyAccessExpression Identifier[name=_http]"
    );

    assert(
      httpGet.length > 0,
      "It doesn't look like you're returning the result of calling `this._http.get()`."
    );

    const getMethod = tsquery(
      ast,
      "MethodDeclaration:has( Identifier[name=getProducts] )  PropertyAccessExpression:has(Identifier[name=get]) "
    );

    assert(
      getMethod.length > 0,
      "It looks like the `getProducts` method is not sending a `GET` request."
    );

    assert(
      thisStatement.length > 1,
      "It doesn't look like you're returning the result of calling `this._http.get()` and passing `this._productsUrl` as a parameter."
    );

    const productsUrlArg = tsquery(
      ast,
      "MethodDeclaration:has( Identifier[name=getProducts] ) CallExpression PropertyAccessExpression Identifier[name=_productsUrl]"
    );

    assert(
      productsUrlArg.length > 0,
      "It doesn't look like you're passing `this._productsUrl` as an argument to the `this._http.get()` method call."
    );
  });
});
