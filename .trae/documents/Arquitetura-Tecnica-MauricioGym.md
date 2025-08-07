# Documentação de Arquitetura Técnica - Sistema MauricioGym

**STATUS: ✅ ARQUITETURA COMPLETAMENTE IMPLEMENTADA**

## 1. Design da Arquitetura

```mermaid
graph TD
    A[Cliente/Browser] --> B[APIs .NET 8.0]
    B --> C[Camada de Serviços]
    C --> D[Camada de Repositórios]
    D --> E[SQL Server Database]
    
    F[Sistema de Auditoria] --> E
    G[Sistema de Autenticação JWT] --> B
    
    subgraph "Camada de Apresentação"
        A
    end
    
    subgraph "Camada de APIs"
        B
        G
    end
    
    subgraph "Camada de Negócio"
        C
        F
    end
    
    subgraph "Camada de Dados"
        D
        E
    end
```

## 2. Descrição das Tecnologias ✅ IMPLEMENTADAS

* **✅ Backend**: .NET 8.0 + ASP.NET Core Web API + Dapper ORM - **FUNCIONAL**

* **✅ Banco de Dados**: SQL Server com queries otimizadas - **OPERACIONAL**

* **✅ Autenticação**: JWT Bearer Token com validação customizada - **IMPLEMENTADO**

* **✅ Documentação**: Swagger/OpenAPI para documentação automática das APIs - **ATIVO**

* **✅ Logging**: Sistema de auditoria integrado com rastreamento completo - **FUNCIONAL**

* **✅ Validação**: Validadores customizados para regras de negócio complexas - **IMPLEMENTADO**

## 3. Definições de Rotas ✅ TODAS FUNCIONAIS

| Rota            | Propósito                                             | Status |
| --------------- | ----------------------------------------------------- | ------ |
| /api/usuario    | Gestão completa de usuários (CRUD, login, permissões) | ✅ ATIVO |
| /api/academia   | Gestão de academias e associações com usuários        | ✅ ATIVO |
| /api/plano      | Gestão de planos e vinculação com usuários            | ✅ ATIVO |
| /api/pagamento  | Processamento e histórico de pagamentos               | ✅ ATIVO |
| /api/acesso     | Controle de entrada e bloqueios de acesso             | ✅ ATIVO |
| /api/auditoria  | Logs e relatórios de auditoria do sistema             | ✅ ATIVO |
| /api/auth       | Autenticação, autorização e renovação de tokens       | ✅ ATIVO |
| /api/relatorios | Geração de relatórios gerenciais e operacionais       | ✅ ATIVO |

## 4. Definições de API

### 4.1 APIs Principais

**Autenticação de usuários**

```
POST /api/auth/login
```

Request:

| Nome do Parâmetro | Tipo   | Obrigatório | Descrição                        |
| ----------------- | ------ | ----------- | -------------------------------- |
| email             | string | true        | Email do usuário para login      |
| senha             | string | true        | Senha do usuário (será hasheada) |

Response:

| Nome do Parâmetro | Tipo          | Descrição                   |
| ----------------- | ------------- | --------------------------- |
| token             | string        | JWT token para autenticação |
| usuario           | UsuarioEntity | Dados do usuário logado     |
| expiresAt         | DateTime      | Data de expiração do token  |

Exemplo:

```json
{
  "email": "admin@mauriciogym.com",
  "senha": "senha123"
}
```

**Gestão de usuários**

```
POST /api/usuario
GET /api/usuario/{id}
PUT /api/usuario/{id}
DELETE /api/usuario/{id}
GET /api/usuario/email/{email}
GET /api/usuario/cpf/{cpf}
```

**Controle de acesso**

```
POST /api/acesso/validar
GET /api/acesso/historico/{idUsuario}
POST /api/acesso/bloquear
DELETE /api/acesso/desbloquear/{id}
```

**Gestão de pagamentos**

```
POST /api/pagamento
GET /api/pagamento/usuario/{idUsuario}
GET /api/pagamento/pendentes
PUT /api/pagamento/{id}/confirmar
```

## 5. Diagrama da Arquitetura do Servidor

```mermaid
graph TD
    A[Cliente/Frontend] --> B[Controller Layer]
    B --> C[Service Layer]
    C --> D[Repository Layer]
    D --> E[(SQL Server Database)]
    
    F[Validator Layer] --> C
    G[Auditoria Service] --> C
    H[JWT Middleware] --> B
    
    subgraph Servidor
        B
        C
        D
        F
        G
        H
    end
```

## 6. Modelo de Dados

### 6.1 Definição do Modelo de Dados

```mermaid
erDiagram
    USUARIOS ||--o{ USUARIO_RECURSOS : possui
    RECURSOS ||--o{ USUARIO_RECURSOS : concede
    USUARIOS ||--o{ USUARIO_ACADEMIAS : frequenta
    ACADEMIAS ||--o{ USUARIO_ACADEMIAS : recebe
    USUARIOS ||--o{ USUARIO_PLANOS : contrata
    PLANOS ||--o{ USUARIO_PLANOS : oferece
    USUARIOS ||--o{ PAGAMENTOS : realiza
    FORMAS_PAGAMENTO ||--o{ PAGAMENTOS : processa
    USUARIOS ||--o{ ACESSOS : registra
    USUARIOS ||--o{ BLOQUEIOS_ACESSO : sofre
    USUARIOS ||--o{ AUDITORIA : gera

    USUARIOS {
        int IdUsuario PK
        string Nome
        string Sobrenome
        string Email UK
        string Senha
        string CPF UK
        string Telefone
        datetime DataNascimento
        string Endereco
        string Cidade
        string Estado
        string CEP
        bit Ativo
        datetime DataCadastro
        datetime DataUltimoLogin
    }
    
    RECURSOS {
        int IdRecurso PK
        string Nome
        string Descricao
        bit Ativo
    }
    
    USUARIO_RECURSOS {
        int IdUsuarioRecurso PK
        int IdUsuario FK
        int IdRecurso FK
        datetime DataConcessao
        bit Ativo
    }
    
    ACADEMIAS {
        int IdAcademia PK
        string Nome
        string CNPJ UK
        string Telefone
        string Email
        string Endereco
        string Cidade
        string Estado
        string CEP
        bit Ativo
        datetime DataCadastro
    }
    
    USUARIO_ACADEMIAS {
        int IdUsuarioAcademia PK
        int IdUsuario FK
        int IdAcademia FK
        datetime DataAssociacao
        bit Ativo
    }
    
    PLANOS {
        int IdPlano PK
        string Nome
        string Descricao
        decimal Valor
        int DuracaoMeses
        bit Ativo
        datetime DataCadastro
    }
    
    USUARIO_PLANOS {
        int IdUsuarioPlano PK
        int IdUsuario FK
        int IdPlano FK
        datetime DataInicio
        datetime DataFim
        bit Ativo
    }
    
    FORMAS_PAGAMENTO {
        int IdFormaPagamento PK
        string Nome
        string Descricao
        bit Ativo
    }
    
    PAGAMENTOS {
        int IdPagamento PK
        int IdUsuario FK
        int IdFormaPagamento FK
        decimal Valor
        datetime DataPagamento
        datetime DataVencimento
        int Status
        string Observacoes
    }
    
    ACESSOS {
        int IdAcesso PK
        int IdUsuario FK
        int IdAcademia FK
        datetime DataHoraEntrada
        datetime DataHoraSaida
        bit AcessoLiberado
    }
    
    BLOQUEIOS_ACESSO {
        int IdBloqueio PK
        int IdUsuario FK
        string Motivo
        datetime DataInicio
        datetime DataFim
        bit Ativo
    }
    
    AUDITORIA {
        int IdAuditoria PK
        int IdUsuario FK
        string Acao
        datetime DataHora
        string Detalhes
    }
```

### 6.2 Linguagem de Definição de Dados (DDL)

**Tabela de Usuários**

```sql
-- Criar tabela de usuários
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Sobrenome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Senha NVARCHAR(255) NOT NULL,
    CPF NVARCHAR(14) UNIQUE NOT NULL,
    Telefone NVARCHAR(20),
    DataNascimento DATE,
    Endereco NVARCHAR(500),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(2),
    CEP NVARCHAR(10),
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME2 DEFAULT GETDATE(),
    DataUltimoLogin DATETIME2
);

-- Criar índices
CREATE INDEX IX_Usuarios_Email ON Usuarios(Email);
CREATE INDEX IX_Usuarios_CPF ON Usuarios(CPF);
CREATE INDEX IX_Usuarios_Ativo ON Usuarios(Ativo);
```

**Tabela de Academias**

```sql
-- Criar tabela de academias
CREATE TABLE Academias (
    IdAcademia INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    CNPJ NVARCHAR(18) UNIQUE NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Endereco NVARCHAR(500),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(2),
    CEP NVARCHAR(10),
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME2 DEFAULT GETDATE()
);

-- Criar índices
CREATE INDEX IX_Academias_CNPJ ON Academias(CNPJ);
CREATE INDEX IX_Academias_Ativo ON Academias(Ativo);
```

**Tabela de Planos**

```sql
-- Criar tabela de planos
CREATE TABLE Planos (
    IdPlano INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(500),
    Valor DECIMAL(10,2) NOT NULL,
    DuracaoMeses INT NOT NULL,
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME2 DEFAULT GETDATE()
);

-- Criar índices
CREATE INDEX IX_Planos_Ativo ON Planos(Ativo);
CREATE INDEX IX_Planos_Valor ON Planos(Valor);
```

**Tabela de Pagamentos**

```sql
-- Criar tabela de formas de pagamento
CREATE TABLE FormasPagamento (
    IdFormaPagamento INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200),
    Ativo BIT DEFAULT 1
);

-- Criar tabela de pagamentos
CREATE TABLE Pagamentos (
    IdPagamento INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdFormaPagamento INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME2 DEFAULT GETDATE(),
    DataVencimento DATETIME2 NOT NULL,
    Status INT NOT NULL DEFAULT 1, -- 1=Pendente, 2=Pago, 3=Cancelado
    Observacoes NVARCHAR(500),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdFormaPagamento) REFERENCES FormasPagamento(IdFormaPagamento)
);

-- Criar índices
CREATE INDEX IX_Pagamentos_Usuario ON Pagamentos(IdUsuario);
CREATE INDEX IX_Pagamentos_Status ON Pagamentos(Status);
CREATE INDEX IX_Pagamentos_DataVencimento ON Pagamentos(DataVencimento);
```

**Dados Iniciais**

```sql
-- Inserir formas de pagamento padrão
INSERT INTO FormasPagamento (Nome, Descricao) VALUES 
('Dinheiro', 'Pagamento em espécie'),
('Cartão de Débito', 'Pagamento com cartão de débito'),
('Cartão de Crédito', 'Pagamento com cartão de crédito'),
('PIX', 'Pagamento via PIX'),
('Transferência Bancária', 'Transferência entre contas');

-- Inserir recursos padrão
INSERT INTO Recursos (Nome, Descricao) VALUES 
('ADMIN_TOTAL', 'Acesso administrativo completo'),
('GERENCIAR_USUARIOS', 'Gerenciar usuários do sistema'),
('GERENCIAR_ACADEMIAS', 'Gerenciar academias'),
('GERENCIAR_PLANOS', 'Gerenciar planos e preços'),
('PROCESSAR_PAGAMENTOS', 'Processar pagamentos'),
('CONTROLAR_ACESSO', 'Controlar acesso às academias'),
('VISUALIZAR_RELATORIOS', 'Visualizar relatórios'),
('ACESSO_ALUNO', 'Acesso básico de aluno');
```

