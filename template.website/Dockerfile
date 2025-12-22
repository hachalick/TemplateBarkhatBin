FROM node:lts-alpine3.23 AS build
WORKDIR /app

COPY package*.json ./
CMD ["npm", "install"]

COPY . .
EXPOSE 8082
CMD ["npm", "run", "build"]
CMD ["npm", "run", "start"]