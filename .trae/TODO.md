# TODO:

- [x] check_database_exists: Verificar se o banco de dados MauricioGym foi criado e se o usuário admin existe (priority: High)
- [x] fix_password_validation: Implementar hash SHA256 no método ValidarLoginAsync do UsuarioService (priority: High)
- [x] test_correct_credentials: Testar login com email 'admin@mauriciogym.com' e senha 'admin123' (não 'Admin123') (priority: Medium)
- [x] verify_api_running: Verificar se a API de usuário está rodando na porta 5001 (priority: Medium)
- [x] test_jwt_generation: Confirmar que o token JWT está sendo gerado corretamente após login (priority: Low)
