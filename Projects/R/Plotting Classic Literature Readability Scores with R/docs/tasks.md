# Setup

## Installation

Double-click on the `R-ClassicBooks-Readability.Rproj` in the root folder of the project. This will open `RStudio`.

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

# Module 01 - Plotting Popular Authors Readability Statistics

## 1.1 - Source Files

Project Overview
-----
In this module we'll create a visualization that presents data about the top ten authors readability.

The data set provided for this module is separated into two main CSV files. One called `titles.csv` and the other called `stats.csv`. Each file was created using data compiled from Project Gutenberg. 

This data has been joined together and is stored in a variable called `books`.

We will filter the `books` data frame to only include the works of the top ten authors and then plot it using the `ggplot2` package.

We will use the tidyverse collection of packages. For more on the tidyverse and its opinionated design philosophy, grammar, and data structures see [Tidyverse](https://www.tidyverse.org/).

First Task
---
In RStudio open the file called `reading.R` that is located in the root folder of the project. We will be working in this file for the duration of this module.

The plot for this module will show two reading ease metrics for the authors of the top ten most downloaded books on Project Gutenberg.

Use the `source()` function to import the prepared file `data.R`.

## 1.2 - Arrange Books by Download

Using functions from the `dplyr` package order the `books` data frame by `download` descending. Save the resulting reordered data frame as `books_by_download`.

## 1.3 - Select Relevant Columns

Our plot only requires columns that are used to calculate the two reading ease metrics. These columns are `author`, `title`, `words`, `syllables`, and `sentences`. Using a pipe `%>%` and the `select()` function refine the `books_by_download` data frame so that it only contains these columns. 

The resulting data frame should be called `books_refined`.

## 1.4 - Top Ten Authors

We need a `list` of the authors of the top ten books for our plot.  

Use the `head()` and `pull()` functions to get a `list` of the top ten authors from the `books_refined` data frame.

Save the resulting `list` as `top_ten_authors`.

## 1.5 - Filtering by the Top Ten Author

With the top ten authors stored in `top_ten_authors` we can find all the works by these authors.

Use the operator `%in%` inside a `filter()` function to filter the `books_refined` data frame to only contain the works of the authors in the `top_ten_authors` list. Arrange this new data frame by `author`.

Save the resulting list as `authors_books`.

[[IMAGE]]

## 1.6 - Adding a Flesch Reading Ease Column

One standard metric of readability is Flesch Reading Ease. The formula to calculate this metric is: `206.835 - 1.015 * (words / sentences) - 84.6 * (syllables / words)`.

Use the `mutate()` function to add a column to the `authors_books` data frame. The new column should be called `flesch_reading_ease` and it should be set equal to the formula above.

Save the resulting data frame as `reading_ease`.

## 1.7 - Adding a Flesch Reading Grade Level Column

Just for good measure let's also calculate the Flesch/Kincaid Grade Level metric.
The formula is: `0.39 * (words / sentences) + 11.8 * (syllables / words) - 15.59`.

Use the `mutate()` function to add a column to the `reading_ease` data frame. The new column should be called `flesch_kincaid_grade_level` and it should be set equal to the formula above.

Save the resulting data frame as `reading_grade`.

## 1.8 - Group by Author

To obtain an average for both readability metrics the data frame needs to be grouped by author behind the scenes.

Group the `reading_grade` data frame by `author`.

Save the resulting data frame as `reading_grouped`.

## 1.9 - Summarising Readability Metrics

The results of the `group_by()` function are not immediately apparent if we view the data frame.

We need to use the `summarise()` function to aggregate the data.

Use the `summarise()` and `mean()` functions to calculate the mean for both the `flesch_reading_ease` and `flesch_kincaid_grade_level` columns.

Save the resulting data frame as `reading_summary`.

## 1.10 - Reshaping a Data Frame

Later on we are going to create a faceted bar plot. We need to use `gather()` to reshape the `reading_summary` data frame for that type of plot.

The `gather()` function requires the that you provide the names for the columns that are created as part of the reshaping process. 

The first column should be `type` and the second should be `score`. *Note: Remember to quote these column names.*

The last argument to the `gather()` function is the column or columns to reshape. Select both the `flesch_reading_ease` and `flesch_kincaid_grade_level` columns.

Save this new data frame to a variable named `reading_long`.

## 1.11 - Initialize a Plot Object

To construct a plot we will use the core function of the `ggplot2` library, `ggplot()`, which stands for grammar and graphics plot. You can find the relevant documentation here: [`ggplot()`](https://ggplot1.tidyverse.org/reference/ggplot.html).

Let's add a call to the core `ggplot()` function and save the results to a variable called `p`.

To view the plot in `RStudio`, on a new line call the `plot()` function and pass in `p` as an argument.

## 1.12 - Adding a Component
The call to `ggplot()` creates a plot object, in our case `p`. The call to `ggplot()` is almost always followed by a call to one or more `geom` functions. Each `geom` function creates a layer on the plot. 

Add a layer to the plot that has the mappings for a bar plot. This is done by adding the `geom_bar()` function to the `ggplot()` function with the plus operator. **Hint: ggplot() + geom_function()**

To ensure the correct mappings, `geom_bar()` requires a named argument `stat` with a value of `identity` be passed in.

## 1.13 - Aesthetic Mappings
Columns in our `reading_long` data frame can be mapped to a layer using the `aes()` function.

Pass the `reading_long` data frame as the first argument to the `ggplot()` function. 
The second argument should be a call to the `aes()` function. 

The following mappings should be passed to the `aes()` function. 

- The x-axis should be the `author`.
- The y-axis should be the `score`.

To view the plot click the `Source` button in the upper right of the `Editor` pane.

## 1.14 - Faceting the Bar Plot

The current plot is adding the two different scores `flesch_reading_ease` and `flesch_kincaid_grade_level` together on one plot.

We can use a facet grid to separate out these two scores.

Above the call to the `plot()` function, add the function call `facet_grid()` to `p`.

Pass `facet_grid()` the keyword argument `row` set equal to `vars(type)`.

Save this statement back to `p`.  **Hint: plot <- plot + function(argument)**

## 1.15 - Customizing the Plot

There are several ways that we can customize the plot. We are going to do something really simple, but this is just the beginning.

Above the call to the `plot()` function, and after the line with a call to `facet_grid()`, add a call of `theme()` to `p`.

Pass `theme()` the correct named arguments to rotate the x axis labels `45` degrees. You will also need to set the `hjust` to `1`.

Save this statement back to `p`.