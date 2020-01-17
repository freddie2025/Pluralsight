# Setup

## Installation

Double-click on the `R-ClassicBooks-Download.Rproj` in the root folder of the project. This will open `RStudio`.

To install the needed R packages for this project run the following command in the R console. 
*Note: Make sure you are in the `R console` and not the `terminal` when running this command.*

```
> packrat::restore()
```

This install process can take some time, when it is finished the prompt will return.
The console will list all of the packages that were installed.
You can also click on the `Packages` tab in the bottom right of `RStudio` to verify that the installation process was successful. 

## Verify Setup

In order to verify that everything is setup correctly, run the following command from the `R console` in `RStudio`.

`> .run$tests()`

This should show you the failing tests. This is good! We'll make these tests pass once we jump into the build step.

Every time you want to check your work locally you can type `.run$tests()` in the `R console`, and it will report the status of every task.

## Previewing Your Work

In order to see your work, you can click the `source` button at the top right of the `RStudio` `Editor` pane. Data frames can be viewed in the top right pane in the `Environments` tab and any plots will show in the bottom right pane in the `Plots` tab.

# Module 01 - Plotting Mark Twain's Download Statistics

## 1.1 - Find Twain's Works

Project Overview
-----
In this module we'll create a visualization that presents data about the Mark Twain's literary works.

The data set provided for this module is a single CSV files  called `books.csv`. The file was created using data compiled from Project Gutenberg. 

This data has already been imported and is stored in a variable called `books`.

We will filter the `books` data frame to only include the Twain's works. We remove duplicate works and then compare average sentence length, total sentences and downloads in a scatter plot.

We will use the tidyverse collection of packages. For more on the tidyverse and its opinionated design philosophy, grammar, and data structures see [Tidyverse](https://www.tidyverse.org/).

First Task
---
In RStudio open the file called `download.R` that is located in the root folder of the project. We will be working in this file for the duration of this module.

Using functions from the `dplyr` package find only those literary works authored by Twain. 

Save the resulting data frame as `twain`.

## 1.2 - Select Relevant Columns

The relevant columns for our plot are  `title`, `author`, `downloads`, `avg_words_per_sentence`, and `sentences`. Use a pipe `%>%` and the `select()` function to refine the `twain` data frame so that it only contains these columns. 

The resulting data frame should be called `twain_refined`.

## 1.3 - Arrange Books by Download

Using functions from the `dplyr` package order the `twain_refined` data frame by `download` descending. Save the resulting reordered data frame as `twain_by_download`.

## 1.4 - Create a Function

If you were to run `download.R` by clicking the `Source` button you would find the `twain_by_download` data frame in the `Environment` tab. Click the `twain_by_download` data frame to view it. You'll notice that there are some duplicates. For accuracy let's remove them. Best to use function for this.

Create a function called `unique_books`. Add two parameters to the parameters list. One for the data frame and another for the column to search for duplicates. This parameter should be set to the default value `'title'.

## 1.5 - Pull the Correct Column

In order to search for duplicates we need a list of values to search through.

In the body of the `unique_books` function take the correct column (second parameter) from the data frame (first parameter) and turn it into a list. 

The `dplyr` package has a great function for this. Save the new list as `items`. 

## 1.6 - Create an Empty List

Still in the body of the `unique_books` function create an empty listed called `duplicates` to store any possible duplicates.

## 1.7 - Create a For Loop

Now that we have a list of `items` to search in our function, let's loop through. Create a `for in` loop that cycles through the `items` list. Call the current loop value `item`.

## 1.8 - Fuzzy Matching

Because our books titles are not exactly the same but are still duplicates will need to use partial string matching. This can be done a number of different ways we will use the built-in function `agrep()`. For our same data set this will work fine. However, this would be quite slow for large data sets. 

In the body of the `for in` loop use the built-in function `agrep()` to test if the current `item` has a match in the `items` list. Assign the result of the call to `agrep()` to a variable named `match`.

To ensure that we have the record with the least amount of downloads store the last value from `match` list in a variable called `last`. *Note: R allows for negative numbers to select values from the end of a list.*

## 1.9 - Add Elements to a List

The `last` variable could contain the row index for a duplicate that needs to be removed or it could be `NULL`. 

To make sure that we aren't adding `NULL` values to our use an `if` statement to test the `length()` of `last`. 

In the body of the `if` statement use the double bracket `[[]]` syntax to add the `last` variable to the `duplicates` list. Use `last` as the name for the list element. **Hint: `list_name[[name]] <- element`.**

## 1.10 - Remove Duplicates

The `duplicates` variable contains a named listed. For the next operation few operations we need to flatten this list into a vector. There is a built-in function that does exactly that. 

Turn the `duplicates` list into a vector with the appropriate function. 

Wrap this call in the `unique` function to remove any possible duplicates.

Store this in a variable called `remove`.

The unary operator can be used to remove rows from a data frame. As the last line of the function us the proper syntax to remove the duplicates stored in `remove` from the data frame passed to the function.

## 1.11 - Call a Function

Now that we are finished with the `unique_books` function we can call it to remove duplicates from the `twain_by_download` data frame. 

Make a call the `unique_books` function with the correct arguments, save the result in a variable named `twain_unique`.

## 1.12 - Initialize a Plot Object

To construct a plot we will use the core function of the `ggplot2` library, `ggplot()`, which stands for grammar and graphics plot. You can find the relevant documentation here: [`ggplot()`](https://ggplot1.tidyverse.org/reference/ggplot.html).

Let's add a call to the core `ggplot()` function and save the results to a variable called `p`.

To view the plot in `RStudio`, on a new line call the `plot()` function and pass in `p` as an argument.

## 1.13 - Adding a Component
The call to `ggplot()` creates a plot object, in our case `p`. The call to `ggplot()` is almost always followed by a call to one or more `geom` functions. Each `geom` function creates a layer on the plot. 

Add a layer to the plot that has the mappings for a bar plot. This is done by adding the `geom_point()` function to the `ggplot()` function with the plus operator. **Hint: ggplot() + geom_function()**

## 1.14 - Aesthetic Mappings
Columns in our `twain_unique` data frame can be mapped to a layer using the `aes()` function.

Pass the `twain_unique` data frame as the first argument to the `ggplot()` function. 
The second argument should be a call to the `aes()` function. 

The following mappings should be passed to the `aes()` function. 

- The x-axis should be the `sentences`.
- The y-axis should be the `avg_words_per_sentence`.

## 1.15 - Geom Aesthetic Mappings

To adjust the points size on the plot lets add an aesthetic mapping to `geom_point()`. 

Pass the `geom_point()` function a call to the `aes()` function.
Pass the `aes()` function a named argument of `size` set equal to the `downloads` column.

To view the plot click the `Source` button in the upper right of the `Editor` pane.
