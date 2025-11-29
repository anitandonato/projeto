# GUIA DE ORGANIZAÇÃO DO TCC NO GOOGLE DOCS

## 📋 PASSO A PASSO COMPLETO

### PASSO 1: Criar o Documento no Google Docs

1. Acesse: https://docs.google.com
2. Clique em "Documento em branco"
3. Renomeie para: **"TCC - CodeSchool - [Seu Nome]"**

---

### PASSO 2: Configurar Formatação Básica

**Margens e Fonte:**
- Arquivo → Configurar página
  - Margens: Superior/Inferior 3cm, Esquerda 3cm, Direita 2cm
  - Tamanho: A4
- Fonte padrão: Arial ou Times New Roman, tamanho 12
- Espaçamento entre linhas: 1,5

**Estilos de Título:**
- Título 1 (H1): Seções principais (ex: "1. DOCUMENTAÇÃO GERAL")
- Título 2 (H2): Subseções (ex: "1.1 Descrição da Situação-Problema")
- Título 3 (H3): Sub-subseções (ex: "1.1.1 Contexto")

---

### PASSO 3: Estrutura do Documento (Ordem)

**CAPA (1ª página)**
```
[LOGO DA INSTITUIÇÃO]

CURSO DE DESENVOLVIMENTO DE SISTEMAS

[NOMES DOS ALUNOS]

CODESCHOOL: PLATAFORMA GAMIFICADA PARA ENSINO
DE PENSAMENTO COMPUTACIONAL

[CIDADE - ESTADO]
2025
```

**FOLHA DE ROSTO (2ª página)**
```
[NOMES DOS ALUNOS]

CODESCHOOL: PLATAFORMA GAMIFICADA PARA ENSINO
DE PENSAMENTO COMPUTACIONAL

Trabalho de Conclusão de Curso apresentado ao
Curso de Desenvolvimento de Sistemas da
[Nome da Instituição] como requisito parcial
para obtenção do título de Técnico em
Desenvolvimento de Sistemas.

Orientador: [Nome do Professor]

[CIDADE - ESTADO]
2025
```

**SUMÁRIO (3ª página)**
- Inserir → Índice → Com números de página
- Atualizar automaticamente quando adicionar seções

---

### PASSO 4: Copiar Conteúdo do TCC-COMPLETO-CONSOLIDADO.md

**Como Copiar (Mantenha a Formatação):**

1. Abra o arquivo `TCC-COMPLETO-CONSOLIDADO.md` no VS Code ou Notepad
2. Copie TODO o conteúdo (Ctrl+A, Ctrl+C)
3. No Google Docs, cole (Ctrl+V)
4. **IMPORTANTE:** O Markdown não será formatado automaticamente

**Como Formatar no Google Docs:**

- Linhas que começam com `# ` → Título 1 (ex: `# 1. DOCUMENTAÇÃO GERAL` vira título)
- Linhas que começam com `## ` → Título 2
- Linhas que começam com `### ` → Título 3
- Negrito: `**texto**` vira **texto** (selecione e Ctrl+B)
- Listas: Linhas com `- ` ou `1. ` vira lista (selecione e clique em "Bullets" ou "Números")

**DICA RÁPIDA:**
- Use Ctrl+H (Localizar e substituir) para trocar Markdown por formatação:
  - Localizar: `**` → Substituir por nada (depois aplique negrito manualmente nas palavras)
  - Localizar: `# ` → Substituir por nada (depois aplique Título 1)

---

### PASSO 5: Adicionar Diagramas e Imagens

**OPÇÃO 1: Diagramas como Imagens (Recomendado)**

1. **Converter Diagramas ASCII para Imagens:**
   - Use https://asciiflow.com/ ou https://textik.com/
   - Copie os diagramas do .md, cole no site
   - Export como PNG
   - Inserir → Imagem → Upload

2. **Criar Diagramas Profissionais:**
   - Use https://draw.io (grátis)
   - Crie Diagrama de Casos de Uso, Classes, Sequência, DER
   - Export como PNG (alta resolução, 300dpi)
   - Inserir no Google Docs

**OPÇÃO 2: Usar Google Drawings (Integrado)**

1. Inserir → Desenho → Novo
2. Crie diagramas simples (caixas, setas, textos)
3. Salvar e fechar (fica editável no Docs)

**Legendas:**
- Após inserir imagem: Clique com botão direito → "Adicionar legenda"
- Exemplo: **Figura 1:** Diagrama de Casos de Uso do Sistema CodeSchool

---

### PASSO 6: Adicionar Tabelas

**Como Criar Tabelas:**
1. Inserir → Tabela → Selecione número de linhas e colunas
2. Exemplo: Tabela de Requisitos Funcionais (3 colunas: ID, Descrição, Prioridade)

**Formatação de Tabelas:**
- Primeira linha: Negrito (cabeçalho)
- Bordas: Clique na tabela → Formato de tabela → Largura da borda: 1pt
- Alinhamento: Centro para cabeçalhos, esquerda para conteúdo

**Legendas:**
- Acima da tabela: **Tabela 1:** Requisitos Funcionais do Sistema

---

### PASSO 7: Numeração de Páginas

1. Inserir → Números de página → Parte inferior (rodapé)
2. **IMPORTANTE:** Capa e Folha de Rosto não têm número
   - Para isso: Inserir → Quebra de seção após a Folha de Rosto
   - Desmarque "Vincular ao anterior" no rodapé da 3ª página
   - Inicie a numeração a partir do Sumário (página 1)

---

### PASSO 8: Referências Bibliográficas (ABNT)

**Configurar Citações (Plugin):**
1. Extensões → Complementos → Instalar complementos
2. Busque "Paperpile" ou "Zotero" (gratuitos para estudantes)
3. Adicione referências automaticamente

**OU Manualmente (Copie do .md):**
- As 25 referências já estão formatadas no arquivo consolidado
- Copie a seção "6.3 Referências Bibliográficas"
- Cole no final do Google Docs

**Formato ABNT (Exemplo):**
```
WING, Jeannette M. Computational thinking. Communications of the ACM,
v. 49, n. 3, p. 33-35, 2006.
```

---

### PASSO 9: Revisar e Finalizar

**Checklist Final:**
- [ ] Capa e Folha de Rosto preenchidas
- [ ] Sumário atualizado (clique com botão direito → Atualizar índice)
- [ ] Todas as imagens têm legendas (Figura 1, Figura 2...)
- [ ] Todas as tabelas têm legendas (Tabela 1, Tabela 2...)
- [ ] Numeração de páginas correta (começa no Sumário)
- [ ] Referências no formato ABNT
- [ ] Ortografia verificada (Ferramentas → Ortografia e gramática)
- [ ] Formatação consistente (fontes, tamanhos, espaçamentos)

**Revisar Conteúdo:**
- Leia cada seção e verifique se faz sentido
- Certifique-se de que diagramas correspondem ao texto
- Valide se todas as histórias de usuário estão alinhadas aos requisitos

---

## 🎨 GUIA FIGMA (PROTÓTIPO)

### O QUE É O FIGMA?

Figma é uma ferramenta de design de interfaces (UI/UX) online e gratuita. Você vai criar protótipos navegáveis das telas do CodeSchool.

**Link:** https://www.figma.com/

---

### PASSO A PASSO FIGMA

#### PASSO 1: Criar Conta e Projeto

1. Acesse https://www.figma.com/
2. Clique em "Sign up" (pode usar conta Google)
3. Crie um novo projeto: "CodeSchool TCC"
4. Dentro do projeto, crie um arquivo: "Protótipo CodeSchool"

---

#### PASSO 2: Configurar Frames (Telas)

1. Clique em **Frame** (F) no menu lateral
2. Selecione "Desktop" → **1440 x 900** (tamanho padrão web)
3. Crie 5 frames (uma para cada tela):
   - Frame 1: Login/Cadastro
   - Frame 2: Dashboard Aluno
   - Frame 3: Tela de Desafio
   - Frame 4: Dashboard Professor
   - Frame 5: Relatório da Turma

**DICA:** Renomeie os frames (clique com botão direito → Rename)

---

#### PASSO 3: Design System (Estilos)

**Cores (Criar Estilos):**
1. Desenhe um retângulo (R)
2. Preencha com #667eea (roxo primary)
3. Clique no ícone de 4 pontos no seletor de cor → "Create style" → Nomeie "Primary Purple"
4. Repita para:
   - Branco (#FFFFFF) - "Background"
   - Cinza (#F5F7FA) - "Card Background"
   - Verde (#4CAF50) - "Success"
   - Vermelho (#FF6B6B) - "Error"

**Textos (Criar Estilos):**
1. Clique em **Text** (T)
2. Escreva "Título Grande"
3. Formate: Segoe UI, Bold, 40px
4. Clique no ícone de 4 pontos no painel Text → "Create style" → Nomeie "Heading 1"
5. Repita para:
   - Heading 2: Segoe UI, Semibold, 32px
   - Heading 3: Segoe UI, Semibold, 24px
   - Body: Segoe UI, Regular, 16px

---

#### PASSO 4: Criar Componentes Reutilizáveis

**Componente: Botão Primary**
1. Desenhe um retângulo: 140px largura x 48px altura
2. Borda arredondada: 12px (Rounded corners)
3. Cor de preenchimento: Primary Purple (#667eea)
4. Adicione texto "Entrar" (T): Branco, Segoe UI Semibold, 16px, centralizado
5. Selecione retângulo + texto → Ctrl+Alt+K (criar componente)
6. Renomeie: "Button/Primary"

**Componente: Card de Desafio**
1. Desenhe retângulo: 250px x 300px
2. Cor: Branco (#FFFFFF)
3. Sombra: 0px 5px 20px rgba(0,0,0,0.1)
   - Selecione retângulo → Effects → + → Drop Shadow
   - X: 0, Y: 5, Blur: 20, Spread: 0, Color: #000000 com 10% opacity
4. Borda arredondada: 12px
5. Adicione:
   - Ícone (✅, 🔓 ou 🔒) - Use emojis ou ícones do plugin "Iconify"
   - Título: "Desafio 1"
   - Descrição: "Primeiros Passos"
   - Badge de dificuldade: "Fácil" (retângulo pequeno verde)
   - Pontos: "100 pts"
6. Selecione tudo → Ctrl+Alt+K → Renomeie: "Card/Desafio"

**DICA:** Crie variantes do card (completo, disponível, bloqueado)
- Clique com botão direito no componente → "Add variant"
- Altere cores/opacidade para cada estado

---

#### PASSO 5: Montar as Telas

**Tela 1: Login/Cadastro**
1. **Frame 1 (1440x900)**
2. Adicione:
   - Background gradiente (Primary Purple → #764ba2)
   - Card branco centralizado (400px x 500px)
   - Logo "🎓 CodeSchool" (texto grande, centralizado)
   - 2 inputs: "Email" e "Senha" (retângulos brancos com borda cinza)
   - Botão "Entrar" (use o componente criado)
   - Link "Não tem conta? Cadastre-se" (texto pequeno azul)

**Tela 2: Dashboard Aluno**
1. **Frame 2 (1440x900)**
2. Adicione:
   - Header: Barra roxa no topo (altura 70px) com logo, menu e botão "Sair"
   - Card de Progresso: Retângulo branco com:
     - Texto "Olá, João! 👋"
     - Barra de progresso (retângulo preenchido 70%)
     - "⭐⭐⭐ 7 desafios completados"
   - Seção "Minhas Turmas": Card com lista de 2 turmas
   - Seção "Ranking": Card com top 5 (use emoji 🥇🥈🥉 para top 3)
   - Grid de Desafios: 10 cards de desafio (use componentes criados)
     - 3 completos (✅ verde)
     - 4 disponíveis (🔓 azul)
     - 3 bloqueados (🔒 cinza, opacidade 50%)

**Tela 3: Tela de Desafio**
1. **Frame 3 (1440x900)**
2. Divida em 2 colunas (50/50):
   - **Coluna Esquerda:** Grid 5x5 do desafio
     - Desenhe 25 quadrados (60px x 60px cada, gap 5px)
     - Pinte alguns de cinza escuro (paredes 🧱)
     - Adicione emoji 🤖 em uma célula (robô)
     - Adicione emoji 🎯 em outra célula (objetivo)
   - **Coluna Direita:** Workspace Blockly (mockup)
     - Retângulo branco com borda
     - Título "Blocos Disponíveis"
     - 5 retângulos coloridos simulando blocos:
       - Verde: "➡️ Mover"
       - Azul: "↪️ Virar Direita"
       - Laranja: "🔁 Repetir 3 vezes"
       - Roxo: "🔍 Se... então"
       - Amarelo: "📦 Variável"
   - Botões: "▶️ Executar", "🔄 Resetar", "💾 Salvar"

**Tela 4: Dashboard Professor**
1. **Frame 4 (1440x900)**
2. Adicione:
   - Header similar ao do aluno (com texto "Painel do Professor")
   - Botões grandes: "➕ Criar Nova Turma" e "📚 Recursos Pedagógicos"
   - 3 Cards de Turmas:
     - Card 1: "📘 6º Ano A" - 25 alunos - Progresso 65% - ⚠️ 3 com dificuldades
     - Card 2: "📗 7º Ano B" - 28 alunos - Progresso 82% - ✅ Indo bem
     - Card 3: "📙 8º Ano C" - 22 alunos - Progresso 45% - ⚠️ 7 com dificuldades
   - Botões em cada card: "Ver Detalhes", "Relatório", "Compartilhar Código"

**Tela 5: Relatório da Turma**
1. **Frame 5 (1440x900)**
2. Adicione:
   - Header com "← Voltar" e "Relatório: 6º Ano A"
   - Card "Estatísticas Gerais":
     - 👥 25 alunos
     - ⭐ Média 680 pts
     - 📈 Taxa 65%
   - Gráfico de Barras (mockup):
     - Desenhe 10 barras horizontais de tamanhos variados
     - Desafio 1: barra quase completa (24/25)
     - Desafio 2: barra 90% (23/25)
     - ...
     - Desafio 10: barra vazia (0/25)
   - Gráfico de Histograma (mockup):
     - Desenhe barras verticais simulando distribuição (0-300pts, 300-600pts, etc.)
   - Lista "Alunos com Dificuldades":
     - 3 linhas com nome, progresso, botão "Ver Detalhes"

---

#### PASSO 6: Criar Interatividade (Protótipo Navegável)

1. Clique no botão **Prototype** (seta no canto superior direito)
2. **Conectar Telas:**
   - Tela Login → Botão "Entrar" → Arraste para Tela Dashboard Aluno
     - Interaction: On Click → Navigate to → Dashboard Aluno
     - Animation: Instant (ou Dissolve para fade)
   - Tela Dashboard Aluno → Card Desafio → Arraste para Tela Desafio
   - Tela Desafio → Botão "Voltar" → Arraste para Dashboard Aluno
   - Tela Dashboard Professor → Botão "Relatório" → Arraste para Relatório da Turma
   - Tela Relatório → Botão "Voltar" → Arraste para Dashboard Professor

3. **Testar Protótipo:**
   - Clique em ▶️ (Play, canto superior direito)
   - Teste se a navegação funciona
   - Ajuste se necessário

---

#### PASSO 7: Compartilhar e Exportar

**Compartilhar com Professor (Link):**
1. Clique em "Share" (canto superior direito)
2. Em "Get link", selecione "Anyone with the link" → "Can view"
3. Copie o link e envie ao orientador

**Exportar para PDF (para o TCC):**
1. Selecione todos os 5 frames (Shift+Click)
2. File → Export → PDF
3. Salve como "Prototipo-CodeSchool.pdf"
4. **No Google Docs:** Inserir → Imagem → Upload → Selecione o PDF
   - Cada frame vira uma imagem
   - Adicione legendas: "Figura X: Protótipo - Tela de Login"

**Exportar Frames Individuais (PNG):**
1. Selecione Frame 1 (Login)
2. Clique em "Export" (painel direito)
3. Formato: PNG, escala: 2x (alta resolução)
4. Download
5. Repita para os 5 frames
6. Insira no Google Docs com legendas apropriadas

---

## 📊 OPÇÃO: PRD (Product Requirements Document) vs. TCC

**O que é PRD?**
- Documento técnico usado em empresas de tecnologia
- Foco em requisitos de produto, user stories, especificações técnicas
- Formato: Markdown ou Confluence, direto ao ponto

**PRD seria bom para o TCC?**
- ❌ **NÃO recomendado** para TCC acadêmico
- TCCs exigem formatação ABNT, introdução teórica, revisão de literatura
- PRD é muito técnico e não cobre aspectos pedagógicos/justificativa

**Mas você pode usar PRD internamente:**
- Criar um PRD para guiar o desenvolvimento (já feito no nosso .md)
- Usar como base para escrever o TCC formal no Google Docs
- PRD fica como anexo (opcional)

---

## 🎯 RESUMO: O QUE FAZER

### 1. GOOGLE DOCS (TCC Formal)
- [ ] Copiar conteúdo de `TCC-COMPLETO-CONSOLIDADO.md`
- [ ] Formatar títulos, listas, tabelas
- [ ] Adicionar capa, folha de rosto, sumário
- [ ] Inserir diagramas (Draw.io ou ASCII convertido)
- [ ] Adicionar numeração de páginas
- [ ] Revisar ortografia e ABNT
- [ ] Exportar como PDF final

### 2. FIGMA (Protótipo)
- [ ] Criar 5 frames (telas principais)
- [ ] Montar Design System (cores, textos, componentes)
- [ ] Desenhar interfaces conforme wireframes do TCC
- [ ] Adicionar interatividade (navegação entre telas)
- [ ] Testar protótipo
- [ ] Exportar como PDF/PNG para incluir no TCC
- [ ] Compartilhar link com professor

### 3. APRESENTAÇÃO (PowerPoint/Slides)
- [ ] Criar slides de apresentação (15-20 slides)
- [ ] Estrutura sugerida:
   1. Capa
   2. Problema
   3. Objetivos
   4. Metodologia
   5. Arquitetura do Sistema
   6. Tecnologias
   7. Requisitos (resumo)
   8. Diagramas UML
   9. Protótipo Figma (prints)
   10. Demo ao vivo (se possível)
   11. Resultados Esperados
   12. Limitações e Melhorias Futuras
   13. Conclusão
   14. Perguntas

---

## 🚀 CRONOGRAMA SUGERIDO (PRÓXIMOS PASSOS)

### Semana 1: Documentação
- **Dia 1-2:** Copiar e formatar TCC no Google Docs
- **Dia 3-4:** Criar diagramas no Draw.io e inserir no Docs
- **Dia 5:** Revisar e pedir feedback do orientador

### Semana 2: Protótipo
- **Dia 1-2:** Configurar Figma, criar Design System
- **Dia 3-4:** Montar as 5 telas principais
- **Dia 5:** Adicionar interatividade e exportar

### Semana 3: Apresentação e Ensaio
- **Dia 1-2:** Criar slides da apresentação
- **Dia 3-4:** Ensaiar apresentação (cronometrar 15-20 min)
- **Dia 5:** Ajustes finais, preparar demo

### Semana 4: Entrega
- **Dia 1:** Exportar TCC como PDF final
- **Dia 2:** Imprimir e encadernar (se necessário)
- **Dia 3:** Entregar documentação
- **Dia 4-5:** Apresentação oficial do TCC

---

## 💡 DICAS EXTRAS

### Revisão Final
- Peça para 2-3 colegas lerem o TCC e darem feedback
- Use o Grammarly (extensão Chrome) para revisar inglês técnico
- Valide se todos os diagramas têm legendas
- Certifique-se de que referências estão completas

### Apresentação
- Prepare-se para perguntas: "Por que Vue.js?", "Como garante segurança?", "Qual a diferença do Scratch?"
- Tenha respostas prontas sobre limitações e melhorias futuras
- Se possível, faça demo ao vivo (login → resolver desafio → ver ranking)
- Tempo: 15-20 minutos + 5-10 min de perguntas

### Postura na Apresentação
- Vista-se formalmente (camisa social, calça/saia formal)
- Fale olhando para a banca, não para os slides
- Use ponteiro laser (ou mouse) para destacar diagramas
- Seja honesto sobre limitações: "Não implementamos X por limitação de tempo, mas está planejado"

---

**BOA SORTE! 🎓🚀**

Se tiver dúvidas específicas sobre Figma, Google Docs ou qualquer parte da documentação, me pergunte!
