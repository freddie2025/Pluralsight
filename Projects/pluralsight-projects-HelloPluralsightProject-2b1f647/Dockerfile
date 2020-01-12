FROM node:8.12-alpine

WORKDIR /src/app/

ADD ./package.json .

RUN ["npm", "install"]

COPY . .

RUN chown -R node:node /src/app

USER node
