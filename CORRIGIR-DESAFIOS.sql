-- SCRIPT PARA CORRIGIR DESCRIÇÕES DOS DESAFIOS
-- Execute este script para atualizar as descrições dos desafios

-- Desafio 1: Primeiros Passos
UPDATE Desafios SET
    Descricao = 'Mova o robô 3 passos para frente até alcançar o objetivo. Use apenas o bloco MOVER.',
    Objetivo = 'Alcançar a posição [3,0]'
WHERE Id = 1;

-- Desafio 2: Virando à Direita
UPDATE Desafios SET
    Descricao = 'Faça o robô andar 2 passos para frente, virar à direita e andar mais 2 passos até o objetivo.',
    Objetivo = 'Alcançar a posição [2,2]'
WHERE Id = 2;

-- Desafio 3: Usando Loops
UPDATE Desafios SET
    Descricao = 'Use o bloco REPETIR para fazer o robô andar 5 passos sem repetir o bloco MOVER manualmente.',
    Objetivo = 'Alcançar a posição [4,0] usando loops'
WHERE Id = 3;

-- Desafio 4: Quadrado Perfeito
UPDATE Desafios SET
    Descricao = 'Faça o robô andar em forma de quadrado (1 passo para cada lado) e voltar à posição inicial. Use LOOPS!',
    Objetivo = 'Voltar para a posição inicial [1,1]'
WHERE Id = 4;

-- Desafio 5: Corredor em L
UPDATE Desafios SET
    Descricao = 'Navegue pelo corredor em formato de L. Ande 4 passos para frente, vire à direita e ande mais 2 passos para baixo.',
    Objetivo = 'Alcançar a posição [4,0]'
WHERE Id = 5;

-- Desafio 6: Escadaria
UPDATE Desafios SET
    Descricao = 'Suba a escada diagonal fazendo um movimento em zigue-zague. Padrão: mover, virar esquerda, mover, virar direita.',
    Objetivo = 'Alcançar a posição [4,0]'
WHERE Id = 6;

-- Desafio 7: Zigue-Zague
UPDATE Desafios SET
    Descricao = 'Percorra o grid em zigue-zague da posição [0,0] até [4,4]. Planeje bem seus movimentos e viradas!',
    Objetivo = 'Alcançar a posição [4,4]'
WHERE Id = 7;

-- Desafio 8: Explorador
UPDATE Desafios SET
    Descricao = 'Explore o mapa grande (6x6) indo da posição inicial [0,0] até o canto oposto [5,5]. Planeje a rota mais eficiente!',
    Objetivo = 'Alcançar a posição [5,5]'
WHERE Id = 8;

-- Desafio 9: Espiral
UPDATE Desafios SET
    Descricao = 'Crie um movimento em espiral saindo do centro [3,3] até a borda do grid [6,0]. Desafio avançado com loops complexos!',
    Objetivo = 'Alcançar a posição [6,0]'
WHERE Id = 9;

-- Desafio 10: Desafio Final
UPDATE Desafios SET
    Descricao = 'O GRANDE DESAFIO FINAL! Percorra o grid 7x7 do canto superior esquerdo [0,6] até o canto inferior direito [6,0]. Use TUDO que aprendeu: loops, viradas estratégicas e sequências complexas!',
    Objetivo = 'Alcançar a posição [6,0]'
WHERE Id = 10;

-- Verificar resultados
SELECT Id, Titulo, Descricao, Objetivo FROM Desafios ORDER BY Ordem;
