library(readr)
library(dplyr)
library(stringr)
library(ggplot2)
library(tidyr)

titles <- read_csv('data/titles.csv')
stats <- read_csv('data/stats.csv')

books <- full_join(titles, stats) %>%
  filter(!str_detect(title, 'Project Gutenberg'))