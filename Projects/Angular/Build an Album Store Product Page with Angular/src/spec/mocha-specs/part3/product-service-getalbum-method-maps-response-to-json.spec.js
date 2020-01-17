const assert = require("chai").assert;
const helpers = require("../helpers");
const { readFileSync } = require("fs");
const { tsquery } = require("@phenomnomnominal/tsquery");

describe("ProductService", () => {
  it("should map the result of get request to json with rxjs map function @product-service-getalbum-method-maps-response-to-json", () => {
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
      "It doesn't look like you are declaring `private _albumUrl` keyword and assigning the contents of the `album.json` file to it."
    );

    const albumUrlDeclaration = tsquery(
      ast,
      "PropertyDeclaration Identifier[name=_albumUrl]"
    );

    assert(
      albumUrlDeclaration.length > 0,
      "It doesn't look like you are declaring `private _albumUrl` keyword and assigning the contents of the `album.json` file to it."
    );

    const albumJsonFile = tsquery(
      ast,
      "PropertyDeclaration StringLiteral[value='../assets/album.json']"
    );

    assert(
      albumJsonFile.length > 0,
      "It doesn't look like you are declaring `private _albumUrl` keyword and assigning the contents of the `album.json` file to it."
    );

    const getAlbumMethod = tsquery(ast, 'Identifier[name="getAlbum"]');

    assert(
      getAlbumMethod.length > 0,
      "The ProductService doesn't have a method named `getAlbum()` yet."
    );

    const returnStatement = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) ReturnStatement"
    );

    assert(
      returnStatement.length > 0,
      "The `getAlbum()` doesn't have a `return` statement yet."
    );

    const thisStatement = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression ThisKeyword"
    );

    assert(
      thisStatement.length > 0,
      "It doesn't look like you're returning the result of calling `this._http.get()` and passing `this._albumUrl` as a parameter."
    );

    const httpGet = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression PropertyAccessExpression Identifier[name=_http]"
    );

    assert(
      httpGet.length > 0,
      "It doesn't look like you're returning the result of calling `this._http.get()`."
    );

    const getMethod = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum])  PropertyAccessExpression:has(Identifier[name=get])"
    );

    assert(
      getMethod.length > 0,
      "It looks like the `getAlbum` method is not sending a `GET` request."
    );

    assert(
      thisStatement.length > 1,
      "It doesn't look like you're returning the result of calling `this._http.get()` and passing `this._albumUrl` as a parameter."
    );

    const albumUrlArg = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression PropertyAccessExpression Identifier[name=_albumUrl]"
    );

    assert(
      albumUrlArg.length > 0,
      "It doesn't look like you're passing `this._albumUrl` as an argument to the `this._http.get()` method call."
    );

    const importRxjs = tsquery(
      ast,
      "ImportDeclaration StringLiteral[value=rxjs/add/operator/map]"
    );

    assert(
      importRxjs.length > 0,
      "The ProductService isn't importing `map` from `rxjs/add/operator/map`."
    );

    const mapMethod = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression Identifier[name=map]"
    );

    assert(
      mapMethod.length > 0,
      "It doesn't look like that you're chaining a `map` method after `this._http.get(this._albumUrl)`."
    );

    const responseParameter = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression:has(Identifier[name=map]) Parameter Identifier[name=response]"
    );

    assert(
      responseParameter.length > 0,
      "The `map` method should be taking `response` as an argument of its callback function."
    );

    const responseReturn = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression:has(Identifier[name=map]) PropertyAccessExpression Identifier[name=response]"
    );

    assert(
      responseReturn.length > 0,
      "The `map` method should be returning `response.json`. "
    );

    const jsonReturn = tsquery(
      ast,
      "MethodDeclaration:has(Identifier[name=getAlbum]) CallExpression:has(Identifier[name=map]) PropertyAccessExpression Identifier[name=json]"
    );

    assert(
      jsonReturn.length > 0,
      "The `map` method should be returning `response.json`. "
    );
  });
});
