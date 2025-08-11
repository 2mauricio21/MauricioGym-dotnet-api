# 🧪 Guia de Teste Completo - Sistema MauricioGym

## 📋 Pré-requisitos

- SQL Server instalado e rodando
- SQL Server Management Studio (SSMS)
- APIs rodando:
  - Gateway: http://localhost:8000
  - Segurança: http://localhost:5000
- Postman (opcional)

## 🗄️ Parte 1: Configuração do Banco de Dados

### Passo 1: Executar Script de Criação

1. **Abra o SQL Server Management Studio**
2. **Conecte no servidor** `localhost` (ou `.`)
3. **Abra o arquivo**: `sql/create-database-complete.sql`
4. **Execute todo o script** (pressione F5 ou Ctrl+E)
5. **Aguarde a conclusão** (pode levar alguns segundos)

### Passo 2: Verificar Criação das Tabelas

1. **Abra o arquivo**: `sql/verificar-banco.sql`
2. **Execute o script de verificação**
3. **Verifique a saída**:
   - ✅ Banco MauricioGymDB encontrado
   - ✅ 13/13 tabelas encontradas
   - ✅ 11/11 colunas da tabela Autenticacao
   - ✅ Usuário admin@mauriciogym.com encontrado

**Se algum item aparecer com ❌, execute novamente o script de criação.**

## 🔐 Parte 2: Teste de Autenticação

### Dados de Teste Disponíveis

- **Email**: `admin@mauriciogym.com`
- **Senha**: `admin123`

### Teste 1: Login via Swagger do Gateway

1. **Acesse**: http://localhost:8000/swagger
2. **Localize a seção**: `🔐 Segurança`
3. **Clique em**: `POST /api/seguranca/login`
4. **Clique em**: `Try it out`
5. **Preencha o JSON**:
   ```json
   {
     "email": "admin@mauriciogym.com",
     "senha": "admin123"
   }
   ```
6. **Clique em**: `Execute`
7. **Resultado esperado**: Status 200 com token JWT

### Teste 2: Login via Swagger da API Segurança

1. **Acesse**: http://localhost:5000/swagger
2. **Localize**: `POST /login`
3. **Execute o mesmo teste** do Passo anterior
4. **Resultado esperado**: Status 200 com token JWT

### Teste 3: Login via Postman

1. **Crie uma nova requisição POST**
2. **URL**: `http://localhost:8000/api/seguranca/login`
3. **Headers**:
   ```
   Content-Type: application/json
   ```
4. **Body (raw JSON)**:
   ```json
   {
     "email": "admin@mauriciogym.com",
     "senha": "admin123"
   }
   ```
5. **Envie a requisição**
6. **Resultado esperado**: Status 200 com token JWT

### Teste 4: Validação de Token

1. **Copie o token** recebido no login
2. **Teste o endpoint**: `POST /api/seguranca/validate-token`
3. **Body**:
   ```json
   {
     "token": "SEU_TOKEN_AQUI"
   }
   ```
4. **Resultado esperado**: Status 200 com dados do usuário

## 🧪 Parte 3: Testes de Erro

### Teste 1: Login com Credenciais Inválidas

```json
{
  "email": "admin@mauriciogym.com",
  "senha": "senhaerrada"
}
```
**Resultado esperado**: Status 401 - Credenciais inválidas

### Teste 2: Login com Email Inexistente

```json
{
  "email": "inexistente@teste.com",
  "senha": "admin123"
}
```
**Resultado esperado**: Status 401 - Usuário não encontrado

### Teste 3: Token Inválido

```json
{
  "token": "token_invalido_123"
}
```
**Resultado esperado**: Status 401 - Token inválido

## 🔍 Parte 4: Verificação de CORS

### Teste via Browser Console

1. **Abra**: http://localhost:8000/swagger
2. **Abra o Console do Navegador** (F12)
3. **Execute**:
   ```javascript
   fetch('http://localhost:5000/login', {
     method: 'POST',
     headers: {
       'Content-Type': 'application/json'
     },
     body: JSON.stringify({
       email: 'admin@mauriciogym.com',
       senha: 'admin123'
     })
   })
   .then(response => response.json())
   .then(data => console.log('Sucesso:', data))
   .catch(error => console.error('Erro:', error));
   ```
4. **Resultado esperado**: Sucesso sem erros de CORS

## 🚨 Solução de Problemas

### Erro: "Nome de objeto 'Autenticacao' inválido"

**Causa**: Tabela não foi criada
**Solução**:
1. Execute novamente `create-database-complete.sql`
2. Verifique com `verificar-banco.sql`
3. Reinicie as APIs

### Erro: CORS Policy

**Causa**: Configuração de CORS
**Solução**:
1. Verifique se as APIs estão rodando nas portas corretas
2. Reinicie as APIs
3. Teste via Postman primeiro

### Erro: Connection Timeout

**Causa**: SQL Server não está rodando
**Solução**:
1. Inicie o SQL Server
2. Verifique a string de conexão
3. Teste a conexão no SSMS

### APIs não respondem

**Solução**:
1. Verifique se as portas estão livres
2. Reinicie as APIs:
   ```bash
   # Terminal 1 - Gateway
   cd MauricioGym-dotnet-api/MauricioGym.Gateway.Api
   dotnet run
   
   # Terminal 2 - Segurança
   cd MauricioGym-dotnet-api/MauricioGym.Seguranca.Api
   dotnet run
   ```

## ✅ Checklist Final

- [ ] Banco MauricioGymDB criado
- [ ] 13 tabelas criadas
- [ ] Tabela Autenticacao com 11 colunas
- [ ] Usuário admin@mauriciogym.com existe
- [ ] Gateway rodando na porta 8000
- [ ] API Segurança rodando na porta 5000
- [ ] Login via Swagger Gateway funciona
- [ ] Login via Swagger Segurança funciona
- [ ] Login via Postman funciona
- [ ] Validação de token funciona
- [ ] Testes de erro retornam status corretos
- [ ] CORS configurado corretamente

## 🎉 Sucesso!

Se todos os itens do checklist estão marcados, o sistema está funcionando perfeitamente!

**Próximos passos**:
1. Integrar com o frontend Angular
2. Implementar outras funcionalidades
3. Adicionar mais usuários de teste