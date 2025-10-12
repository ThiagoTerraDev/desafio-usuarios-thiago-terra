# API de Gerenciamento de Usuários

Uma Web API RESTful construída com ASP.NET Core 9.0 para gerenciar dados de usuários. Esta aplicação fornece endpoints para operações CRUD em registros de usuários com suporte para diferentes departamentos e turnos de trabalho.

## 🚀 Tech Stack

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **Azure SQL Edge** (SQL Server engine / Entity Framework compatibility)
- **Swagger/OpenAPI**

## 📋 Funcionalidades

- Operações CRUD completas para usuários (Criar, Ler, Atualizar, Deletar)
- Desativação de usuários
- Categorização de usuários por departamento
- Gerenciamento de turnos de trabalho (Manhã, Tarde, Noite)
- Entity Framework Core com Azure SQL Edge
- Documentação interativa com Swagger/OpenAPI
- Configuração para ambiente de desenvolvimento

## 📊 Modelo de Dados

O modelo `UserModel` contém os seguintes campos:

- **Id**: Identificador único do usuário
- **Name**: Nome do usuário
- **LastName**: Sobrenome do usuário
- **Department**: Departamento (RH, Financeiro, Compras, Atendimento, Zeladoria)
- **Shift**: Turno de trabalho (Manhã, Tarde, Noite)
- **Active**: Status de ativação do usuário
- **CreatedAt**: Data e hora de criação
- **UpdatedAt**: Data e hora da última atualização

## 🔌 Endpoints da API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/users` | Lista todos os usuários |
| `GET` | `/api/users/{id}` | Busca um usuário por ID |
| `POST` | `/api/users` | Cria um novo usuário |
| `PUT` | `/api/users` | Atualiza um usuário existente |
| `PATCH` | `/api/users/{id}/deactivate` | Desativa um usuário |
| `DELETE` | `/api/users/{id}` | Deleta um usuário |

## 🚀 Como Executar o Projeto

### Pré-requisitos

- .NET 9.0 SDK
- Docker (para o Azure SQL Edge)
- Git

### Instalação

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd desafio-usuarios-thiago-terra
   ```

2. **Inicie o Azure SQL Edge com Docker**
   
   > **Importante**: Certifique-se de que o Docker esteja rodando antes de executar o comando abaixo:
   
   ```bash
   docker run -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=SuaSenhaForte123! -p 1433:1433 --name sqlserver -d mcr.microsoft.com/azure-sql-edge
   ```
   > **Nota**: Substitua `SuaSenhaForte123!` por uma senha forte de sua escolha.

3. **Verifique se o container está rodando**
   ```bash
   docker ps
   ```
   Você deve ver o container `sqlserver` na lista.

4. **Navegue até o diretório do projeto backend**
   ```bash
   cd api-rest-dotnet
   ```

5. **Configure o ambiente de desenvolvimento**
   
   Crie um arquivo chamado `appsettings.Development.json` no diretório `api-rest-dotnet` com o seguinte conteúdo:
   
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=localhost,1433;Initial Catalog=User;User Id=sa;Password=SuaSenhaForte123!;Encrypt=false;TrustServerCertificate=true"
     },
     "AllowedHosts": "*"
   }
   ```
   > **Importante**: Substitua `SuaSenhaForte123!` pela mesma senha que você definiu no passo 2.

6. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

7. **Execute as migrações do banco de dados**
   ```bash
   dotnet ef database update
   ```

8. **Inicie a aplicação**
   ```bash
   dotnet run
   ```

9. **Acesse a API**
   - Swagger UI: `https://localhost:7183/swagger` ou `http://localhost:5127/swagger`

## 🔧 Configuração

### Conexão com o Banco de Dados

A aplicação usa o Azure SQL Edge com a seguinte configuração padrão para desenvolvimento:

- **Host**: localhost
- **Porta**: 1433
- **Banco de dados**: User
- **Autenticação**: SQL Server Authentication
- **Usuário**: sa
- **Senha**: Definida por você durante a configuração do Docker

### Configuração de Ambiente

- **Produção**: Usa `appsettings.json` (connection string placeholder por segurança)
- **Desenvolvimento/Localhost**: Usa `appsettings.Development.json` (inclui connection string real)

### Confiar no Certificado de Desenvolvimento (Opcional)

Se você encontrar avisos de certificado SSL, pode confiar no certificado de desenvolvimento:

```bash
dotnet dev-certs https --trust
```

## 📚 Documentação da API

Após iniciar a aplicação, você pode acessar a documentação interativa da API através do Swagger UI em:
- `https://localhost:7183/swagger` (HTTPS)
- `http://localhost:5127/swagger` (HTTP)

## 🛠️ Desenvolvimento

### Adicionar Novas Migrações

```bash
dotnet ef migrations add NomeDaMigracao
```

### Atualizar o Banco de Dados

```bash
dotnet ef database update
```

### Compilar a Aplicação

```bash
dotnet build
```

### Executar em Modo de Desenvolvimento

```bash
dotnet run
```

## 👨‍💻 Autor

**Thiago Terra**
- LinkedIn: [Thiago Terra](https://www.linkedin.com/in/thiago-terra-158a71266/)
- GitHub: [@ThiagoTerraDev](https://github.com/ThiagoTerraDev)
