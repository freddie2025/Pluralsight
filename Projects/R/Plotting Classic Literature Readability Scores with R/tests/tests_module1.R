library(rlang)
library(glue)

setwd('..')

solution <- new.env()
source('tests/solution.R', local = solution)

user <- new.env()
source('reading.R', local = user)

parsed <- parse_exprs(file('reading.R'))

source_arg <- ''
plot_arg <- ''
ggplot_check <- 0
ggplot_named_check <- 0
geom_bar_check <- 0
geom_bar_stat_check <- FALSE
geom_bar_stat <- ''

for (line in parsed) {
  
  arg_list <- call_args(line)
  if (is_call(line, 'source', 1)) {
    source_arg <- arg_list
  }
  
  if(is_call(line, '<-') && arg_list[[1]] == 'p') {
    
    right <- arg_list[[2]]
    right_call <- call_args(arg_list[[2]])

    if(is_call(right, 'ggplot', 0) && length(right_call) == 0) {
      ggplot_called_zero <- TRUE
    }
    
    if(length(right) >= 3) {
      if(right[[1]] == '+' && is_call(right[[2]], 'ggplot') && is_call(right[[3]], 'geom_bar')){
        
        ggplot_args <- call_args(right[[2]])
        ggplot_check <- length(ggplot_args)
        
        ggplot_named_args <- call_standardise(right[[2]])
        ggplot_named_check <- length(ggplot_named_args)
        
        if(is_call(ggplot_named_args$mapping, 'aes')) {
          aes_args <- call_standardise(ggplot_named_args$mapping)
        }
        
        geom_bar_args <- call_standardise(right[[3]])
        geom_bar_check <- length(geom_bar_args)
        
        if(!is.null(geom_bar_args$stat)) {
          geom_bar_stat_check <- TRUE
          geom_bar_stat <- geom_bar_args$stat
        }
        
        ggplot_called_zero <- TRUE
      }
      
      if(right[[1]] == '+' && right[[2]] == 'p' && is_call(right[[3]], 'facet_grid')){
        facet_grid_args <- call_standardise(right[[3]])
        if(is_call(facet_grid_args$rows, 'vars')) {
          vars_args <- call_args(facet_grid_args$rows)
        }
      }
      
      if(right[[1]] == '+' && right[[2]] == 'p' && is_call(right[[3]], 'theme')){
        theme_args <- call_standardise(right[[3]])

        if(is_call(theme_args$axis.text.x, 'element_text')) {
          element_text_args <- call_standardise(theme_args$axis.text.x)
        }
      }
      
    }

  }

  if (is_call(line, 'plot', 1)) {
    plot_arg <- arg_list
  }

}

context('Module 01')
test_that('Source Files @source-files', {
  expect(source_arg == 'data.R', 'Have you called the `source()` function, passing in `data.R`?')
})

test_that('Arrange Books by Download @arrange-by-download', {
  expect('books_by_download' %in% ls(envir = user), 'Does the `books_by_download` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$books_by_download, solution$books_by_download, ignore_row_order = FALSE)), 'The `books_by_download` data frame does not contain the correct data ordered by download.')
})

test_that('Select Relevant Columns @select-columns', {
  expect('books_refined' %in% ls(envir = user), 'Does the `books_refined` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$books_refined, solution$books_refined)), 'The `books_refined` data frame does not contain the correct data or columns.')
})

test_that('Top Ten Authors @top-ten-authors', {
  expect('top_ten_authors' %in% ls(envir = user), 'Does the `top_ten_authors` list exist in `reading.R`?')
  expect(isTRUE(all.equal(user$top_ten_authors, solution$top_ten_authors)), 'The `top_ten_authors` list does not contain the correct data ordered by download.')
})

test_that('Filtering by the Top Ten Author @filter-top-ten-authors', {
  expect('authors_books' %in% ls(envir = user), 'Does the `authors_books` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$authors_books, solution$authors_books)), 'The `authors_books` data frame does not contain the correct data.')
})

test_that('Adding a Flesch Reading Ease Column @flesch-reading-ease', {
  expect('reading_ease' %in% ls(envir = user), 'Does the `reading_ease` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$reading_ease, solution$reading_ease)), 'The `reading_ease` data frame does not contain the correct data. Has the `flesch_reading_ease` column been added?')
})

test_that('Adding a Flesch Reading Grade Level Column @flesch-kincaid-grade-level', {
  expect('reading_grade' %in% ls(envir = user), 'Does the `reading_grade` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$reading_grade, solution$reading_grade)), 'The `reading_grade` data frame does not contain the correct data. Has the `flesch_kincaid_grade_level` column been added?')
})

test_that('Group by Author @group-by-author', {
  expect('reading_grouped' %in% ls(envir = user), 'Does the `reading_grouped` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$reading_grouped, solution$reading_grouped)), 'The `reading_grouped` data frame does not contain the correct data. Has it been group by author?')
})

test_that('Summarising Readability Metrics @summarising-readability-metrics', {
  expect('reading_summary' %in% ls(envir = user), 'Does the `reading_summary` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$reading_summary, solution$reading_summary)), 'The `reading_summary` data frame does not contain the correct data. Have you calculated the mean for the appropriate columns?')
})

test_that('Reshaping a Data Frame @reshape', {
  expect('reading_long' %in% ls(envir = user), 'Does the `reading_long` data frame exist in `reading.R`?')
  expect(isTRUE(all_equal(user$reading_long, solution$reading_long)), 'The `reading_long` data frame does not contain the correct data. Have you reshaped it to have the correct columns?')
})

test_that('Initialize a Plot Object @initialize-plot', {
  expect(exists('ggplot_called_zero'), 'Was the variable `p` set to the result of a call to `ggplot()`')
  expect(plot_arg == 'p', 'Was the `plot()` function called and passed an argument of `p`?')
})

test_that('Adding a Component @geom-bar', {
  expect(exists('geom_bar_args'), 'Was the `geom_bar()` function added to `ggplot()`?')
  expect(geom_bar_stat_check, 'Was the correct named argument added to the `geom_bar()` function?')
  expect(geom_bar_stat == 'identity', 'Does the `stat` named argument have a value of `identity`?')
})

test_that('Aesthetic Mappings @aes', {
  expect(ggplot_check != 0, 'Have you added the proper arguments to the `ggplot()` function?')
  expect(ggplot_named_check != 0, 'Have you added the proper arguments to the `ggplot()` function?')
  expect(ggplot_named_args$data == 'reading_long', 'Are you passing the `reading_long` data frame to the `ggplot()` function?')
  expect(exists('aes_args'), 'Have you passed a call to the `aes()` function as the second argument to the `ggplot()` function?')
  expect(aes_args$x == 'author', 'Was the `author` column passed to the `aes()` function?')
  expect(aes_args$y == 'score', 'Was the `score` column passed to the `aes()` function?')
})

test_that('Faceting the Bar Plot @facet-grid', {
  expect(exists('facet_grid_args'), 'Have you add a call to the `facet_grid()` function to `p` and assigned it back to `p`?')
  expect(exists('vars_args'), 'Was a `rows` named argument set equal to `vars(type)` passed to the `facet_grid()` function?')
  expect(vars_args[[1]] == 'type', 'Was `type` passed to the `vars()` function?')
})

test_that('Customizing the Plot @customize-plot', {
  expect(exists('theme_args'), 'Have you add a call to the `theme()` function to `p` and assigned it back to `p`?')
  expect(exists('element_text_args'), 'Was an `axis.text.x` named argument set equal to `element_text()` passed to the `theme()` function?')
  expect(element_text_args$angle == 45, 'Was `angle` set to `45` passed to the `element_text()` function?')
  expect(element_text_args$hjust == 1, 'Was `hjust` set to `1` passed to the `element_text()` function?')
})