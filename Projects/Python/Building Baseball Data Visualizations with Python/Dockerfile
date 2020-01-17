FROM python:3.7.2-slim-stretch

WORKDIR /src/app/

COPY ./requirements.txt .

RUN ["pip", "install", "-r", "./requirements.txt"]

COPY . .

RUN groupadd projects && useradd --no-create-home -g projects projects && \
  chown -R projects:projects /src/app

USER projects
