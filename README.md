# Desafio Técnico da Wake Tech
Desafio técnico da Wake Tech para demonstrar conhecimentos no processo de análise e desenvolvimento de uma aplicação, neste caso, um CRUD de produtos.

## Projeto

A "VirtualStore" é uma API fictícia que simula um backend de produtos em uma loja, contendo operações básicas relacionadas a um produto, tais como criação, captura, atualização e deleção.

Trata-se de um projeto ASP.NET core no qual foi utilizado a versão 7 do .NET conjuntamente com EntityFramework como facilitador para operações no banco de dados, utilizando a abordagem "Code-First" para criação do modelo de banco de dados. O banco de dados escolhido foi o Postgres.

O projeto foi desenvolvido utilizando boas práticas de desenvolvimento, bem como padrões de projeto que são essenciais para uma boa estruturação e legibilidade da aplicação, como SOLID e Design Patterns.

## Estrutura da aplicação

A aplicação foi desenvolvida em 6 camadas:

- **VirtualStore.API**: responsável por receber solicitações HTTP e fornecer respostas apropriadas.

- **VirtualStore.Application**: responsável pela lógica de negócios da aplicação, definindo as interfaces dos serviços da aplicação bem como sua implementação.

- **VirtualStore.Domain**: responsável por armazenar a entidade da aplicação e os objetos de transferência de dados (DTO) para representar e transferir informações  relevantes.

- **VirtualStore.Infrastructure**: responsável por gerenciar a infraestrutura técnica da aplicação, incluindo o contexto do banco de dados, contratos para serviços , scripts e migrations do banco de dados.

- **VirtualStore.IntegrationTests**: responsável pela implementação dos testes de integração.

- **VirtualStore.UnitTests**: responsável pela implementação dos testes unitários.


## Requisitos para executar a aplicação

- Certifique-se de ter a SDK7 instalado na sua máquina e também o Docker. Caso queira fazer o download, acesse o link https://dotnet.microsoft.com/pt-br/download/dotnet/7.0 .

- Para clonar este repositório, execute o seguinte comando em seu terminal:

```
git clone https://github.com/eazevedo016/VirtualStore.git
```
- Abra o projeto em uma IDE de sua preferência, abra o terminal e navegue até o projeto "VirtualStore.API"

- Execute o seguinte comando:

```
docker compose build
```

- Agora é só acessar o Swagger disponível através da URL: http://localhost:8080/swagger/index.html

## Visão do CRUD

A documentação da API foi consolidada utilizando o Swagger. Através dele é possível testar quaisquer métodos que a API expõe. 


![image](https://github.com/eazevedo016/VirtualStore/assets/75282286/551eff95-f7c1-49d0-82dc-dec6ef581e2f)



