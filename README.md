# Sistema de Gerenciamento de Usuários

Uma aplicação Full Stack para gerenciar dados de usuários, composta por uma Web API RESTful construída com ASP.NET Core 9.0 e um frontend em Angular. A aplicação fornece operações CRUD completas com suporte para diferentes departamentos e turnos de trabalho.

## 🚀 Tech Stack

### Backend
- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **Azure SQL Edge** (SQL Server engine / Entity Framework compatibility)
- **Swagger/OpenAPI**

### Frontend
- **Angular**
- **TypeScript**
- **Node.js 20.19 LTS**

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

- **.NET 9.0 SDK**
- **Docker** - Para o Azure SQL Edge
- **Node.js 20.19 LTS** (recomendado usar NVM para gerenciar versões)
- **NVM (Node Version Manager)**
- **Angular CLI** - Será instalado após configurar o Node.js
- **Git**

### Configuração do Backend

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
     "AppSettings": {
       "Token": "GENERATE_YOUR_OWN_SECRET_KEY_AT_LEAST_64_CHARACTERS_LONG_FOR_HMAC_SHA512"
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
   - Swagger UI: `http://localhost:5127/swagger` (padrão) ou `https://localhost:7183/swagger` (requer configuração HTTPS)

### Configuração do Frontend

1. **Configure a versão correta do Node.js**
   
   O projeto usa Node.js 20.19 LTS. Se você usa o NVM, navegue até o diretório do frontend e execute:
   
   ```bash
   cd front-angular
   nvm use
   ```
   
   Se a versão 20.19 não estiver instalada, o NVM irá instalar automaticamente:
   
   ```bash
   nvm install
   ```
   
   > **Nota**: O arquivo `.nvmrc` no diretório `front-angular` especifica automaticamente a versão 20.19 do Node.js. Ao executar `nvm use` ou `nvm install` sem argumentos, o NVM lerá este arquivo automaticamente.

2. **Instale o Angular CLI globalmente**
   ```bash
   npm install -g @angular/cli
   ```

3. **Instale as dependências do projeto**
   ```bash
   npm install
   ```

4. **Inicie o servidor de desenvolvimento**
   ```bash
   ng serve
   ```
   
   Ou para abrir automaticamente no navegador:
   ```bash
   ng serve --open
   ```

5. **Acesse a aplicação**
   - Frontend: `http://localhost:4200`

### Executando o Projeto Completo

Para executar tanto o backend quanto o frontend:

1. **Terminal 1 - Backend**:
   ```bash
   cd api-rest-dotnet
   dotnet run
   ```

2. **Terminal 2 - Frontend**:
   ```bash
   cd front-angular
   nvm use  # Garante que está usando Node.js 20.19
   ng serve
   ```

## 🔧 Setup Geral

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

### 🔌 Configuração de Protocolo (HTTP vs HTTPS)

Por padrão, este projeto está configurado para usar **HTTP** em desenvolvimento (mais simples e sem problemas de certificados). O backend .NET roda simultaneamente em ambas as portas:
- **HTTP**: `http://localhost:5127` (padrão configurado)
- **HTTPS**: `https://localhost:7183` (disponível, mas requer configuração)

#### 🔒 Para Usar HTTPS (Opcional)

Se você preferir usar HTTPS (protocolo mais seguro para produção), siga estes passos:

1. **Confie no certificado de desenvolvimento**:
   ```bash
   dotnet dev-certs https --trust
   ```
   
   Este comando irá:
   - Criar um certificado de desenvolvimento local (se não existir)
   - Adicionar o certificado aos certificados confiáveis do seu sistema operacional
   - Solicitar sua senha de administrador para confirmar

2. **Configure o frontend para usar HTTPS**:
   
   Edite os arquivos de environment do Angular:
   
   **`front-angular/src/environments/environment.ts`**
   ```typescript
   export const environment = {
     ApiUrl: 'https://localhost:7183/api',
   };
   ```
   
   **`front-angular/src/environments/environment.development.ts`**
   ```typescript
   export const environment = {
     ApiUrl: 'https://localhost:7183/api',
   };
   ```

3. **Descomente a linha no backend**:
   
   No arquivo `api-rest-dotnet/Program.cs`, descomente a linha:
   ```csharp
   app.UseHttpsRedirection();
   ```

4. **Reinicie ambos os servidores** (backend e frontend)

5. **Acesse via HTTPS**:
   - Backend: `https://localhost:7183`
   - Swagger: `https://localhost:7183/swagger`
   - Frontend: `http://localhost:4200` (conecta ao backend via HTTPS)

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

## 👨‍💻 Autor

**Thiago Terra**
- LinkedIn: [Thiago Terra](https://www.linkedin.com/in/thiago-terra-158a71266/)
- GitHub: [@ThiagoTerraDev](https://github.com/ThiagoTerraDev)

<!-- Adicionar que estou salvando PasswordSalt identico a PasswordHash porque, hoje em dia, o projeto usa bcrypt e ele já lida automaticamente com Salt -->
<!-- Mas isso pode mudar futuramente, então por enquanto estou fazendo assim. -->