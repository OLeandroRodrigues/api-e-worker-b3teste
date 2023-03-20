#!/bin/bash
docker pull mysql:5.7
docker pull rabbitmq:3-management
mkdir -p volume
if [ ! -d "$PWD/volume/mysql" ]; then
   mkdir -p volume/mysql/data
   chmod 777 -R $PWD/volume/mysql
fi

if [ ! -d "$PWD/volume/rabbitmq" ]; then
   mkdir -p volume/rabbitmq/data
   chmod 777 -R $PWD/volume/rabbitmq
fi

if [ -z $1 ]; then
  chmod 777 -R $PWD/volume/
  docker-compose -f docker-compose.yaml up -d
elif [[ $1 = "up" ]]; then
  chmod 777 -R $PWD/volume/
elif [[ $1 = "down" ]]; then
  docker-compose -f docker-compose.yaml down
fi