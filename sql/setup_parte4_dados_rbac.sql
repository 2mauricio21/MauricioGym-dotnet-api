-- Script parte 4 - Inserir dados iniciais RBAC
USE MauricioGymDB;
GO

-- Inserir Papéis do Sistema
INSERT INTO Papel (Nome, Descricao, EhSistema) VALUES
('SuperAdmin', 'Administrador com acesso total ao sistema', 1),
('AdminSistema', 'Administrador de sistema para gerenciar academias', 1),
('AdminAcademia', 'Administrador de academia específica', 1),
('Suporte', 'Usuário de suporte com acesso limitado', 1);

-- Inserir Permissões
INSERT INTO Permissao (Nome, Descricao, Recurso, Acao) VALUES
-- Administrador
('Admin.GerenciarAdministradoresSistema', 'Gerenciar administradores do sistema', 'Administrador', 'Gerenciar'),
('Admin.GerenciarPapeis', 'Gerenciar papéis e permissões', 'Papel', 'Gerenciar'),

-- Academia
('Academia.Criar', 'Criar novas academias', 'Academia', 'Criar'),
('Academia.Ler', 'Visualizar dados das academias', 'Academia', 'Ler'),
('Academia.Atualizar', 'Atualizar dados das academias', 'Academia', 'Atualizar'),
('Academia.Excluir', 'Excluir academias', 'Academia', 'Excluir'),

-- Cliente
('Cliente.Gerenciar', 'Gerenciar clientes da academia', 'Cliente', 'Gerenciar'),
('Cliente.Ler', 'Visualizar dados dos clientes', 'Cliente', 'Ler'),

-- Plano
('Plano.Gerenciar', 'Gerenciar planos da academia', 'Plano', 'Gerenciar'),

-- Pagamento
('Pagamento.Processar', 'Processar pagamentos', 'Pagamento', 'Processar'),
('Pagamento.Ler', 'Visualizar pagamentos', 'Pagamento', 'Ler'),

-- CheckIn
('CheckIn.Processar', 'Processar check-ins', 'CheckIn', 'Processar'),

-- Financeiro
('Financeiro.VisualizarRelatoriosSistema', 'Visualizar relatórios financeiros do sistema', 'Financeiro', 'Ler'),
('Financeiro.VisualizarRelatoriosAcademia', 'Visualizar relatórios financeiros da academia', 'Financeiro', 'Ler');

-- Associar Permissões aos Papéis
DECLARE @SuperAdminId INT = (SELECT Id FROM Papel WHERE Nome = 'SuperAdmin');
DECLARE @AdminSistemaId INT = (SELECT Id FROM Papel WHERE Nome = 'AdminSistema');
DECLARE @AdminAcademiaId INT = (SELECT Id FROM Papel WHERE Nome = 'AdminAcademia');
DECLARE @SuporteId INT = (SELECT Id FROM Papel WHERE Nome = 'Suporte');

-- SuperAdmin - Todas as permissões
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @SuperAdminId, Id FROM Permissao;

-- AdminSistema - Gerenciar academias e ver relatórios
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @AdminSistemaId, Id FROM Permissao 
WHERE Nome IN ('Academia.Criar', 'Academia.Ler', 'Academia.Atualizar', 'Academia.Excluir', 'Financeiro.VisualizarRelatoriosSistema');

-- AdminAcademia - Gerenciar sua academia
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @AdminAcademiaId, Id FROM Permissao 
WHERE Nome IN ('Cliente.Gerenciar', 'Plano.Gerenciar', 'Pagamento.Processar', 'CheckIn.Processar', 'Financeiro.VisualizarRelatoriosAcademia');

-- Suporte - Apenas leitura
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @SuporteId, Id FROM Permissao 
WHERE Nome IN ('Academia.Ler', 'Cliente.Ler', 'Pagamento.Ler');

-- Atualizar administrador existente para SuperAdmin
UPDATE Administrador 
SET EhSuperAdmin = 1, IdAcademia = NULL 
WHERE Id = 1;

-- Associar papel SuperAdmin ao primeiro administrador
IF NOT EXISTS (SELECT 1 FROM AdministradorPapel WHERE IdAdministrador = 1 AND IdPapel = @SuperAdminId)
INSERT INTO AdministradorPapel (IdAdministrador, IdPapel) VALUES (1, @SuperAdminId);

-- Inserir algumas modalidades padrão para a academia principal
INSERT INTO Modalidade (Nome, Descricao, IdAcademia) VALUES
('Academia', 'Treino de musculação e fitness', 1),
('Yoga', 'Aulas de yoga e meditação', 1),
('Crossfit', 'Treinamento funcional de alta intensidade', 1),
('Natação', 'Aulas de natação e aqua fitness', 1);

PRINT 'Dados RBAC e modalidades inseridos com sucesso!';
