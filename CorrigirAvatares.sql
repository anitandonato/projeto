-- Script para corrigir avatares baseado nos pontos dos alunos
-- Execute no banco de dados: CodeSchool.API/codeschool.db

-- Atualizar avatares baseado na pontuação
UPDATE Usuarios
SET AvatarId = CASE
    WHEN (
        SELECT COALESCE(SUM(d.Pontos), 0)
        FROM Progressos p
        INNER JOIN Desafios d ON p.DesafioId = d.Id
        WHERE p.AlunoId = Usuarios.Id AND p.Concluido = 1
    ) < 50 THEN 1  -- Nível 1: < 50 pts → Avatar 🦸
    WHEN (
        SELECT COALESCE(SUM(d.Pontos), 0)
        FROM Progressos p
        INNER JOIN Desafios d ON p.DesafioId = d.Id
        WHERE p.AlunoId = Usuarios.Id AND p.Concluido = 1
    ) < 150 THEN 2  -- Nível 2: 50-149 pts → Avatar 🧙
    WHEN (
        SELECT COALESCE(SUM(d.Pontos), 0)
        FROM Progressos p
        INNER JOIN Desafios d ON p.DesafioId = d.Id
        WHERE p.AlunoId = Usuarios.Id AND p.Concluido = 1
    ) < 300 THEN 3  -- Nível 3: 150-299 pts → Avatar 🥷
    WHEN (
        SELECT COALESCE(SUM(d.Pontos), 0)
        FROM Progressos p
        INNER JOIN Desafios d ON p.DesafioId = d.Id
        WHERE p.AlunoId = Usuarios.Id AND p.Concluido = 1
    ) < 500 THEN 4  -- Nível 4: 300-499 pts → Avatar 🤖
    ELSE 5  -- Nível 5: 500+ pts → Avatar 👾
END
WHERE Tipo = 0;  -- 0 = Aluno

-- Verificar resultado
SELECT
    Nome,
    Email,
    AvatarId,
    (SELECT COALESCE(SUM(d.Pontos), 0)
     FROM Progressos p
     INNER JOIN Desafios d ON p.DesafioId = d.Id
     WHERE p.AlunoId = Usuarios.Id AND p.Concluido = 1) AS Pontos
FROM Usuarios
WHERE Tipo = 0
ORDER BY Pontos DESC;
