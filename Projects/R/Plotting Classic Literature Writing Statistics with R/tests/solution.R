library(readr)
library(dplyr)
library(stringr)
library(tidyr)
library(ggplot2)
library(testthat)

titles <- read_csv('data/titles.csv')
stats <- read_csv('data/stats.csv')

books <- full_join(titles, stats)

dickens <- filter(books, str_detect(author, 'Dickens'))

dickens_stats <- select(dickens, id, words, sentences, to_be_verbs, contractions, pauses, cliches, similes)

published <- read_csv('data/published.csv')

time <- full_join(dickens_stats, published)

time_long <- gather(time, type, value, words:similes)

p <- ggplot(time_long , aes(year, value, color = type)) + geom_line()