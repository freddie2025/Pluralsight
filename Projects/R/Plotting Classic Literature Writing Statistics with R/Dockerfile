FROM r-base

RUN apt-get update && apt-get install -qq -y --no-install-recommends \
  libxml2-dev libssl-dev libcurl4-openssl-dev libssh2-1-dev \
  r-cran-ggplot2 r-cran-dplyr r-cran-tidyr r-cran-readr r-cran-stringr r-cran-testthat r-cran-xml2 r-cran-optparse r-cran-checkmate

ENV APP_DIR /src/app/

RUN mkdir -p $APP_DIR

WORKDIR ${APP_DIR}

COPY . .