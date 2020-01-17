## Add a Script Element

In this module we will create a script that calculates the average rating for values 1 to 5.

Open the file called `index.html` in the root of the project.

Add a `<script>` element to the bottom of the `<body>` element that has a source of `js/ratings.js`.

## Collect Ratings Function

Open the main file for the project `js/ratings.js`.

At the top of this file, create a function called `collect_ratings()`. This function does not accept any arguments.

## Create a Ratings Object

In the body of the `collect_ratings()` function, create an object named `ratings`. The object should have three keys `count`, `sum`, and `average` all set to `0`.

## Create a Let Binding

Still in the body of the `collect_ratings()` function, create a variable with the let keyword called `rating`. Give it the initial value of `0`.

## Select HTML Elements

To calculate the average rating, we need the values of all input elements in the HTML that have a class of `rating`. Use the `querySelectorAll()` method of `document` to select all elements that have a class of `rating`. The `querySelectorAll()` method creates an array that should be stored in a constant called `elements`.

## Use the forEach Function

Cycle through the `elements` array with a `forEach` loop. Pass an anonymous or arrow function to the `forEach` loop. The function should accept a single argument called `element`.

## Set a rating

In the `forEach` loop function, call the `replace()` function on the `element.id`. To `replace()`, pass the correct arguments that replace the word `star` with nothing. Pass this statement to `parseInt()` to convert the remaining string to a number. All of this should be assigned to the existing `rating` variable.

## Set an Object Value - count

Still in the `forEach` loop function, pass `element.value` to the `parseInt()` function and use the addition assignment operator to assign the result to the `count` property of the `ratings` object.

## Set an Object Value - sum

Still in the `forEach` loop function, pass `element.value` to the `parseInt()` function and multiple it by `rating`. Use the addition assignment operator to assign the result to the `sum` property of the `ratings` object.

## Prevent Divide by Zero

Below the `forEach` loop, create an `if` statement that tests whether the `count` property of the `ratings` object is not equal to `0`.

## Calculate Average Rating

In the `if` statement, set the `average` property of the ratings property to `sum` divided by `count`.

## Return Ratings

Return the `ratings` object from the `collect_ratings()` function.

## Change Event - Handler

Below the `collect_ratings()` function, add an event listener that listens for the `change` event on `document`. When creating the event listener, pass an arrow function as the handler.

## Call Collect Ratings Function

In the change event listener handler, call the `collect_ratings()` function and store the result in a constant called ratings.

## Set value of Input Element

Still in the `change` event listener handler, use the `querySelector()` method to select the element that has an `id` of `average`. Set the `value` property of this element to `ratings.average`. Fix `ratings.average` to two decimal places.