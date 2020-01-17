library(rlang)
library(glue)
library(stringi)

setwd('..')

solution <- new.env()
source('tests/solution.R', local = solution)

user <- new.env()
source('download.R', local = user)

parsed <- parse_exprs(file('download.R'))

unique_books_called <- FALSE
ggplot_check <- 0
ggplot_named_check <- 0
geom_point_check <- 0
geom_point_mapping_check <- FALSE
plot_arg <- ''

for (line in parsed) {
  arg_list <- call_args(line)
  
  if(is_call(line, '<-') && arg_list[[1]] == 'p') {
    right <- arg_list[[2]]
    right_call <- call_args(arg_list[[2]])
    
    if(is_call(right, 'ggplot', 0) && length(right_call) == 0) {
      ggplot_called_zero <- TRUE
    }
    
    if(length(right) >= 3) {
      if(right[[1]] == '+' && is_call(right[[2]], 'ggplot') && is_call(right[[3]], 'geom_point')){
        
        ggplot_args <- call_args(right[[2]])
        ggplot_check <- length(ggplot_args)
        
        ggplot_named_args <- call_standardise(right[[2]])
        ggplot_named_check <- length(ggplot_named_args)
        
        if(is_call(ggplot_named_args$mapping, 'aes')) {
          aes_args <- call_standardise(ggplot_named_args$mapping)
        }

        geom_point_args <- call_standardise(right[[3]])
        geom_point_check <- length(geom_point_args)
        
        if(!is.null(geom_point_args$mapping)) {
          geom_point_mapping_check <- TRUE
          geom_point_aes <- call_standardise(geom_point_args$mapping)
        }
      
        ggplot_called_zero <- TRUE
      }
    }
  }

  if (is_call(line, 'plot', 1)) {
    plot_arg <- arg_list
  }
  
}

walkAST <- function(expr, args) {
  if(is_closure(expr)) {
    lines <- parse_expr(deparse(body(expr)))
  } else {
    lines <- expr
  }

  for (cc in seq_along(lines)) {
    line <- lines[[cc]]
    if(is_call(line)) {
      this_operator <- line[[1]]
      this_left <- line[[2]]
      this_right <- line[[3]]
      
      if(this_operator == 'if' && args[[1]] == 'if' && this_left == args[[2]] && typeof(this_right) == "language") {
        if(is_list(args[[3]])) {
          return(walkAST(this_right, args[[3]]))
        } else {
          return(TRUE)
        }
      }

      str_this_operator <- stri_replace_all_fixed(deparse(line[[1]]), " ", "")
      str_this_left <- stri_replace_all_fixed(deparse(line[[2]]), " ", "")
      str_this_right <- stri_replace_all_fixed(deparse(line[[3]]), " ", "")
      if(str_this_left == args[[1]] && str_this_operator == args[[2]] && str_this_right == args[[3]]) {
        return(TRUE)
      }
      
      if(this_operator == 'for' && args[[1]] == 'for') {
        for (cc in seq_along(line)) {
          if(typeof(line[[cc]]) == "language") {
            return(walkAST(line[[cc]], args[[2]]))
          }
        }
      }
    }
  }
  return(FALSE)
}

context('Module 01')
test_that('Find Twain\'s Works @find-twains-works', {
  expect('twain' %in% ls(envir = user), 'Does the `twain` data frame exist in `download.R`?')
  expect(isTRUE(all_equal(user$twain, solution$twain)), 'The `twain` data frame does not contain the correct data or columns.')
})

test_that('Select Relevant Columns @select-relevant-columns', {
  expect('twain_refined' %in% ls(envir = user), 'Does the `twain_refined` data frame exist in `download.R`?')
  expect(isTRUE(all_equal(user$twain_refined, solution$twain_refined)), 'The `twain_refined` data frame does not contain the correct data or columns.')
})

test_that('Arrange Books by Download @arrange-by-download', {
  expect('twain_by_download' %in% ls(envir = user), 'Does the `twain_by_download` data frame exist in `download.R`?')
  expect(isTRUE(all_equal(user$twain_by_download, solution$twain_by_download, ignore_row_order = FALSE)), 'The `twain_by_download` data frame does not contain the correct data or columns. Or the data is not in the correct order.')
})

test_that('Create a Function @create-function', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  defined_args <- formals(user$unique_books)
  expect(is_symbol(defined_args$data), 'Is the first parameter in the `unique_books` function definition named `data`?')
  expect(is_character(defined_args$column), 'Is the second parameter in the `unique_books` function definition named `column`? Does it have a default value of `title`?')
  expect(defined_args$column == 'title', 'Is the second parameter `column` set to `\'title\'`?')
})

test_that('Pull the Correct Column @pull', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  alternative <- walkAST(user$unique_books, list('items', '<-', 'data%>%pull(column)')) || walkAST(user$unique_books, list('items', '<-', 'pull(data,column)'))
  expect(alternative, 'Have you `pull`ed the `column` and stored it in a varaible called `items`?')
})

test_that('Create an Empty List @empty-list', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect(walkAST(user$unique_books, list('duplicates', '<-', 'list()')) || walkAST(user$unique_books, list('duplicates', '<-', 'list(0)')), 'Have you created an empty list called `duplicates`?')
})

test_that(' Create a For Loop @for-loop', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect(walkAST(user$unique_books, list('item', 'for', 'items')), 'Have you created a `for` loop to loop through `items`?')
})

test_that('Fuzzy Matching @fuzzy-matching', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect(walkAST(user$unique_books, list('for', list('match', '<-', 'agrep(item,items)'))), 'Are you using the `agrep` function to fuzzy match `item` in `items`?')
  expect(walkAST(user$unique_books, list('for', list('last', '<-', 'match[-1]'))), 'Are you taking the last item from the `match` list?')
})

test_that('Add Elements to a List @add-list', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect(walkAST(user$unique_books, list('for', list('if', 'length(last)', ''))) || walkAST(user$unique_books, list('for', list('if', 'length(last)>0', ''))), 'Do you have an `if` statement that checks the `length` of `last`?')
  expect(walkAST(user$unique_books, list('for', list('if', 'length(last)', list('duplicates[[last]]', '<-', 'last')))), 'In the body of an `if` statement are you adding the `last` variable to the `duplicates` list?')
})

test_that('Remove Duplicates @remove-duplicates', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect(walkAST(user$unique_books, list('remove', '<-', 'unique(unlist(duplicates))')), 'Have you converted the `duplicates` list to a vector. Make sure all values are unique with the `unique()` function?')
  expect(walkAST(user$unique_books, list('data', '[', '-remove')), 'Have you removed the duplicates from the `data` data frame?')
})

test_that('Call a Function @call-function', {
  expect(is_function(user$unique_books), 'Have you defined a function called `unique_books`?')
  expect('twain_unique' %in% ls(envir = user), 'Does the `twain_unique` data frame exist in `download.R`?')
  expect(isTRUE(all_equal(user$twain_by_download, solution$twain_by_download)), 'The `twain_unique` data frame does not contain the correct data or columns. Have you called the `unique_books` function with the correct argument?')
})

test_that('Initialize a Plot Object @initialize-plot', {
  expect(exists('ggplot_called_zero'), 'Was the variable `p` set to the result of a call to `ggplot()`')
  expect(plot_arg == 'p', 'Was the `plot()` function called and passed an argument of `p`?')
})

test_that('Adding a Component @add-component', {
  expect(geom_point_check != 0, 'Was the `geom_point()` function added to `ggplot()`?')
})

test_that('Aesthetic Mappings @aesthetic-mappings', {
  expect(ggplot_check != 0, 'Have you added the proper arguments to the `ggplot()` function?')
  expect(ggplot_named_check != 0, 'Have you added the proper arguments to the `ggplot()` function?')
  expect(ggplot_named_args$data == 'twain_unique', 'Are you passing the `twain_unique` data frame to the `ggplot()` function?')
  expect(exists('aes_args'), 'Have you passed a call to the `aes()` function as the second argument to the `ggplot()` function?')
  expect(aes_args$x == 'sentences', 'Was the `sentences` column passed to the `aes()` function?')
  expect(aes_args$y == 'avg_words_per_sentence', 'Was the `avg_words_per_sentence` column passed to the `aes()` function?')
})

test_that('Geom Aesthetic Mappings @geom-aesthetic-mappings', {
  expect(geom_point_check != 0, 'Have you added the proper arguments to the `geom_point()` function?')
  expect(geom_point_mapping_check, 'Have you added the proper arguments to the `geom_point()` function?')
  expect(geom_point_mapping_check && length(geom_point_aes$size) != 0, 'Have you added the `size` mapping to the `aes()` function?')
  expect(geom_point_mapping_check && geom_point_aes$size == 'downloads', 'Have you added the `size` mapping to the `aes()` function?')
})
