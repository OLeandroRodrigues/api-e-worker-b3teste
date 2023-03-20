# Projeto Cadastro Tarefa

Este projeto foi criado com o .NET versão 5.

Você pode acessar ele pelo [Github Pages](https://github.com/OLeandroRodrigues/api-e-worker-b3teste/).

## Escopo

Criar um projeto .NET 5, utilizando a abordagem DDD com as seguintes funcionalidades:

- Construir a estrutura do projeto utilizando DDD;
- Criar microserviços de CRUD de tarefas ;
- Implementar endpoint para enviar tarefa para a Fila no RabbitMq ;
- Criar worker para obter itens da fila do RabbitMq e persistir na base de dados; 

## Tecnologias

- .NET 5
- EntityFrameworkCore 5.0.17
- Swagger 3.0.1
- Mysql
- RabbitMq

## Como instalar

- Baixe ou clone este repositório usando `git clone https://github.com/OLeandroRodrigues/api-e-worker-b3teste.git/`;

## Como executar

Execute o docker compose.
Abrir o gitbash dentro do diretório ..B3.Test\Devops  e executar o comando sh run-compose.sh

API
A API é chamada B3.Test.API e deve ser executada manualmente através do comando dotnet run .B3.Test\B3.Test.API
A API poderá ser acessada via `http://localhost:58801`.

Worker 
O worker é chamada B3.Test.Worker e deve ser executada manualmente através do comando dotnet run .B3.Test\B3.Test.Worker
