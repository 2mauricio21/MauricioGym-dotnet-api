# MauricioGym - Scripts de Criação das Tabelas SQL Server

## 1. Tabelas do Domínio Usuario

### Tabela Usuarios
```sql
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Sobrenome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Senha NVARCHAR(255) NOT NULL,
    CPF NVARCHAR(14) UNIQUE,
    Telefone NVARCHAR(20),
    DataNascimento DATE,
    Endereco NVARCHAR(500),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(2),
    CEP NVARCHAR(10),
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    DataUltimoLogin DATETIME NULL
);

CREATE INDEX IX_Usuarios_Email ON Usuarios(Email);
CREATE INDEX IX_Usuarios_CPF ON Usuarios(CPF);
CREATE INDEX IX_Usuarios_Ativo ON Usuarios(Ativo);
```

### Tabela Recursos
```sql
CREATE TABLE Recursos (
    IdRecurso INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(500),
    Codigo NVARCHAR(50) NOT NULL UNIQUE,
    Ativo BIT NOT NULL DEFAULT 1
);

CREATE INDEX IX_Recursos_Codigo ON Recursos(Codigo);
CREATE INDEX IX_Recursos_Ativo ON Recursos(Ativo);
```

### Tabela UsuarioRecursos
```sql
CREATE TABLE UsuarioRecursos (
    IdUsuarioRecurso INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdRecurso INT NOT NULL,
    IdAcademia INT NULL, -- NULL para recursos globais
    DataAtribuicao DATETIME NOT NULL DEFAULT GETDATE(),
    Ativo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT FK_UsuarioRecursos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_UsuarioRecursos_Recurso FOREIGN KEY (IdRecurso) REFERENCES Recursos(IdRecurso),
    CONSTRAINT UQ_UsuarioRecursos_Usuario_Recurso_Academia UNIQUE (IdUsuario, IdRecurso, IdAcademia)
);

CREATE INDEX IX_UsuarioRecursos_Usuario ON UsuarioRecursos(IdUsuario);
CREATE INDEX IX_UsuarioRecursos_Recurso ON UsuarioRecursos(IdRecurso);
CREATE INDEX IX_UsuarioRecursos_Academia ON UsuarioRecursos(IdAcademia);
CREATE INDEX IX_UsuarioRecursos_Ativo ON UsuarioRecursos(Ativo);
```

## 2. Tabelas do Domínio Academia

### Tabela Academias
```sql
CREATE TABLE Academias (
    IdAcademia INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    CNPJ NVARCHAR(18) NOT NULL UNIQUE,
    Endereco NVARCHAR(500),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(2),
    CEP NVARCHAR(10),
    Telefone NVARCHAR(20),
    Email NVARCHAR(255),
    HorarioAbertura TIME NOT NULL,
    HorarioFechamento TIME NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_Academias_CNPJ ON Academias(CNPJ);
CREATE INDEX IX_Academias_Ativo ON Academias(Ativo);
```

### Tabela UsuarioAcademias
```sql
CREATE TABLE UsuarioAcademias (
    IdUsuarioAcademia INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdAcademia INT NOT NULL,
    DataVinculo DATETIME NOT NULL DEFAULT GETDATE(),
    Ativo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT FK_UsuarioAcademias_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_UsuarioAcademias_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
    CONSTRAINT UQ_UsuarioAcademias_Usuario_Academia UNIQUE (IdUsuario, IdAcademia)
);

CREATE INDEX IX_UsuarioAcademias_Usuario ON UsuarioAcademias(IdUsuario);
CREATE INDEX IX_UsuarioAcademias_Academia ON UsuarioAcademias(IdAcademia);
CREATE INDEX IX_UsuarioAcademias_Ativo ON UsuarioAcademias(Ativo);
```

## 3. Tabelas do Domínio Plano

### Tabela Planos
```sql
CREATE TABLE Planos (
    IdPlano INT IDENTITY(1,1) PRIMARY KEY,
    IdAcademia INT NOT NULL,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(1000),
    Valor DECIMAL(10,2) NOT NULL,
    DuracaoEmDias INT NOT NULL,
    PermiteAcessoTotal BIT NOT NULL DEFAULT 1,
    HorarioInicioPermitido TIME NULL,
    HorarioFimPermitido TIME NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Planos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia)
);

CREATE INDEX IX_Planos_Academia ON Planos(IdAcademia);
CREATE INDEX IX_Planos_Ativo ON Planos(Ativo);
CREATE INDEX IX_Planos_Valor ON Planos(Valor);
```

### Tabela UsuarioPlanos
```sql
CREATE TABLE UsuarioPlanos (
    IdUsuarioPlano INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdPlano INT NOT NULL,
    DataInicio DATE NOT NULL,
    DataFim DATE NOT NULL,
    ValorPago DECIMAL(10,2) NOT NULL,
    StatusPlano NVARCHAR(20) NOT NULL DEFAULT 'Ativo', -- Ativo, Suspenso, Cancelado, Expirado
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_UsuarioPlanos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_UsuarioPlanos_Plano FOREIGN KEY (IdPlano) REFERENCES Planos(IdPlano),
    CONSTRAINT CK_UsuarioPlanos_StatusPlano CHECK (StatusPlano IN ('Ativo', 'Suspenso', 'Cancelado', 'Expirado')),
    CONSTRAINT CK_UsuarioPlanos_DataFim CHECK (DataFim >= DataInicio)
);

CREATE INDEX IX_UsuarioPlanos_Usuario ON UsuarioPlanos(IdUsuario);
CREATE INDEX IX_UsuarioPlanos_Plano ON UsuarioPlanos(IdPlano);
CREATE INDEX IX_UsuarioPlanos_DataInicio ON UsuarioPlanos(DataInicio);
CREATE INDEX IX_UsuarioPlanos_DataFim ON UsuarioPlanos(DataFim);
CREATE INDEX IX_UsuarioPlanos_StatusPlano ON UsuarioPlanos(StatusPlano);
CREATE INDEX IX_UsuarioPlanos_Ativo ON UsuarioPlanos(Ativo);
```

## 4. Tabelas do Domínio Pagamento

### Tabela FormasPagamento
```sql
CREATE TABLE FormasPagamento (
    IdFormaPagamento INT IDENTITY(1,1) PRIMARY KEY,
    IdAcademia INT NOT NULL,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200),
    AceitaParcelamento BIT NOT NULL DEFAULT 0,
    MaximoParcelas INT NOT NULL DEFAULT 1,
    TaxaProcessamento DECIMAL(5,2) NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT FK_FormasPagamento_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia)
);

CREATE INDEX IX_FormasPagamento_Academia ON FormasPagamento(IdAcademia);
CREATE INDEX IX_FormasPagamento_Ativo ON FormasPagamento(Ativo);
```

### Tabela Pagamentos
```sql
CREATE TABLE Pagamentos (
    IdPagamento INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdUsuarioPlano INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME NOT NULL DEFAULT GETDATE(),
    DataVencimento DATE NULL,
    FormaPagamento NVARCHAR(50) NOT NULL,
    StatusPagamento NVARCHAR(20) NOT NULL DEFAULT 'Pendente', -- Pendente, Aprovado, Rejeitado, Cancelado
    TransacaoId NVARCHAR(100),
    ObservacaoPagamento NVARCHAR(1000),
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Pagamentos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_Pagamentos_UsuarioPlano FOREIGN KEY (IdUsuarioPlano) REFERENCES UsuarioPlanos(IdUsuarioPlano),
    CONSTRAINT CK_Pagamentos_StatusPagamento CHECK (StatusPagamento IN ('Pendente', 'Aprovado', 'Rejeitado', 'Cancelado')),
    CONSTRAINT CK_Pagamentos_Valor CHECK (Valor > 0)
);

CREATE INDEX IX_Pagamentos_Usuario ON Pagamentos(IdUsuario);
CREATE INDEX IX_Pagamentos_UsuarioPlano ON Pagamentos(IdUsuarioPlano);
CREATE INDEX IX_Pagamentos_DataPagamento ON Pagamentos(DataPagamento);
CREATE INDEX IX_Pagamentos_StatusPagamento ON Pagamentos(StatusPagamento);
CREATE INDEX IX_Pagamentos_TransacaoId ON Pagamentos(TransacaoId);
CREATE INDEX IX_Pagamentos_Ativo ON Pagamentos(Ativo);
```

## 5. Tabelas do Domínio Acesso

### Tabela Acessos
```sql
CREATE TABLE Acessos (
    IdAcesso INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdAcademia INT NOT NULL,
    DataHoraEntrada DATETIME NOT NULL DEFAULT GETDATE(),
    DataHoraSaida DATETIME NULL,
    TipoAcesso NVARCHAR(20) NOT NULL DEFAULT 'Normal', -- Normal, Visitante, Funcionario
    ObservacaoAcesso NVARCHAR(500),
    AcessoLiberado BIT NOT NULL DEFAULT 1,
    MotivoNegacao NVARCHAR(500),
    
    CONSTRAINT FK_Acessos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_Acessos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
    CONSTRAINT CK_Acessos_TipoAcesso CHECK (TipoAcesso IN ('Normal', 'Visitante', 'Funcionario')),
    CONSTRAINT CK_Acessos_DataHoraSaida CHECK (DataHoraSaida IS NULL OR DataHoraSaida >= DataHoraEntrada)
);

CREATE INDEX IX_Acessos_Usuario ON Acessos(IdUsuario);
CREATE INDEX IX_Acessos_Academia ON Acessos(IdAcademia);
CREATE INDEX IX_Acessos_DataHoraEntrada ON Acessos(DataHoraEntrada);
CREATE INDEX IX_Acessos_DataHoraSaida ON Acessos(DataHoraSaida);
CREATE INDEX IX_Acessos_TipoAcesso ON Acessos(TipoAcesso);
CREATE INDEX IX_Acessos_AcessoLiberado ON Acessos(AcessoLiberado);
```

### Tabela BloqueiosAcesso
```sql
CREATE TABLE BloqueiosAcesso (
    IdBloqueioAcesso INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdAcademia INT NOT NULL,
    DataInicioBloqueio DATETIME NOT NULL DEFAULT GETDATE(),
    DataFimBloqueio DATETIME NULL,
    MotivoBloqueio NVARCHAR(200) NOT NULL,
    ObservacaoBloqueio NVARCHAR(1000),
    Ativo BIT NOT NULL DEFAULT 1,
    IdUsuarioResponsavel INT NOT NULL,
    
    CONSTRAINT FK_BloqueiosAcesso_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_BloqueiosAcesso_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
    CONSTRAINT FK_BloqueiosAcesso_UsuarioResponsavel FOREIGN KEY (IdUsuarioResponsavel) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT CK_BloqueiosAcesso_DataFimBloqueio CHECK (DataFimBloqueio IS NULL OR DataFimBloqueio >= DataInicioBloqueio)
);

CREATE INDEX IX_BloqueiosAcesso_Usuario ON BloqueiosAcesso(IdUsuario);
CREATE INDEX IX_BloqueiosAcesso_Academia ON BloqueiosAcesso(IdAcademia);
CREATE INDEX IX_BloqueiosAcesso_DataInicioBloqueio ON BloqueiosAcesso(DataInicioBloqueio);
CREATE INDEX IX_BloqueiosAcesso_DataFimBloqueio ON BloqueiosAcesso(DataFimBloqueio);
CREATE INDEX IX_BloqueiosAcesso_Ativo ON BloqueiosAcesso(Ativo);
CREATE INDEX IX_BloqueiosAcesso_UsuarioResponsavel ON BloqueiosAcesso(IdUsuarioResponsavel);
```

## 6. Tabela de Auditoria (Infra)

### Tabela Auditorias
```sql
CREATE TABLE Auditorias (
    IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Descricao NVARCHAR(1000) NOT NULL,
    Data DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Auditorias_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);

CREATE INDEX IX_Auditorias_Usuario ON Auditorias(IdUsuario);
CREATE INDEX IX_Auditorias_Data ON Auditorias(Data);
```

## 7. Dados Iniciais (Seeds)

### Recursos Básicos do Sistema
```sql
-- Inserir recursos básicos
INSERT INTO Recursos (Nome, Descricao, Codigo) VALUES
('Administrador Global', 'Acesso total ao sistema', 'ADMIN_GLOBAL'),
('Criar Academia', 'Permissão para criar novas academias', 'CRIAR_ACADEMIA'),
('Gerenciar Usuários Globais', 'Gerenciar usuários em todas as academias', 'GERENCIAR_USUARIOS_GLOBAIS'),
('Dono da Academia', 'Proprietário da academia com acesso total', 'DONO_ACADEMIA'),
('Funcionário', 'Funcionário da academia', 'FUNCIONARIO'),
('Cliente', 'Cliente da academia', 'CLIENTE'),
('Gerenciar Planos', 'Criar e editar planos da academia', 'GERENCIAR_PLANOS'),
('Gerenciar Pagamentos', 'Processar pagamentos', 'GERENCIAR_PAGAMENTOS'),
('Liberar Acesso', 'Liberar entrada de clientes', 'LIBERAR_ACESSO'),
('Bloquear Acesso', 'Bloquear acesso de clientes', 'BLOQUEAR_ACESSO'),
('Ver Relatórios', 'Visualizar relatórios financeiros', 'VER_RELATORIOS'),
('Cadastrar Funcionários', 'Cadastrar novos funcionários', 'CADASTRAR_FUNCIONARIOS');
```

### Usuário Administrador Inicial
```sql
-- Inserir usuário administrador inicial (senha: admin123 - deve ser alterada)
INSERT INTO Usuarios (Nome, Sobrenome, Email, Senha, Ativo) VALUES
('Administrador', 'Sistema', 'admin@mauriciogym.com', 'SENHA_COM_HASH_AQUI', 1);

-- Atribuir o recurso de Administrador Global ao usuário inicial
INSERT INTO UsuarioRecursos (IdUsuario, IdRecurso, IdAcademia, Ativo)
VALUES (1, 1, NULL, 1);