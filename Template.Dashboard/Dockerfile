FROM node:lts-alpine3.23 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
EXPOSE 8081
CMD ["npm", "run", "start"]

