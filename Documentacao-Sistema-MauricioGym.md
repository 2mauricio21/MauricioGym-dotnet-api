# Sistema MauricioGym - Documentação Completa

## Visão Geral

O MauricioGym é um sistema de gerenciamento de academia desenvolvido em .NET 8, seguindo o padrão de arquitetura estabelecido. O sistema utiliza um modelo de usuários unificado onde todos os tipos de usuários (administradores, donos de academia, funcionários, clientes) são representados pela mesma entidade Usuario, sendo diferenciados através do sistema de recursos.

## Principais Funcionalidades

### 1. Sistema de Login e Autenticação
- Login único para todos os tipos de usuários
- Autenticação baseada em JWT
- Controle de acesso através de recursos

### 2. Sistema de Recursos
- Define permissões e tipos de usuário
- Não utiliza propriedades booleanas como "IsAdmin"
- Recursos controlam o que cada usuário pode fazer

### 3. Auditoria
- Rastreamento de alterações, adesões de planos, exclusões
- Localizada na camada de infraestrutura
- Apenas serviço e repositório (sem API)

### 4. Simulação de Pagamento
- Sistema preparado para gateway de pagamento
- Funcionalidades de cobrança e controle financeiro

## Tipos de Usuários e Permissões

### Administrador do Sistema
- Acesso total a todos os dados
- Gerenciamento de academias
- Controle de usuários globais

### Dono da Academia
- Incluir usuários em planos
- Criar e gerenciar pagamentos
- Liberar acesso para clientes
- Visualizar quantidade de clientes
- Ver faturamento mensal
- Bloquear acesso de clientes
- Cadastrar funcionários

### Funcionários
- Liberar clientes para entrada
- Incluir clientes em planos
- Receber e atender clientes

### Clientes
- Acesso apenas ao próprio cadastro
- Visualizar e editar: nome, sobrenome, email, endereço
- Consultar status do plano

## Estrutura de Domínios

### 1. MauricioGym.Usuario
- Gerenciamento de usuários
- Sistema de login
- Controle de recursos

### 2. MauricioGym.Academia
- Cadastro e gerenciamento de academias
- Configurações específicas por unidade

### 3. MauricioGym.Plano
- Tipos de planos disponíveis
- Configurações de preços e benefícios

### 4. MauricioGym.Pagamento
- Processamento de pagamentos
- Histórico financeiro
- Simulação de gateway

### 5. MauricioGym.Acesso
- Controle de entrada e saída
- Liberação de acesso
- Bloqueios temporários

### 6. MauricioGym.Auditoria (Infra)
- Rastreamento de ações
- Log de alterações
- Histórico de operações

## Regras de Negócio

### Usuários
- Todos os usuários são representados pela entidade Usuario
- Diferenciação através de recursos atribuídos
- Email único no sistema
- CPF único quando aplicável

### Recursos
- Sistema flexível de permissões
- Recursos podem ser combinados
- Hierarquia de permissões respeitada

### Auditoria
- Registra automaticamente: IdUsuario, Descrição, Data
- Data preenchida automaticamente na criação
- Descrição definida na camada de serviço
- Não possui API própria

### Pagamentos
- Status de pagamento controlado
- Histórico completo mantido
- Preparado para integração com gateway

### Planos
- Diferentes tipos e modalidades
- Controle de vigência
- Renovação automática ou manual

## Padrões Técnicos

### Nomenclatura
- Entidades: NomeEntity
- Todas as entidades herdam de IEntity
- Repository Pattern com interfaces
- Controllers em projetos separados: NomeBase.NomeLogica.API

### Estrutura de Projetos
```
MauricioGym.Infra/
MauricioGym.Usuario/
MauricioGym.Usuario.Api/
MauricioGym.Academia/
MauricioGym.Academia.Api/
MauricioGym.Plano/
MauricioGym.Plano.Api/
MauricioGym.Pagamento/
MauricioGym.Pagamento.Api/
MauricioGym.Acesso/
MauricioGym.Acesso.Api/
```

### Banco de Dados
- SQL Server
- Relacionamentos bem definidos
- Índices para performance
- Constraints de integridade

## Considerações de Segurança

- Autenticação obrigatória para todas as operações
- Autorização baseada em recursos
- Logs de auditoria para ações críticas
- Validação de dados em todas as camadas
- Proteção contra SQL Injection
- Criptografia de senhas

## Próximos Passos

1. Implementação das entidades base
2. Criação das tabelas do banco de dados
3. Desenvolvimento dos repositórios
4. Implementação dos serviços
5. Criação das APIs
6. Testes unitários e integração
7. Documentação da API
8. Deploy e configuração