# OrderFlow API

API REST para gerenciamento de produtos, unidades, estoque e pedidos, desenvolvida como projeto acadêmico do curso de Análise e Desenvolvimento de Sistemas.

## Tecnologias utilizadas

- C#
- ASP.NET Core (.NET 10)
- Entity Framework Core
- PostgreSQL
- JWT para autenticação
- Swagger / OpenAPI
- Docker

## Arquitetura

O projeto utiliza arquitetura em camadas:

- **Domain:** entidades e regras de domínio.
- **Application:** casos de uso, DTOs e interfaces.
- **Infrastructure:** persistência de dados e implementações.
- **API:** endpoints, autenticação e configuração da aplicação.

## Funcionalidades

- Cadastro e autenticação de usuários.
- Autenticação por JWT.
- Controle de acesso por perfil.
- Gerenciamento de produtos e unidades.
- Consulta e controle de estoque.
- Criação de pedidos.
- Reserva e baixa de estoque.
- Simulação de aprovação e recusa de pagamentos.
- Registro de auditoria na criação de pedidos.

## Configuração e execução

### Pré-requisitos

- .NET 10 SDK
- Docker
- PostgreSQL (ou Docker para executar o banco)

### Banco de dados

O projeto utiliza PostgreSQL 17 executado em um container Docker.

Para criar e iniciar o banco de dados, execute:

```bash
docker run --name orderflow-postgres \
  -e POSTGRES_DB=orderflow_db \
  -e POSTGRES_USER=orderflow \
  -e POSTGRES_PASSWORD=sua_senha \
  -p 5432:5432 \
  -d postgres:17
```

Substitua `sua_senha` por uma senha de sua escolha.

Configure a connection string da aplicação com os mesmos dados utilizados na criação do container, por meio do User Secrets ou de variáveis de ambiente.

Configure também a chave JWT por meio dessas configurações locais.

Com o banco em execução, aplique as migrations do Entity Framework Core, caso existam pendentes:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project src/OrderFlow.Infrastructure --startup-project src/OrderFlow.API
```
### Configuração das credenciais

As configurações são armazenadas localmente utilizando o .NET User Secrets, evitando a inclusão de credenciais no código-fonte.


Configure a conexão com o banco:

```bash
dotnet user-secrets set "ConnectionStrings:OrderFlow" "Host=localhost;Port=5432;Database=orderflow_db;Username=orderflow;Password=SUA_SENHA" --project src/OrderFlow.API
```

Configure a chave JWT e seus identificadores:

```bash
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_JWT" --project src/OrderFlow.API
dotnet user-secrets set "Jwt:Issuer" "OrderFlow" --project src/OrderFlow.API
dotnet user-secrets set "Jwt:Audience" "OrderFlowClient" --project src/OrderFlow.API
```

Substitua os valores de exemplo pelas credenciais definidas no ambiente local.

### Executar a API

Na raiz do projeto, execute:

```bash
dotnet restore
dotnet build
dotnet run --project src/OrderFlow.API
```

Após iniciar a aplicação, acesse a URL local exibida no terminal e acrescente `/swagger` para visualizar a documentação interativa da API.

## Testes

Os testes funcionais foram realizados utilizando Postman, contemplando autenticação, produtos, pedidos, permissões e tratamento de erros.

A coleção está disponível no arquivo:

`OrderFlow_API.postman_collection.json`

## Estrutura do projeto

```text
OrderFlow/
├── src/
│   ├── OrderFlow.API/
│   ├── OrderFlow.Application/
│   ├── OrderFlow.Domain/
│   └── OrderFlow.Infrastructure/
├── OrderFlow.slnx
├── OrderFlow_API.postman_collection.json
└── README.md
```
## Links do projeto

- **Repositório GitHub:** https://github.com/vinicius2104/OrderFlow
- **Coleção Postman:** disponível no arquivo `OrderFlow_API.postman_collection.json` na raiz do repositório.
- **Swagger / OpenAPI:** disponível localmente em `http://localhost:xxxx/swagger` após iniciar a API.

## Projeto acadêmico

Desenvolvido como parte do projeto do curso de Análise e Desenvolvimento de Sistemas – UNINTER.