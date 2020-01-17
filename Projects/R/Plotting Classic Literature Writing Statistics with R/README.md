# Plotting Classic Literature Writing Statistics with R - DRAFT

## Setup

### Installation

Double-click on the `R-ClassicBooks.Rproj` in the root folder of the project. This will open `RStudio`.

To install the needed R packages for this project run the following command in the R console. 
*Note: Make sure you are in the `R console` and not the `terminal` when running this command.*

```
> packrat::restore()
```

This install process can take some time, when it is finished the prompt will return.
The console will list all of the packages that were installed.
You can also click on the `Packages` tab in the bottom right of `RStudio` to verify the install process was successful. 

### Verify Setup

In order to verify that everything is setup correctly, run the following command from the `R console` in `RStudio`.

`> .run$tests()`

This should show you the failing tests. This is good! We'll be fixing these tests once we jump into the build step.

Every time you want to check your work locally you can type `.run$tests()` in the `R console`, and it will report the status of every task.

### Previewing Your Work

In order to see your work, you can click the `source` button at the top right of the `RStudio` editor window. Data frames can be viewed in the top right and any plots will show in the bottom right in the `Plots` tab.
