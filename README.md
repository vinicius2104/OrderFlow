# OrderFlow API

API REST desenvolvida em C# com ASP.NET Core e PostgreSQL para gerenciamento de pedidos, produtos, estoque e pagamentos da rede fictícia Raízes do Nordeste.

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core
- PostgreSQL 17
- Docker e Docker Compose
- JWT (autenticação e autorização)
- Swagger
- Postman

## Estrutura do projeto

```text
OrderFlow/
├── src/
│   ├── OrderFlow.API/
│   ├── OrderFlow.Application/
│   ├── OrderFlow.Domain/
│   └── OrderFlow.Infrastructure/
├── docs/
│   └── OrderFlow_API.postman_collection.json
├── OrderFlow.slnx
└── README.md
```

## Funcionalidades

- Cadastro e autenticação de usuários.
- Autenticação com JWT.
- Controle de acesso por perfil de usuário.
- Cadastro e gerenciamento de unidades.
- Cadastro e gerenciamento de produtos.
- Controle de estoque por unidade.
- Registro e consulta de pedidos.
- Identificação do canal de origem dos pedidos.
- Reserva e baixa de estoque durante o processamento dos pedidos.
- Simulação de aprovação e recusa de pagamentos.
- Registro de auditoria das operações.

## Requisitos

Para executar o projeto, é necessário ter instalado:

- .NET 10 SDK
- Docker
- Docker Compose

## Configuração do ambiente

### 1. Clonar o repositório

```bash
git clone https://github.com/vinicius2104/OrderFlow.git
cd OrderFlow
```

### 2. Iniciar o banco de dados

Na raiz do projeto, execute:

```bash
docker compose up -d
```

O Docker Compose iniciará o PostgreSQL 17 com as seguintes configurações:

- Banco de dados: `orderflow_db`
- Usuário: `orderflow`
- Senha: `orderflow_dev_2026`
- Porta: `5432`

### 3. Configurar as credenciais da aplicação

Configure a connection string utilizando User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:OrderFlow" "Host=localhost;Port=5432;Database=orderflow_db;Username=orderflow;Password=orderflow_dev_2026" --project src/OrderFlow.API
```

Gere uma chave aleatória para o JWT:

```bash
openssl rand -base64 32
```

Copie o valor gerado e configure:

```bash
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_GERADA" --project src/OrderFlow.API
dotnet user-secrets set "Jwt:Issuer" "OrderFlow" --project src/OrderFlow.API
dotnet user-secrets set "Jwt:Audience" "OrderFlowClient" --project src/OrderFlow.API
```

Substitua `SUA_CHAVE_GERADA` pelo valor obtido no comando anterior.

### 4. Aplicar as migrations

Caso ainda não tenha a ferramenta Entity Framework instalada:

```bash
dotnet tool install --global dotnet-ef
```

Execute as migrations para criar as tabelas e inserir os dados iniciais:

```bash
dotnet ef database update --project src/OrderFlow.Infrastructure --startup-project src/OrderFlow.API
```

### 5. Executar a API

Na raiz do projeto, execute:

```bash
dotnet restore
dotnet build
dotnet run --project src/OrderFlow.API
```

## Usuário gestor padrão

O banco de dados já possui um usuário gestor cadastrado automaticamente por meio de uma migration.

**Credenciais de acesso:**

| Campo | Valor |
|---|---|
| E-mail | admin@orderflow.com |
| Senha | Admin@123 |
| Perfil | GESTOR |

Esse usuário permite acessar os recursos administrativos da API.


## Documentação da API

Após iniciar a aplicação, a documentação interativa do Swagger estará disponível em:

```text
http://localhost:5000/swagger
```

A porta pode variar conforme a configuração de execução apresentada no terminal.

## Testes com Postman

O projeto disponibiliza uma coleção do Postman com requisições para testar os principais endpoints da API.

Arquivo:

```text
docs/OrderFlow_API.postman_collection.json
```

Importe a coleção no Postman para executar as requisições.

## Banco de dados

O PostgreSQL é utilizado para armazenar os dados da aplicação. A estrutura do banco é gerenciada pelo Entity Framework Core por meio de migrations.

O acesso ao banco pode ser realizado utilizando ferramentas como DBeaver.

## Arquitetura

O projeto utiliza arquitetura em camadas, dividida em:

- **API:** responsável pelos endpoints e pela comunicação HTTP.
- **Application:** contém os serviços e as regras de aplicação.
- **Domain:** concentra as entidades e regras de negócio.
- **Infrastructure:** responsável pelo acesso ao banco de dados e pelas implementações de infraestrutura.