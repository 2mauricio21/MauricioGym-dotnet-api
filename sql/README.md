# 🗄️ Setup do Banco de Dados - MauricioGym

## 📖 Visão Geral

Este diretório contém os scripts SQL necessários para configurar o banco de dados do sistema MauricioGym.

## 📁 Arquivos

- **`create-database-complete.sql`** - Script principal de criação do banco
- **`verificar-banco.sql`** - Script de verificação da instalação
- **`README.md`** - Esta documentação

## 🚀 Instalação Rápida

### Pré-requisitos

- SQL Server 2019 ou superior
- SQL Server Management Studio (SSMS)
- Permissões de administrador no SQL Server

### Passos de Instalação

1. **Abra o SSMS** e conecte no servidor
2. **Execute**: `create-database-complete.sql`
3. **Verifique**: `verificar-banco.sql`
4. **Pronto!** Sistema configurado

## 📊 Estrutura do Banco

### Banco de Dados
- **Nome**: `MauricioGymDB`
- **Collation**: `SQL_Latin1_General_CP1_CI_AS`
- **Tabelas**: 13 tabelas principais

### Tabelas Criadas

| Tabela | Descrição | Registros Iniciais |
|--------|-----------|-------------------|
| `Usuarios` | Dados dos usuários | 1 (admin) |
| `Autenticacao` | Credenciais de login | 1 (admin) |
| `Academias` | Informações das academias | 1 (matriz) |
| `Funcionarios` | Dados dos funcionários | 1 (admin) |
| `Planos` | Planos de assinatura | 3 (básico, premium, vip) |
| `Usuarios_Planos` | Relacionamento usuário-plano | 1 |
| `Pagamentos` | Histórico de pagamentos | 0 |
| `Acessos` | Log de acessos | 0 |
| `Bloqueios` | Controle de bloqueios | 0 |
| `RecuperacaoSenha` | Tokens de recuperação | 0 |
| `Logs` | Logs do sistema | 0 |
| `Configuracoes` | Configurações gerais | 5 |
| `Notificacoes` | Sistema de notificações | 0 |

## 👤 Usuário Padrão

O script cria automaticamente um usuário administ