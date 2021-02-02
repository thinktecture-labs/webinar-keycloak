# Build Env
FROM node:alpine AS build-env
WORKDIR /app

COPY package.json ./
COPY package-lock.json ./

RUN npm i

COPY src ./src
COPY config ./config
COPY angular.json ./
COPY tsconfig.json ./
COPY tsconfig.app.json ./
COPY tslint.json ./

RUN npm run build-prod

# Prod Env
FROM nginx:alpine
WORKDIR /app

COPY --from=build-env /app/dist/blog-frontend /usr/share/nginx/html
COPY --from=build-env /app/config/nginx.conf /etc/nginx/conf.d/default.conf
