## SVGElement Class

In this module we will create a library that allows users of the library to create any type of SVG element and configure its attributes.

To start, open the file `js/sight.js` and at the top add a `class` called `SVGElement`.

## SVGElement Class - Constructor

In the newly created `SVGElement` class, create a `constructor` that has a single parameter of `type`.

## SVGElement Class - Constructor Class Properties

In the `SVGElement` class constructor, create a class property called `type`, set it equal to `type`.

Also, assign a class property called `namespace` the value `'http://www.w3.org/2000/svg'`.

**Hint: always use `this` when assigning class properties.**

## SVGElement Class - Constructor Create ElementNS

In the `SVGElement` class constructor, assign a class property called `node` a call to the function `document.createElementNS()`.

Pass in the `namespace` and `type` class properties to the function call.

Return `this` at the end of the constructor.

## SVGElement Class - Attr Method

Below the constructor, still in the `SVGElement` class, create a class method named `attr` that accepts a parameter of `attrs`.

In the body of the method, return `this`.

## SVGElement Class - Attr Method For Loop

The `attrs` parameter should be an object. We need to loop through the object and get the key and value for each pair.

The most efficient way to do this is using the new `Object.entries()` method and a `for of` loop.

**Hint: Make sure you use array notation on the left side of the `of` keyword. Name the first element `key` and the second element `value`.**

In the body of the `for of` loop, call `setAttributeNS()` on `this.node`. Pass the correct arguments to the function. **Hint: `null` should be the first argument.**

## SVGElement Class - Append Method

Below the `attr` method, still in the `SVGElement` class, create a class method named `append` that accepts a parameter of `element`.

In the body of the method, return `this`.

## SVGElement Class - Append Method Get Parent

In the `append` method, we would like to detect whether the `element` being passed in is a string.

In a ternary statement, use the `typeof` operator to check if `element` is of type `string`.

In the `if` block, use `document.querySelector()` to select the `element`.

In the `else` block get the `element` `node`.

Assign the statement to the constant `parent`.

## SVGElement Class - Append Method Append Child

Append `this.node` to `parent` using the correct DOM method.

## Sight Class

Add a `class` called `Sight` below the `SVGElement` class.

## Sight Class - Constructor

In the `Sight` class, create a `constructor` that has three parameters: `selector`, `width`, `height`.

## Sight Class - Constructor New SVGElement Instance

In the body of the `Sight` class `constructor`, assign a class property of `svg` a `new` instance of the SVGElement class. Pass the type of `'svg'` to the `constructor`.

Next, on the instance, chain a call to the `attr` method. Pass an object to the `attr` method that has a single key value pair. The key should be `viewbox` and the value should be a template literal that would create a string with the format `0 0 width height`.

After the `attr` method, chain a call to the `append` method. Pass the `append` method `selector`.

## Sight Class - Draw Method

The last method of our library will draw an SVG element to the root SVG element.

Create this method called `draw` below the `Sight` class `constructor`. This draw method should accept the `type` of element to be drawn and the attributes(`attrs`) of the element.

In the body of the `draw` method, return a new `SVGElement` instance. Pass `type` to the instance.

Chain a call to the `attr` method, passing in `attrs`.

Finally chain another call to the `append` method, passing in the `svg` class property.

## Create Sight Instance

We will now use our SVG Library to create a circle.

Open the `index.html` file in the root directory, and find the empty `<script>` block at the bottom of the `<body>`.

In the `<script>` block, create a new `Sight` instance, passing the instance the selector `'.svg'`, and the width and height of `400`. Save the instance to a constant called `svg`.

## Draw Method

The `svg` constant is now an instance of the `Sight` class. Call the `draw` method on this instance to draw a `circle`. Pass the `draw` method an object with the key value pairs for these attributes: `cx`, `cy` and `r` all set to `50`.