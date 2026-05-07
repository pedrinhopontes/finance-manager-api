# Finance Manager API

API REST para gerenciamento de finanças pessoais, desenvolvida com .NET 10 seguindo Clean Architecture.

## Sobre o Projeto

Sistema que permite ao usuário cadastrar receitas e despesas, visualizar saldo e acompanhar suas finanças pessoais. Desenvolvido como projeto de portfólio para consolidar conhecimentos em arquitetura de software, autenticação JWT, boas práticas e tecnologias do ecossistema .NET.

## Arquitetura

O projeto segue os princípios de Clean Architecture dividido em 4 camadas:

- **Domain** — entidades, enums e interfaces (regras de negócio)
- **Application** — casos de uso com CQRS e MediatR
- **Infrastructure** — acesso a dados com EF Core e serviços externos
- **API** — controllers, configuração e Swagger

## Tecnologias

- .NET 10
- ASP.NET Core
- Entity Framework Core
- MediatR (CQRS)
- JWT Bearer Authentication
- BCrypt.Net (hash de senhas)
- SQL Server / LocalDB
- Swagger / OpenAPI

## Como rodar localmente

**Pré-requisitos:** .NET 10 SDK e SQL Server ou LocalDB instalados.

Clone o repositório:

    git clone https://github.com/pedrinhopontes/finance-manager-api.git
    cd finance-manager-api

Aplique as migrations:

    dotnet ef database update --project FinanceManager.Infrastructure --startup-project FinanceManager.API

Rode a aplicação:

    dotnet run --project FinanceManager.API

Acesse o Swagger em https://localhost:7052/swagger

## Endpoints

| Método | Rota | Autenticação | Descrição |
|--------|------|-------------|-----------|
| POST | /api/Auth/register | ❌ | Cadastro de usuário |
| POST | /api/Auth/login | ❌ | Login e geração de token JWT |
| GET | /api/Transactions | ✅ | Lista transações do usuário |
| POST | /api/Transactions | ✅ | Cria nova transação |
| DELETE | /api/Transactions/{id} | ✅ | Remove uma transação |

## Frontend

Este projeto possui um frontend em Angular disponível em [finance-manager-web](https://github.com/pedrinhopontes/finance-manager-web).
