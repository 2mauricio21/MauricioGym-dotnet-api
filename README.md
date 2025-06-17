# 🏋️‍♂️ MauricioGym - Sistema de Academia com Domínios Separados

Sistema de gerenciamento de academia desenvolvido em .NET 8, com **arquitetura limpa de domínios separados** e testes unitários automatizados.

## 📚 Visão Geral

O MauricioGym é um sistema completo para academias que gerencia usuários, planos, check-ins, mensalidades e controle financeiro. Foi desenvolvido seguindo princípios de **Clean Architecture** e **SOLID**, com uma clara separação de responsabilidades em domínios independentes.

## 🏗️ Arquitetura de Domínios

O sistema foi estruturado em **2 contextos principais**, cada um com sua própria API independente:

### 🔧 **Domínio Administrador** (Porta 5001)

Responsável pela gestão administrativa completa:

- **Administradores**: Gerenciamento de administradores do sistema
- **Usuários**: CRUD completo de usuários
- **Planos**: Criação e gerenciamento de planos de mensalidade
- **Caixa**: Controle financeiro e relatórios
- **Permissões**: Controle de acesso e manipulação de usuários
- **Usuário-Plano**: Associação de usuários aos planos

### 👤 **Domínio Usuario** (Porta 5002)

Responsável pelas ações dos usuários finais:

- **Check-in**: Registrar entrada na academia (com validação automática de mensalidade)
- **Mensalidades**: Consultar mensalidades e fazer pagamentos com descontos

## 📁 Estrutura do Projeto

```plaintext
📁 MauricioGym/
├── 👨‍💼 Administrador/           # Core do domínio Administrador
│   ├── Entities/               # Entidades de domínio
│   ├── Repositories/           # Repositórios e acesso a dados
│   └── Services/               # Regras de negócio e serviços
├── 🌐 Administrador.Api/        # API REST do Administrador
│   ├── Controllers/            # Endpoints da API
│   └── Startup.cs              # Configuração da API
├── 🧪 Administrador.Testes/     # Testes unitários do Administrador
├── 🏃‍♂️ Usuario/                 # Core do domínio Usuario
│   ├── Entities/               # Entidades de domínio
│   ├── Repositories/           # Repositórios e acesso a dados
│   └── Services/               # Regras de negócio e serviços
├── 🌐 Usuario.Api/              # API REST do Usuario
│   ├── Controllers/            # Endpoints da API
│   └── Startup.cs              # Configuração da API
├── 🧪 Usuario.Testes/           # Testes unitários do Usuario
├── 💾 sql/                      # Scripts de banco de dados
└── 📜 *.bat                     # Scripts de automação
```

## 🎯 Regras de Negócio Implementadas

1. **Check-in Inteligente**: Usuário só consegue fazer check-in se a mensalidade estiver em dia
2. **Descontos Automáticos**: 10% trimestral, 20% semestral, 30% anual
3. **Remoção Lógica**: Dados nunca são deletados fisicamente (soft delete)
4. **APIs Independentes**: Cada domínio tem sua própria API REST
5. **Validações de Dados**: Email único, campos obrigatórios, etc.
6. **Controle Financeiro**: Registros automáticos no caixa

## 🚀 Início Rápido (3 passos)

### Pré-requisitos

- .NET 8 SDK instalado
- SQL Server LocalDB (incluído no Visual Studio ou SQL Server Express)

### 1. Setup do Banco de Dados

```bash
# Execute o script que faz tudo automaticamente
setup-banco.bat
```

Este script cria o banco `MauricioGymDB` com todas as tabelas e dados de exemplo para testes.

### 2. Build e Testes

```bash
# Execute o script que testa tudo automaticamente
testar-apis.bat
```

Este script compila a solução e executa todos os testes unitários para garantir que tudo está funcionando corretamente.

### 3. Executar as APIs

Você pode iniciar cada API independentemente:

**API Administrador:**

```bash
dotnet run --project Administrador.Api/MauricioGym.Administrador.Api.csproj
```

Acesse: [http://localhost:5001/swagger](http://localhost:5001/swagger)

**API Usuario:**

```bash
dotnet run --project Usuario.Api/MauricioGym.Usuario.Api.csproj
```

Acesse: [http://localhost:5002/swagger](http://localhost:5002/swagger)

## 📊 Dados de Exemplo Inclusos

O banco de dados é criado com dados de exemplo para facilitar testes:

- **2 Administradores**: [admin@mauriciogym.com](mailto:admin@mauriciogym.com), [mauricio@mauriciogym.com](mailto:mauricio@mauriciogym.com)
- **4 Planos**: Mensal (R$ 99,90), Trimestral (R$ 269,90), Semestral (R$ 499,90), Anual (R$ 999,90)
- **5 Usuários**: João, Maria, Pedro, Ana, Carlos
- **5 Vínculos**: Cada usuário com um plano diferente
- **3 Check-ins**: Registros de entrada recentes
- **5 Mensalidades**: Mix de pagas e pendentes

## 🧪 Endpoints para Testar

### API Administrador (porta 5001)

```plaintext
GET    /api/Administrador             - Lista administradores
GET    /api/Usuario                   - Lista usuários (clientes)
GET    /api/Plano                     - Lista planos disponíveis
GET    /api/Caixa                     - Resumo financeiro
POST   /api/UsuarioPlano              - Associa usuário a um plano
```

### API Usuario (porta 5002)

```plaintext
POST   /api/CheckIn                   - Realizar check-in
GET    /api/CheckIn/usuario/{id}      - Listar check-ins por usuário
GET    /api/Mensalidade/usuario/{id}  - Listar mensalidades do usuário
POST   /api/Mensalidade/pagamento     - Registrar pagamento
```

## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Dapper**: ORM para acesso aos dados
- **SQL Server**: Banco de dados relacional
- **xUnit + Moq**: Framework de testes
- **Swagger**: Documentação da API
- **FluentValidation**: Validação de dados

## ⚡ Solução de Problemas

### Erro de conexão com banco?

```bash
# Verificar LocalDB
sqllocaldb info

# Iniciar LocalDB se necessário
sqllocaldb start mssqllocaldb

# Recriar banco
setup-banco.bat
```

### Testes falhando?

```bash
# Execute o script que compila e testa tudo
testar-completo.bat
```

### Swagger não carrega?

- Verifique se a API está rodando
- Acesse: [http://localhost:5001/swagger](http://localhost:5001/swagger) (não https)
- Certifique-se de que está rodando em ambiente de Development

## 📝 Documentação das APIs

Cada API possui documentação Swagger completa que pode ser acessada pelos seguintes endereços:

- **Administrador API**: [http://localhost:5001/swagger](http://localhost:5001/swagger)
- **Usuario API**: [http://localhost:5002/swagger](http://localhost:5002/swagger)

## 🧩 Arquitetura e Padrões

O projeto segue uma arquitetura limpa com separação clara de responsabilidades:

- **Entities**: Modelos de domínio
- **Repositories**: Acesso a dados com Dapper
- **Services**: Regras de negócio
- **Controllers**: Endpoints REST
- **Interfaces**: Abstrações para inversão de dependência
- **Testes**: Validação das regras de negócio

Cada domínio (Administrador e Usuario) possui seu próprio conjunto dessas camadas, permitindo evolução independente.
- **Swagger**: Documentação da API
- **xUnit**: Framework de testes
- **Moq**: Biblioteca de mocking

## 📁 Estrutura do Projeto

```
MauricioGym/
├── Controllers/          # Controllers Web API
├── Entities/            # Entidades do domínio
├── Repositories/        # Repositórios de dados
├── Services/           # Serviços de negócio
└── IServiceCollectionExtension.cs

MauricioGym.Api/
├── Controllers/        # Controllers da API
├── Program.cs         # Configuração da aplicação
└── appsettings.json   # Configurações

MauricioGym.Testes/
├── *Test.cs          # Testes unitários
└── MauricioGym.Testes.csproj
```

## 🔧 Comandos Úteis

### Build
```bash
dotnet build
```

### Executar Testes
```bash
dotnet test
```

### Executar API
```bash
dotnet run --project MauricioGym.Api
```

### Restore de Pacotes
```bash
dotnet restore
```

## 📊 Cobertura de Testes

O projeto possui **51 testes unitários** cobrindo:
- Controllers
- Services
- Validações
- Repositórios

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 🏃‍♂️ Execução Rápida

Para executar rapidamente:

```bash
# Restaurar, buildar e executar testes
dotnet restore && dotnet build && dotnet test

# Executar a API
dotnet run --project MauricioGym.Api
```

## 📝 Notas

- Este projeto roda 100% localmente
- Não possui dependências de AWS ou Azure
- Usar SQL Server local ou remoto conforme necessário
- Swagger disponível em `/swagger` quando executando a API

## 🏋️‍♂️ Dados de Exemplo

Execute o script `sql/script_completo_mauriciogym.sql` para criar:
- 1 administrador: admin@mauriciogym.com
- 5 alunos já cadastrados
- Planos (mensal, trimestral, anual)
- Mensalidades, check-ins e caixa populados

## GitHub
Repositório: https://github.com/2mauricio21/MauricioGym-dotnet-api.git

---

> Projeto 100% local, sem dependências de nuvem, pronto para desenvolvimento e estudo.
