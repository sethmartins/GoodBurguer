# 🍔 Good Hamburger

API para gerenciamento de pedidos de uma lanchonete, desenvolvida em C# com .NET e ASP.NET Core.

---

## 📌 Sobre o projeto

Este projeto implementa um sistema de pedidos com regras de negócio específicas para uma lanchonete, incluindo:

- Cadastro e consulta de pedidos
- Cálculo automático de descontos
- Validações de domínio
- Integração com frontend em Blazor
- Testes automatizados

---

## 🧠 Regras de negócio

- Cada pedido pode conter:
  - 1 Sanduíche
  - 1 Batata
  - 1 Refrigerante

- Descontos aplicados:
  - 🟢 Sanduíche + Batata + Refrigerante → **20%**
  - 🟡 Sanduíche + Refrigerante → **15%**
  - 🟠 Sanduíche + Batata → **10%**
  - 🔴 Outros casos → **sem desconto**

- Restrições:
  - Não é permitido adicionar itens duplicados do mesmo tipo
  - Pedido deve conter pelo menos um sanduíche

---

## 🏗️ Arquitetura

O projeto segue princípios de:

- **DDD (Domain Driven Design)**
- **Clean Architecture**
- **CQRS sem MediatR(Devido a baixa complexidade do projeto)**

### Estrutura:
src/

├── GoodBurger.API → Controllers, Middleware

├── GoodBurger.Application → Commands, Queries, Handlers

├── GoodBurger.Domain → Entidades e regras de negócio

├── GoodBurger.Infrastructure → EF Core, Repositórios

└── GoodBurger.Web → Frontend em Blazor

---

## ⚙️ Tecnologias utilizadas

- .NET 10
- ASP.NET Core
- Swagger / OpenAPI / Scalar
- Entity Framework Core
- FluentValidation
- xUnit / Reqnroll (BDD)
- Blazor Server
- Bootstrap

---

## ▶️ Como executar o projeto

### Pré-requisitos

- .NET 10 SDK instalado
- Visual Studio ou VS Code

---

### 🔧 Backend (API)

```bash
cd src/GoodBurger.API
dotnet run
```
A API estará disponível em:
```
https://localhost:7273
```
```
cd src/GoodBurger.Web
dotnet run
```
Acesse:
```
https://localhost:7033
```
📡 Endpoints principais
### 📋 Pedidos
GET /api/pedido → lista todos os pedidos
GET /api/pedido/{id} → busca por ID
POST /api/pedido → cria pedido
PUT /api/pedido/{id} → atualiza pedido
DELETE /api/pedido/{id} → remove pedido

### 🍟 Cardápio
GET /api/cardapio → lista itens disponíveis

### 🧪 Testes
Rodar testes:
```
dotnet test
```
### Inclui:

###### Testes unitários de domínio
###### Testes de integração da API
###### Testes BDD com Reqnroll

### 💡 Decisões técnicas

#### ✔ Uso de Owned Entity (ItemPedido)

###### O ItemPedido foi modelado como Owned Entity dentro de Pedido, pois:

###### Representa um snapshot do item no momento do pedido
###### Não possui identidade fora do agregado
###### Evita inconsistência com alterações futuras no catálogo

####  ✔ Separação Item vs ItemPedido
###### vItem → catálogo
###### ItemPedido → dados congelados no pedido

#### ✔ Regras no domínio

##### Toda lógica de negócio está dentro da entidade Pedido, garantindo:
###### Alta coesão
###### Facilidade de teste
###### Independência de infraestrutura

### ⚠️ Melhorias futuras

#### Autenticação e autorização
#### Paginação e filtros na listagem
#### Cache de consultas
#### Deploy em nuvem (Azure / AWS)
#### UI mais rica (ex: MudBlazor)

#### 👨‍💻 Autor

#### Desenvolvido por Seth Martins
