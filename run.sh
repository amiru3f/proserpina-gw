#!/bin/bash
cd Proserpina.Auth.Api && dotnet build && ls
cd ./bin/Debug/net5.0 && docker build -t proserpina-api-auth:latest . && cd ../../../
cd ../Proserpina.HelloService.Api && dotnet build && ls
cd ./bin/Debug/net5.0 && docker build -t proserpina-api-hello .
cd .. && docker-compose up -d

curl  localhost:8080/token?username=admin&password=admin
curl -i -H "token: 65af3ae3-d4d4-4e64-809e-c33de711d79b" localhost:8080/hello
echo 