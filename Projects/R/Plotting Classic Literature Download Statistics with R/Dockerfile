FROM r-base:3.6.0

WORKDIR /src/app/

RUN apt-get update && apt-get install -qq -y --no-install-recommends \
  libxml2-dev libssl-dev libcurl4-openssl-dev libssh2-1-dev \
  r-cran-ggplot2 r-cran-dplyr r-cran-tidyr r-cran-readr \
  r-cran-stringr r-cran-testthat r-cran-xml2 r-cran-optparse r-cran-checkmate

COPY . .

RUN groupadd projects && useradd --no-create-home -g projects projects && \
  chown -R projects:projects /src/app

USER projects
