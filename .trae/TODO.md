# TODO:

- [x] disable_yarp_test: Desabilitar temporariamente o YARP para testar se o Swagger UI funciona sem interferência do proxy (priority: High)
- [x] reenable_yarp: Reabilitar YARP no Gateway descomentando app.MapReverseProxy() e os serviços do YARP (priority: High)
- [x] start_usuario_api: Iniciar API de usuário na porta 5001 para que o proxy funcione (priority: High)
- [x] configure_swagger_standalone: Configurar Swagger UI para funcionar independentemente do status das APIs backend (priority: Medium)
- [x] add_health_checks: Adicionar verificação de saúde das APIs backend antes de fazer proxy (priority: Medium)
- [x] implement_fallback: Implementar fallback quando APIs não estão disponíveis (priority: Medium)
- [x] test_login_endpoint: Testar endpoint /api/Usuario/login no Swagger UI em navegadores externos (priority: Medium)
- [x] test_isolated_swagger: Testar Swagger UI isolado em navegadores externos (priority: Low)
