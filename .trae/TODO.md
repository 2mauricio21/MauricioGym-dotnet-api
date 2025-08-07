# TODO:

- [x] analyze_swagger_config: Analisar a configuração atual do SwaggerAggregationService para entender como os endpoints estão sendo agregados (priority: High)
- [x] fix_swagger_server_config: Modificar o SwaggerAggregationService para configurar o servidor base como localhost:8000 (Gateway) em vez das APIs individuais (priority: High)
- [x] update_swagger_paths: Garantir que todos os paths no Swagger apontem para o Gateway e não para as APIs individuais (priority: High)
- [x] test_swagger_gateway: Testar o endpoint /api/Usuario/login através do Swagger para confirmar que está passando pelo Gateway (priority: Medium)
- [x] verify_all_endpoints: Verificar se todos os outros endpoints também estão funcionando corretamente através do Gateway (priority: Low)
