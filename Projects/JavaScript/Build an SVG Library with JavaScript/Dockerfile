FROM node:12.7-alpine

ENV APP_DIR /src/app/

RUN mkdir -p $APP_DIR

WORKDIR ${APP_DIR}

ADD ./package.json .

RUN ["npm", "install"]

COPY . .

RUN chown -R node:node $APP_DIR

USER node