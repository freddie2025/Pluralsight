library(glue)
library(tibble)

JsonReporter <- R6::R6Class("JsonReporter",
  inherit = Reporter,
  public = list(
    timer = NULL,
    test_number = 0,
    test_context = '',
    test_results = tibble(tag = character(), time = numeric(),  passed = numeric(), result = character(), test_number = numeric()),

    elapsed_time = function() {
      time <- round((private$proctime() - self$timer)[["elapsed"]], 2)
      self$timer <- private$proctime()
      time
    },

    start_reporter = function() {
      self$timer <- private$proctime()
    },

    add_result = function(context, test, result) {
      if (!is.null(test)) { 
        self$test_context <- context
        self$test_number <- self$test_number + 1
        time <- self$elapsed_time()
        tag <- regmatches(test, gregexpr('@(.*)', test))[[1]]

        passed <- as.integer(as.logical(testthat:::expectation_ok(result)))

        self$test_results <- add_row(self$test_results, tag = tag, time = time, passed = passed, result = format(result), test_number = self$test_number)
      } else {

        remove_backtrace <- gsub('\\d+:(.*)', '', format(result))
        quoted <- trim(gsub('"', '`', format(remove_backtrace)))
        self$test_results <- add_row(self$test_results, tag = '@error', time = 0, passed = -1, result = quoted, test_number = 0)
      }
    },

    end_reporter = function() {

      test_results <- self$test_results %>%
        group_by(tag) %>%
        slice(which.min(passed)) %>%
        arrange(test_number)

      full_pass <- all(as.logical(test_results$passed))
      suiteTime <- sum(test_results$time)
      tests <- nrow(test_results)
      failures <- sum(test_results$passed == 0)
      errors <- sum(test_results$passed == -1)
      timestamp <- private$timestamp()

      if(errors <= 0) {
        self$cat_line(glue('{{ "testsuites": [{{\n  "suite": "{self$test_context}",\n  "timestamp": "{timestamp}",\n  "suiteTime": "{suiteTime}",\n  "passed": {tolower(as.character(full_pass))},\n  "tests": {tests},\n  "failures": {failures},\n  "errors": {errors},\n'))

        self$cat_line('  "testResults": [')

        for (row in 1:nrow(test_results)) {
          tag <- test_results[row, "tag"]
          time  <- test_results[row, "time"]
          passed <- test_results[row, "passed"]
          result  <- test_results[row, "result"]
          comma <- ','

          if (row == nrow(test_results)) {
            comma <- ''
          }

          if(as.logical(passed)){
            self$cat_line(glue('    {{ "tag": "{tag}", "time": {time}, "passed": {tolower(as.logical(passed))}, "error": {{}} }}{comma}'))
          } else {
            self$cat_line(glue('    {{ "tag": "{tag}", "time": {time}, "passed": {tolower(as.logical(passed))}, "error": {{ "message": "{result}"}} }}{comma}'))
          }
        }

        self$cat_line('  ]\n}]}')
      } else {
        self$cat_line('Execution halted')
      }
      
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