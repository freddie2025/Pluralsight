library(glue)
library(tibble)

ProjectReporter <- R6::R6Class("ProjectReporter",
  inherit = Reporter,
  public = list(
    test_number = 0,
    test_context = '',
    test_results = tibble(test = character(), tag = character(), passed = numeric(), result = character(), test_number = numeric()),
  
    add_result = function(context, test, result) {
      if (!is.null(test)) { 
        self$test_context <- context
        self$test_number <- self$test_number + 1
        tag <- regmatches(test, gregexpr('@(.*)', test))[[1]]
  
        passed <- as.integer(as.logical(testthat:::expectation_ok(result)))
  
        self$test_results <- add_row(self$test_results, test = test, tag = tag, passed = passed, result = format(result), test_number = self$test_number)
      } else {
        remove_backtrace <- gsub('\\d+:(.*)', '', format(result))
        quoted <- trim(gsub('"', '`', format(remove_backtrace)))
        self$test_results <- add_row(test = '', self$test_results, tag = '@error', passed = -1, result = format(result), test_number = 0)
      }
    },
  
    end_reporter = function() {
  
      test_results <- self$test_results %>%
      group_by(tag) %>%
      slice(which.min(passed)) %>%
      arrange(test_number)

      tests <- nrow(test_results)
      failures <- sum(test_results$passed == 0)
      pass <- sum(test_results$passed == 1)
      errors <- sum(test_results$passed == -1)
  
      if(errors <= 0) {
        self$cat_line(glue('{self$test_context}\n'))
  
        for (row in 1:nrow(test_results)) {
          test_name <- gsub('@(.*)', '', test_results[row, "test"])
          tag <- test_results[row, "tag"]
          passed <- test_results[row, "passed"]
          result  <- test_results[row, "result"]
        
          self$cat_line(row, '. ', test_name)
          if (as.logical(passed)) {
            self$cat_line(crayon::green('<passed>'))
          } else {
            self$cat_line(crayon::red('<failed> '), result[[1]])
          }
        }
      } else {
        for (row in 1:nrow(test_results)) {
          self$cat_line(test_results[row, "result"])
        }
        tests <- 0
      }
      
      self$cat_line(glue('\n\nTests: {tests}\nFailures: {failures}\nPassed: {pass}\nErrors: {errors}\n'))
    }
  ),
  
  private = list(
    proctime = function() {
      proc.time()
    },
    timestamp = function() {
      toString(Sys.time())
    }
  )
)


