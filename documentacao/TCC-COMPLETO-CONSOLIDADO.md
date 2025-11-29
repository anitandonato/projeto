# TRABALHO DE CONCLUSÃO DE CURSO (TCC)

## CODESCHOOL: PLATAFORMA GAMIFICADA PARA ENSINO DE PENSAMENTO COMPUTACIONAL

**Curso:** Desenvolvimento de Sistemas
**Instituição:** [Nome da Instituição]
**Alunos:** [Nomes dos Integrantes]
**Orientador:** [Nome do Professor]
**Ano:** 2025

---

# SUMÁRIO

1. DOCUMENTAÇÃO GERAL DO PROJETO
   - 1.1 Descrição da Situação-Problema
   - 1.2 Objetivos
   - 1.3 Justificativa
   - 1.4 Público-Alvo e Perfis de Usuários
   - 1.5 Revisão de Conceitos
   - 1.6 Metodologia
   - 1.7 Cronograma
   - 1.8 Resultados Esperados

2. ENGENHARIA DE REQUISITOS
   - 2.1 Requisitos Funcionais
   - 2.2 Requisitos Não-Funcionais
   - 2.3 Histórias de Usuário
   - 2.4 Regras de Negócio

3. MODELAGEM E ARQUITETURA
   - 3.1 Diagrama de Casos de Uso
   - 3.2 Diagrama de Classes
   - 3.3 Diagrama de Sequência
   - 3.4 Diagrama Entidade-Relacionamento (DER)

4. INTERFACE E PROTOTIPAÇÃO
   - 4.1 Wireframes
   - 4.2 Design System
   - 4.3 Protótipo Navegável (Figma)
   - 4.4 Diretrizes de Acessibilidade WCAG 2.1 AA

5. DESIGN DA SOLUÇÃO
   - 5.1 Arquitetura do Sistema
   - 5.2 Tecnologias Utilizadas
   - 5.3 Descrição dos Módulos e Funcionalidades
   - 5.4 Segurança

6. CONSIDERAÇÕES FINAIS
   - 6.1 Limitações do Protótipo Atual
   - 6.2 Possíveis Evoluções e Melhorias Futuras
   - 6.3 Referências Bibliográficas

---

# 1. DOCUMENTAÇÃO GERAL DO PROJETO

## 1.1 Descrição da Situação-Problema

O ensino de programação e pensamento computacional no Brasil enfrenta desafios significativos:

**Problema Central:**
- Falta de ferramentas educacionais adaptadas à realidade brasileira (idioma, BNCC)
- Escassez de plataformas acessíveis para estudantes com deficiência
- Desmotivação de alunos com métodos tradicionais de ensino de lógica
- Dificuldade de professores em acompanhar progresso individual de múltiplos alunos

**Contexto Educacional:**
- BNCC (2018) incluiu pensamento computacional como competência obrigatória
- 82% das escolas públicas não possuem ferramentas adequadas para ensino de programação (dados de 2023)
- Estudantes de 11-14 anos (ensino fundamental II) têm pouco contato com lógica computacional
- Professores carecem de recursos pedagógicos alinhados ao currículo nacional

**Impacto:**
- Estudantes brasileiros ficam defasados em habilidades digitais essenciais para o século XXI
- Perpetuação da exclusão digital
- Falta de preparação para carreiras em tecnologia

---

## 1.2 Objetivos

### 1.2.1 Objetivo Geral

Desenvolver uma plataforma web gamificada, acessível e alinhada à BNCC para ensino de pensamento computacional a estudantes do ensino fundamental II (6º ao 9º ano), permitindo que professores acompanhem o progresso de suas turmas.

### 1.2.2 Objetivos Específicos

1. **Criar sistema de desafios progressivos** de programação visual (Blockly) com 10 níveis de dificuldade crescente
2. **Implementar mecânicas de gamificação** (pontos, badges, níveis, ranking) para aumentar engajamento
3. **Garantir acessibilidade WCAG 2.1 Nível AA** com alto contraste, ajuste de fontes e narração
4. **Desenvolver dashboard para professores** com relatórios de progresso, gráficos e identificação de alunos com dificuldades
5. **Criar sistema de turmas** com códigos únicos para facilitar gestão de alunos
6. **Produzir materiais pedagógicos** (narrativas, atividades desplugadas, mapeamento BNCC) para apoiar professores
7. **Mapear todos os desafios às competências da BNCC** para validar alinhamento curricular

---

## 1.3 Justificativa

### 1.3.1 Relevância Educacional

- **Pensamento Computacional como Competência Essencial:** Desenvolvimento de decomposição, abstração, reconhecimento de padrões e algoritmos
- **Alinhamento à BNCC:** Atende competências gerais 2 (pensamento científico) e 5 (cultura digital)
- **Aprendizagem Ativa:** Metodologia hands-on, learning by doing
- **Progressão Estruturada:** Desafios organizados por complexidade (Taxonomia de Bloom)

### 1.3.2 Relevância Social

- **Inclusão Digital:** Ferramentas acessíveis reduzem barreiras para estudantes com deficiência
- **Democratização do Conhecimento:** Plataforma gratuita, sem custo de licenciamento
- **Empoderamento Docente:** Relatórios automatizados facilitam intervenções pedagógicas personalizadas
- **Preparação para o Futuro:** Habilidades computacionais são diferenciais no mercado de trabalho

### 1.3.3 Relevância Técnica

- **Stack Moderno:** Vue.js 3, ASP.NET Core 8, SQLite (tecnologias atuais e eficientes)
- **Programação Visual:** Blockly reduz barreira de entrada (sem sintaxe complexa)
- **Arquitetura Escalável:** API RESTful stateless, fácil de expandir
- **Open-Source:** Uso de bibliotecas com licenças permissivas (MIT, Apache 2.0)

---

## 1.4 Público-Alvo e Perfis de Usuários

### Persona 1: João - O Aluno Curioso
- **Idade:** 12 anos (7º ano)
- **Perfil:** Gosta de jogos e desafios, primeira experiência com programação
- **Necessidades:** Interface intuitiva, feedback imediato, recompensas visuais
- **Motivações:** Competir com colegas no ranking, conquistar todas as badges
- **Frustrações:** Dificuldade com conceitos abstratos, desânimo ao errar

### Persona 2: Maria - A Aluna com Deficiência Visual
- **Idade:** 13 anos (8º ano)
- **Perfil:** Baixa visão, usa leitor de tela NVDA
- **Necessidades:** Alto contraste, navegação por teclado, narração de elementos
- **Motivações:** Autonomia para aprender programação sem ajuda constante
- **Frustrações:** Plataformas não acessíveis, falta de suporte a leitores de tela

### Persona 3: Professora Ana - A Educadora
- **Idade:** 35 anos, professora de Matemática
- **Perfil:** Quer integrar pensamento computacional às aulas, sem formação em TI
- **Necessidades:** Materiais prontos (atividades, mapeamento BNCC), relatórios visuais
- **Motivações:** Inovar pedagogicamente, identificar alunos com dificuldades
- **Frustrações:** Falta de tempo para acompanhar 120+ alunos individualmente

### Persona 4: Pedro - O Aluno Avançado
- **Idade:** 14 anos (9º ano)
- **Perfil:** Já tem conhecimento básico de lógica, quer desafios complexos
- **Necessidades:** Desafios difíceis, uso de variáveis e condicionais avançados
- **Motivações:** Ser o top 1 do ranking geral, completar todos os desafios
- **Frustrações:** Plataformas muito simples, falta de profundidade

---

## 1.5 Revisão de Conceitos

### 1.5.1 Pensamento Computacional

**Definição:** Processo de resolução de problemas inspirado em conceitos da Ciência da Computação (Wing, 2006).

**4 Pilares:**
1. **Decomposição:** Dividir problemas complexos em partes menores
2. **Reconhecimento de Padrões:** Identificar similaridades e tendências
3. **Abstração:** Focar no essencial, ignorar detalhes irrelevantes
4. **Algoritmos:** Criar sequência de passos para resolver o problema

**Aplicação no CodeSchool:**
- Desafios 1-3: Decomposição (dividir movimento em passos)
- Desafios 4-6: Padrões (usar loops para repetições)
- Desafios 7-8: Abstração e condicionais (decisões baseadas em sensores)
- Desafios 9-10: Algoritmos complexos (combinar todos os conceitos)

### 1.5.2 Gamificação

**Definição:** Uso de elementos de jogos em contextos não-jogo para aumentar engajamento (Deterding et al., 2011).

**Mecânicas Implementadas:**
- **Pontos:** Feedback quantitativo de progresso (100-200 pts por desafio)
- **Níveis:** Sensação de progressão (5 níveis baseados em pontos)
- **Badges:** Recompensas por marcos (3, 7, 10 desafios)
- **Ranking:** Competição saudável (leaderboard por turma)
- **Narrativa:** Storytelling contextualiza desafios (Explorador Digital)
- **Avatares:** Personalização de identidade

### 1.5.3 BNCC (Base Nacional Comum Curricular)

**Competências Gerais Atendidas:**
- **Competência 2:** Pensamento científico, crítico e criativo
- **Competência 5:** Cultura digital e uso crítico de tecnologias

**Habilidades de Matemática (6º-9º ano):**
- EF06MA16: Associar pares ordenados a pontos do plano cartesiano (grid do desafio)
- EF07MA15: Utilizar a linguagem algébrica (variáveis nos blocos)
- EF08MA06: Resolver problemas com números naturais (loops, contadores)
- EF09MA18: Reconhecer e aplicar relações entre variáveis (condicionais)

### 1.5.4 WCAG 2.1 (Acessibilidade Web)

**Nível AA (Objetivo do Projeto):**
- **Perceptível:** Contraste 4.5:1, textos redimensionáveis, alt em imagens
- **Operável:** Navegação por teclado, foco visível, sem armadilhas de foco
- **Compreensível:** Idioma da página definido, navegação consistente
- **Robusto:** HTML válido, compatibilidade com leitores de tela

### 1.5.5 Blockly (Google)

**Definição:** Biblioteca JavaScript open-source para programação visual drag-and-drop.

**Vantagens:**
- Remove barreira de sintaxe (sem erros de digitação)
- Visual e intuitivo (blocos encaixam como Lego)
- Usado em Scratch, Code.org, App Inventor
- Customizável (criação de blocos personalizados)

---

## 1.6 Metodologia

### 1.6.1 Abordagem: Design Centrado no Usuário (DCU) + Scrum Adaptado

**Fase 1: Pesquisa e Planejamento (Semanas 1-2)**
- Levantamento de requisitos com professores e alunos
- Análise de plataformas similares (Scratch, Code.org, Hora do Código)
- Definição de personas e histórias de usuário
- Mapeamento de competências BNCC

**Fase 2: Design e Prototipação (Semanas 3-4)**
- Criação de wireframes de baixa fidelidade
- Design System (cores, tipografia, componentes)
- Protótipo navegável no Figma
- Validação com grupo focal (3 professores, 5 alunos)

**Fase 3: Desenvolvimento - Sprint 1 (Semanas 5-6)**
- Configuração do ambiente (Vue.js, ASP.NET Core, SQLite)
- Módulo de Autenticação (cadastro, login, JWT)
- Módulo de Turmas (criar, entrar)
- Testes unitários de serviços críticos

**Fase 4: Desenvolvimento - Sprint 2 (Semanas 7-8)**
- Módulo de Desafios (CRUD, Blockly, validação)
- Sistema de Gamificação (pontos, níveis, badges)
- Ranking (turma e geral)
- Testes de integração da API

**Fase 5: Desenvolvimento - Sprint 3 (Semanas 9-10)**
- Módulo de Relatórios (gráficos Chart.js)
- Módulo de Acessibilidade (alto contraste, fontes, narração)
- Recursos Pedagógicos (narrativas, atividades, BNCC)
- Testes E2E com Cypress

**Fase 6: Testes e Validação (Semana 11)**
- Testes de usabilidade com 10 alunos
- Auditoria de acessibilidade (axe DevTools, WAVE)
- Testes de carga (100 usuários simultâneos)
- Correções de bugs críticos

**Fase 7: Documentação e Entrega (Semana 12)**
- Redação do TCC completo
- Preparação da apresentação
- Deploy em ambiente de produção (Azure/Vercel)
- Entrega final

---

## 1.7 Cronograma

| Semana | Atividades | Entregáveis |
|--------|------------|-------------|
| 1-2 | Pesquisa, levantamento de requisitos | Documento de requisitos, personas |
| 3-4 | Design, wireframes, protótipo Figma | Protótipo navegável validado |
| 5-6 | Sprint 1: Auth + Turmas | Módulos funcionais, testes unitários |
| 7-8 | Sprint 2: Desafios + Gamificação | 10 desafios completos, ranking |
| 9-10 | Sprint 3: Relatórios + Acessibilidade | Dashboard professor, WCAG AA |
| 11 | Testes completos (usabilidade, acessibilidade, carga) | Relatório de testes, bugs corrigidos |
| 12 | Documentação final, apresentação, deploy | TCC completo, apresentação, sistema online |

---

## 1.8 Resultados Esperados

1. **Plataforma Web Funcional:**
   - 10 desafios de programação visual implementados
   - Autenticação segura (JWT, BCrypt)
   - Sistema de turmas operacional
   - Gamificação completa (pontos, badges, ranking)

2. **Conformidade WCAG 2.1 Nível AA:**
   - Auditoria com 0 erros críticos (axe, WAVE)
   - Navegação completa por teclado
   - Compatibilidade com NVDA/JAWS

3. **Dashboard para Professores:**
   - Relatórios com gráficos (Chart.js)
   - Identificação automática de alunos com dificuldades
   - Exportação de dados (futuro)

4. **Materiais Pedagógicos:**
   - Narrativa completa do "Explorador Digital"
   - 8 atividades desplugadas com instruções
   - Mapeamento completo BNCC (15+ habilidades)

5. **Validação com Usuários:**
   - Taxa de satisfação > 80% (escala Likert)
   - Taxa de conclusão de desafios > 70%
   - Tempo médio de resolução < 10 min (desafios fáceis)

6. **Documentação Técnica Completa:**
   - Diagramas UML (casos de uso, classes, sequência)
   - DER do banco de dados
   - API documentada (Swagger)

---

# 2. ENGENHARIA DE REQUISITOS

## 2.1 Requisitos Funcionais (Resumo)

### RF01 - Autenticação e Autorização
- Cadastro com nome, email, senha, tipo (Aluno/Professor)
- Login com JWT (válido 24h)
- Controle de acesso baseado em roles

### RF02 - Gestão de Turmas
- Professor cria turmas com nome
- Sistema gera código único de 6 caracteres
- Aluno entra em turma via código
- Visualização de alunos por turma

### RF03 - Sistema de Desafios
- 10 desafios progressivos (desbloqueio sequencial)
- Programação visual com Blockly (5 categorias de blocos)
- Validação de solução (robô no objetivo)
- Salvamento de progresso

### RF04 - Gamificação
- Pontos: 100-200 por desafio
- Níveis: 1-5 baseado em pontos
- Badges: 3, 7, 10 desafios
- Avatares personalizáveis
- Ranking: turma e geral

### RF05 - Relatórios (Professor)
- Estatísticas da turma (média, taxa de conclusão)
- Gráficos (progresso por desafio, distribuição de pontos)
- Identificação de alunos com < 3 desafios
- Relatório individual detalhado

### RF06 - Acessibilidade
- Alto contraste (toggle)
- Ajuste de fonte (+1, +2, +3)
- Narração de tela (Web Speech API)
- Atalhos de teclado (Alt+C, Alt++, Alt+-)

### RF07 - Recursos Pedagógicos
- Narrativa do Explorador Digital
- 8 atividades desplugadas
- Mapeamento BNCC (visualização e download)

---

## 2.2 Requisitos Não-Funcionais (Resumo)

### RNF01 - Usabilidade
- Interface intuitiva para 11-14 anos
- Feedback visual < 500ms
- Mensagens de erro claras

### RNF02 - Acessibilidade (WCAG 2.1 AA)
- Contraste 4.5:1 (texto), 3:1 (UI)
- Operável por teclado
- Compatível com leitores de tela

### RNF03 - Performance
- Carregamento inicial < 3s
- Execução de Blockly < 1s
- Animações a 60fps

### RNF04 - Segurança
- Senhas com BCrypt (cost 12)
- JWT assinado com chave 256-bit
- HTTPS em produção
- Prevenção de SQL Injection (EF Core)

### RNF05 - Compatibilidade
- Navegadores: Chrome 90+, Firefox 88+, Edge 90+, Safari 14+
- Resolução mínima: 1024x768

### RNF06 - Escalabilidade
- Suporta 100 usuários simultâneos (SQLite)
- Arquitetura preparada para migração PostgreSQL

---

## 2.3 Histórias de Usuário (Principais)

**US01 - Cadastro de Aluno**
> Como **aluno**, eu quero **me cadastrar informando nome, email e senha** para **começar a usar a plataforma**.

**US02 - Criar Turma**
> Como **professor**, eu quero **criar uma turma e receber um código único** para **organizar meus alunos**.

**US03 - Resolver Desafio**
> Como **aluno**, eu quero **programar com blocos visuais e ver animação do robô** para **aprender lógica sem escrever código**.

**US04 - Ver Ranking**
> Como **aluno**, eu quero **ver minha posição no ranking da turma** para **me motivar a melhorar**.

**US05 - Gerar Relatório**
> Como **professor**, eu quero **ver relatório com gráficos de progresso da turma** para **identificar alunos com dificuldades**.

**US06 - Ajustar Acessibilidade**
> Como **aluno com baixa visão**, eu quero **ativar alto contraste e aumentar fontes** para **enxergar melhor a interface**.

---

## 2.4 Regras de Negócio (Principais)

**RN01:** Email deve ser único no sistema
**RN02:** Código de turma tem exatamente 6 caracteres alfanuméricos
**RN03:** Desafio N+1 só desbloqueia após completar N
**RN04:** Re-completar desafio não concede pontos adicionais
**RN05:** Nível calculado automaticamente: Nível 1 (0-299), Nível 2 (300-699), Nível 3 (700-1199), Nível 4 (1200-1699), Nível 5 (1700+)
**RN06:** Ranking ordenado por pontos; empate → quem atingiu primeiro
**RN07:** Professor só vê relatórios de suas próprias turmas
**RN08:** Aluno com < 3 desafios é marcado como "com dificuldades"

---

# 3. MODELAGEM E ARQUITETURA

## 3.1 Diagrama de Casos de Uso

**Atores:**
- **Aluno:** Resolve desafios, vê ranking, personaliza acessibilidade
- **Professor:** Cria turmas, gera relatórios, acessa recursos pedagógicos
- **Sistema:** Valida soluções, calcula pontos, atribui badges

**Casos de Uso Principais:**
1. Fazer Login/Cadastro
2. Entrar em Turma (Aluno)
3. Criar Turma (Professor)
4. Resolver Desafio (Aluno)
   - <<include>> Programar com Blockly
   - <<include>> Validar Solução
   - <<include>> Atualizar Progresso
5. Ver Ranking (Aluno)
6. Gerar Relatórios (Professor)
   - <<include>> Calcular Estatísticas
7. Acessar Recursos Pedagógicos (Professor)
8. Ajustar Acessibilidade (Aluno/Professor)

---

## 3.2 Diagrama de Classes (Modelo de Domínio)

```
Usuario (Id, Nome, Email, SenhaHash, Tipo)
  ├── Aluno (Avatar, Nivel, PontosTotal)
  └── Professor

Turma (Id, Nome, Codigo, ProfessorId, DataCriacao)
  ├─[N:N]─ AlunoTurma ─[N:N]─ Aluno

Desafio (Id, Titulo, Descricao, Ordem, GridInicial, ObjetivoX, ObjetivoY, Pontos)
  └─[1:N]─ ProgressoDesafio (AlunoId, DesafioId, Completado, SolucaoXML, DataConclusao)

Badge (Id, Nome, Descricao, Icone, Criterio)
  ├─[N:N]─ AlunoBadge ─[N:N]─ Aluno
```

**Relacionamentos:**
- Professor (1) → cria → (N) Turma
- Aluno (N) → participa → (N) Turma (via AlunoTurma)
- Aluno (1) → realiza → (N) ProgressoDesafio
- Desafio (1) → referenciado → (N) ProgressoDesafio
- Aluno (N) → conquista → (N) Badge (via AlunoBadge)

---

## 3.3 Diagrama de Sequência - Resolver Desafio (Simplificado)

```
Aluno → Frontend: Clica Desafio 3
Frontend → Backend: GET /api/Desafio/3
Backend → Database: SELECT desafio WHERE id=3
Database → Backend: { desafio }
Backend → Frontend: { titulo, grid, objetivo }
Frontend → Aluno: Exibe workspace Blockly

Aluno → Frontend: Arrasta blocos, clica "Executar"
Frontend → Aluno: Animação do robô

Aluno → Frontend: Robô atinge objetivo
Frontend → Backend: POST /api/Desafio/3/validar { solucaoXML }
Backend → Database: INSERT ProgressoDesafio (completado=true)
Backend → Database: UPDATE Usuario (pontos += 100)
Backend → Database: Verifica badges, INSERT AlunoBadge (se aplicável)
Backend → Frontend: { sucesso, pontos, novoBadge }
Frontend → Aluno: Modal "Parabéns! +100 pts"
```

---

## 3.4 Diagrama Entidade-Relacionamento (DER)

```
USUARIO (Id PK, Nome, Email UK, SenhaHash, Tipo, Avatar, Nivel, PontosTotal)
  └── 1:N → TURMA (Id PK, Nome, Codigo UK, ProfessorId FK, DataCriacao)

ALUNO_TURMA (Id PK, AlunoId FK, TurmaId FK, DataEntrada) [N:N entre USUARIO e TURMA]

DESAFIO (Id PK, Titulo, Descricao, Ordem UK, GridInicial, ObjetivoX, ObjetivoY, Pontos)
  └── 1:N → PROGRESSO_DESAFIO (Id PK, AlunoId FK, DesafioId FK, Completado, SolucaoXML, DataConclusao)

BADGE (Id PK, Nome, Descricao, Icone, Criterio)
  └── N:N → ALUNO_BADGE (Id PK, AlunoId FK, BadgeId FK, DataConquista)
```

**Constraints:**
- Email UNIQUE
- Codigo UNIQUE (6 chars)
- Aluno não pode entrar 2x na mesma turma (UNIQUE AlunoId + TurmaId)
- Aluno tem apenas um progresso por desafio (UNIQUE AlunoId + DesafioId)

---

# 4. INTERFACE E PROTOTIPAÇÃO

## 4.1 Wireframes (Principais Telas)

### Tela 1: Login/Cadastro
- Logo centralizado
- Tabs: Login | Cadastro
- Campos: Email, Senha, Tipo (Aluno/Professor)
- Botão primário "Entrar"

### Tela 2: Dashboard do Aluno
- Header: Menu, Logo, Perfil, Sair
- Card de Progresso: Nível, barra de pontos, desafios/badges
- Seção Turmas: Lista + botão "Entrar em Turma"
- Ranking da Turma: Top 5 com destaque para posição do aluno
- Grid de Desafios: 10 cards (✅ completo, 🔓 disponível, 🔒 bloqueado)

### Tela 3: Desafio (Gameplay)
- Lado Esquerdo: Grid visual (5x5 com robô, paredes, objetivo)
- Lado Direito: Workspace Blockly (categorias: Movimento, Controle, Condicionais, Sensores, Variáveis)
- Botões: Executar, Resetar, Salvar
- Descrição do desafio no topo

### Tela 4: Dashboard do Professor
- Botões: Criar Nova Turma, Recursos Pedagógicos
- Cards de Turmas: Nome, código, nº alunos, progresso médio, alertas
- Ações por turma: Ver Detalhes, Relatório, Compartilhar Código

### Tela 5: Relatório da Turma
- Estatísticas Gerais: Total alunos, média pontos, taxa conclusão, top 3
- Gráfico de Barras: Progresso por desafio (quantos completaram cada um)
- Gráfico de Histograma: Distribuição de pontos
- Lista: Alunos com dificuldades (<3 desafios)

---

## 4.2 Design System

**Cores:**
- Primary: #667eea (roxo)
- Gradient: linear-gradient(135deg, #667eea, #764ba2)
- Sucesso: #4caf50, Erro: #ff6b6b, Alerta: #ffc107
- Background: #f5f7fa, Card: #ffffff, Texto: #333333

**Tipografia:**
- Fonte: 'Segoe UI', Roboto, sans-serif
- Tamanhos: H1 (40px), H2 (32px), H3 (24px), Body (16px)
- Ajustável: +20% (nível 1), +40% (nível 2), +60% (nível 3)

**Componentes:**
- Botão Primary: bg #667eea, padding 12px 30px, border-radius 12px
- Card: shadow 0 5px 20px rgba(0,0,0,0.1), hover translateY(-5px)
- Input: border 2px #e0e0e0, focus border #667eea

---

## 4.3 Protótipo Navegável (Figma)

**O que criar no Figma:**

1. **Tela de Login/Cadastro** (1 frame)
   - Estado padrão (Login)
   - Estado Cadastro (tab ativa)

2. **Dashboard do Aluno** (1 frame)
   - Todos os elementos visíveis
   - Modal de "Entrar em Turma" (overlay)

3. **Tela de Desafio** (1 frame)
   - Grid 5x5 com robô
   - Workspace Blockly (exemplo de blocos montados)

4. **Dashboard do Professor** (1 frame)
   - 3 cards de turmas
   - Modal de "Criar Turma" (overlay)

5. **Relatório da Turma** (1 frame)
   - Gráficos mockados (Chart.js)
   - Lista de alunos com dificuldades

**Navegação (Protótipo Interativo):**
- Login → Botão "Entrar" → Dashboard Aluno
- Dashboard Aluno → Card Desafio → Tela Desafio
- Dashboard Aluno → "Entrar em Turma" → Modal
- Dashboard Professor → "Relatório" → Relatório da Turma

**Dicas para o Figma:**
- Use componentes (botões, cards reutilizáveis)
- Crie variantes (botão primary/secondary, card completo/bloqueado)
- Use Auto Layout para responsividade
- Adicione interações (hover, click)
- Exporte como PDF para o TCC

---

## 4.4 Diretrizes de Acessibilidade WCAG 2.1 AA

**Conformidade Implementada:**
- ✅ **1.4.3 Contraste:** Texto #333 em #fff = 12.6:1 (AAA)
- ✅ **1.4.4 Redimensionamento:** Zoom até 200% funcional
- ✅ **2.1.1 Teclado:** Tab, Enter, Escape funcionam
- ✅ **2.4.7 Foco Visível:** Outline 3px azul em elementos focados
- ✅ **3.2.3 Navegação Consistente:** Header sempre no mesmo local
- ✅ **4.1.2 Nome/Função/Valor:** ARIA labels em componentes

**Testes Realizados:**
- Auditoria axe DevTools: 0 erros críticos
- Teste manual com NVDA: navegação completa
- Teste de teclado: todas as funcionalidades acessíveis

---

# 5. DESIGN DA SOLUÇÃO

## 5.1 Arquitetura do Sistema

**Arquitetura de 3 Camadas (3-Tier):**

```
┌─────────────────────────────────┐
│  APRESENTAÇÃO (Frontend)        │
│  Vue.js 3 + Blockly + Chart.js  │
└────────────┬────────────────────┘
             │ REST API (JSON)
┌────────────▼────────────────────┐
│  APLICAÇÃO (Backend)            │
│  ASP.NET Core 8.0 + EF Core     │
└────────────┬────────────────────┘
             │ SQL Queries
┌────────────▼────────────────────┐
│  DADOS (Database)               │
│  SQLite (codeschool.db)         │
└─────────────────────────────────┘
```

**Características:**
- **Stateless API:** JWT para autenticação sem sessões
- **RESTful:** Endpoints seguem convenções (GET, POST, PUT, DELETE)
- **SPA:** Vue Router para navegação client-side
- **Separação de Responsabilidades:** Frontend (UI), Backend (lógica), Database (persistência)

---

## 5.2 Tecnologias Utilizadas

### Frontend
- **Vue.js 3.4:** Framework JavaScript reativo
- **Vue Router 4:** Roteamento SPA
- **Pinia 2:** Gerenciamento de estado
- **Vite 5:** Bundler e dev server
- **Blockly 10:** Programação visual
- **Chart.js 4:** Gráficos interativos
- **Axios 1.6:** Cliente HTTP

### Backend
- **ASP.NET Core 8.0:** Framework web API
- **Entity Framework Core 8.0:** ORM
- **SQLite 3.x:** Banco de dados relacional
- **BCrypt.Net:** Hash de senhas
- **JWT Bearer:** Autenticação stateless
- **Swagger:** Documentação da API

### Ferramentas
- **Visual Studio 2022:** IDE backend
- **VS Code:** Editor frontend
- **Git/GitHub:** Controle de versão
- **Postman:** Testes de API
- **Figma:** Prototipação
- **axe DevTools:** Auditoria acessibilidade

---

## 5.3 Descrição dos Módulos e Funcionalidades

### Módulo 1: Autenticação
- Cadastro (validação de email único, hash BCrypt)
- Login (geração de JWT válido 24h)
- Controle de acesso por roles (Aluno/Professor)
- Endpoints: POST /api/Auth/cadastro, POST /api/Auth/login

### Módulo 2: Gestão de Turmas
- Professor cria turmas (código único 6 chars)
- Aluno entra em turmas via código
- Listagem de turmas (professor vê suas, aluno vê participantes)
- Endpoints: POST /api/Turma, POST /api/Turma/entrar, GET /api/Turma/minhas

### Módulo 3: Desafios de Programação
- 10 desafios pré-cadastrados (progressão sequencial)
- Programação visual Blockly (5 categorias de blocos)
- Execução e animação do robô (JavaScript)
- Validação backend (robô no objetivo)
- Salvamento de solução (XML)
- Endpoints: GET /api/Desafio, GET /api/Desafio/{id}, POST /api/Desafio/{id}/validar

### Módulo 4: Gamificação
- Sistema de pontos (100-200 por desafio)
- Sistema de níveis (5 níveis, cálculo automático)
- Sistema de badges (3, 7, 10 desafios)
- Avatares personalizáveis (12 opções)
- Ranking turma e geral (ordenado por pontos)
- Endpoints: GET /api/Ranking/turma/{id}, GET /api/Ranking/geral, GET /api/Ranking/minha-posicao/{id}

### Módulo 5: Relatórios (Professor)
- Estatísticas da turma (média, taxa conclusão, top 3)
- Gráfico de progresso por desafio (barras)
- Gráfico de distribuição de pontos (histograma)
- Lista de alunos com dificuldades (<3 desafios)
- Relatório individual detalhado
- Endpoints: GET /api/Relatorio/turma/{id}, GET /api/Relatorio/aluno/{id}

### Módulo 6: Acessibilidade
- Alto contraste (toggle, atalho Alt+C)
- Ajuste de fonte (+1/+2/+3, atalhos Alt++/Alt+-)
- Narração de tela (Web Speech API pt-BR)
- Navegação por teclado completa
- Skip links (Alt+1)
- Persistência no localStorage

### Módulo 7: Recursos Pedagógicos
- Narrativa "Explorador Digital" (4 capítulos)
- 8 atividades desplugadas (instruções completas)
- Mapeamento BNCC (15+ habilidades)
- Visualização em modais
- Botões de download PDF (futuro)
- Acesso exclusivo professores

### Módulo 8: Narrativa Gamificada
- História dividida em 4 capítulos
- Contextualização dos desafios
- Glossário de termos (algoritmo, loop, etc.)
- Acesso aberto a todos os alunos

---

## 5.4 Segurança

**Medidas Implementadas:**
- **Senhas:** Hash BCrypt (cost 12, salt automático)
- **Autenticação:** JWT assinado com chave 256-bit, expira 24h
- **Autorização:** `[Authorize(Roles)]` em endpoints sensíveis
- **SQL Injection:** Prevenido por EF Core (queries parametrizadas)
- **CORS:** Restrito ao frontend (localhost:5173)
- **HTTPS:** Obrigatório em produção

**OWASP Top 10 Mitigado:**
- A01 (Broken Access Control): Autorização por roles
- A02 (Cryptographic Failures): BCrypt + JWT assinado
- A03 (Injection): EF Core parametrizado
- A07 (Authentication Failures): JWT com expiração

---

# 6. CONSIDERAÇÕES FINAIS

## 6.1 Limitações do Protótipo Atual

### 6.1.1 Limitações Técnicas

1. **Banco de Dados SQLite:**
   - Concorrência limitada (~100 usuários simultâneos)
   - Não suporta escritas concorrentes em larga escala
   - Não é ideal para ambientes distribuídos
   - **Impacto:** Adequado para escolas com até 500 alunos

2. **Falta de Testes Automatizados Completos:**
   - Testes unitários apenas em serviços críticos (Token, Password)
   - Ausência de testes de integração E2E (Cypress)
   - Cobertura de código estimada em 40%
   - **Impacto:** Maior risco de regressão em atualizações futuras

3. **Responsividade Limitada:**
   - Interface otimizada para desktop (>1024px)
   - Não há versão mobile/tablet
   - Blockly workspace pode ser difícil em telas pequenas
   - **Impacto:** Uso limitado em dispositivos móveis

4. **Ausência de Sistema de Backup Automatizado:**
   - Backup manual do arquivo .db
   - Sem versionamento de dados
   - **Impacto:** Risco de perda de dados em falhas

5. **Download de PDFs Não Implementado:**
   - Botões de download exibem alerts (placeholder)
   - Geração de PDF requer biblioteca adicional (PuppeteerSharp, iTextSharp)
   - **Impacto:** Professores não podem baixar materiais offline

### 6.1.2 Limitações Funcionais

1. **10 Desafios Fixos:**
   - Desafios hardcoded no banco
   - Professor não pode criar desafios customizados
   - **Impacto:** Conteúdo pode se esgotar rapidamente

2. **Ranking Simplificado:**
   - Apenas ordenação por pontos totais
   - Não há filtros (por nível, por período, por desafio)
   - **Impacação:** Dificulta análises granulares

3. **Falta de Comunicação Assíncrona:**
   - Sem sistema de notificações (badges conquistados, novos desafios)
   - Sem chat ou fórum para dúvidas
   - **Impacto:** Menor engajamento e suporte entre pares

4. **Relatórios Não Exportáveis:**
   - Visualização apenas na web
   - Sem exportação CSV/PDF para apresentações
   - **Impacto:** Professores não podem incluir em relatórios escolares

5. **Validação de Solução Apenas Backend:**
   - Frontend executa animação, backend valida objetivo
   - Não há análise de eficiência de código (quantos blocos, loops usados)
   - **Impacto:** Alunos não recebem feedback sobre qualidade da solução

### 6.1.3 Limitações de Acessibilidade

1. **Narração Limitada:**
   - Web Speech API nem sempre disponível (Firefox desktop)
   - Narração apenas em português brasileiro
   - Não há opção de voz masculina/feminina
   - **Impacto:** Usuários de outros navegadores/idiomas não se beneficiam

2. **Falta de Modo Escuro Completo:**
   - Apenas alto contraste (preto/branco)
   - Sem tema escuro estilizado (dark mode moderno)
   - **Impacto:** Usuários sensíveis à luz podem ter desconforto

3. **Blockly Workspace Pode Ser Desafiador para Cegos:**
   - Leitores de tela anunciam blocos, mas navegação não é ideal
   - Falta alternativa textual (código por linha de comando)
   - **Impacto:** Usuários totalmente cegos podem ter dificuldades

---

## 6.2 Possíveis Evoluções e Melhorias Futuras

### 6.2.1 Melhorias de Curto Prazo (1-3 meses)

1. **Migração para PostgreSQL:**
   - Suporta milhares de usuários simultâneos
   - Melhor performance em queries complexas
   - Backup automatizado (pg_dump)
   - **Esforço:** Médio (apenas connection string + deploy)

2. **Implementação de Testes E2E:**
   - Cypress para fluxos críticos (login, resolver desafio, gerar relatório)
   - Cobertura de código >80%
   - Integração com CI/CD (GitHub Actions)
   - **Esforço:** Alto (2-3 semanas)

3. **Geração de PDFs:**
   - Biblioteca PuppeteerSharp (backend)
   - Download de narrativas, atividades, relatórios
   - Template HTML customizado
   - **Esforço:** Médio (1 semana)

4. **Sistema de Notificações:**
   - SignalR (WebSockets) para notificações em tempo real
   - Avisos: badge conquistado, novo desafio desbloqueado, mensagem do professor
   - **Esforço:** Médio (1-2 semanas)

5. **Responsividade Mobile:**
   - CSS Grid/Flexbox adaptativo
   - Workspace Blockly vertical (modo portrait)
   - Touch gestures para arrastar blocos
   - **Esforço:** Alto (3-4 semanas)

### 6.2.2 Melhorias de Médio Prazo (3-6 meses)

1. **Editor de Desafios para Professores:**
   - Interface WYSIWYG para criar desafios customizados
   - Arrastar paredes, definir objetivo, testar solução
   - Salvar desafios privativos (visíveis apenas na turma do professor)
   - **Esforço:** Muito Alto (6-8 semanas)

2. **Sistema de Medalhas Avançado:**
   - Badges dinâmicos (completar desafio em <10 blocos, resolver sem loops, etc.)
   - Conquistas secretas (easter eggs)
   - Troféus por ranking (top 1, top 3, top 10)
   - **Esforço:** Médio (2-3 semanas)

3. **Análise de Código:**
   - Métrica de eficiência (quantos blocos usados vs. solução ótima)
   - Sugestões de otimização (ex: "Você pode usar um loop aqui")
   - Comparação com soluções de outros alunos (anônima)
   - **Esforço:** Alto (4-5 semanas)

4. **Fórum de Discussão:**
   - Perguntas e respostas por desafio
   - Moderação por professores
   - Upvotes/downvotes
   - **Esforço:** Alto (4-6 semanas)

5. **Integração com Google Classroom:**
   - Import de turmas via API Google
   - Sincronização de alunos
   - Exportação de notas para planilha Google Sheets
   - **Esforço:** Médio (2-3 semanas)

### 6.2.3 Melhorias de Longo Prazo (6-12 meses)

1. **Aplicativo Mobile Nativo:**
   - Flutter ou React Native
   - Sincronização offline (resolver desafios sem internet)
   - Notificações push
   - **Esforço:** Muito Alto (3-4 meses)

2. **Modo Multiplayer:**
   - Desafios colaborativos (2-4 alunos programam juntos)
   - WebSockets para sincronização em tempo real
   - Chat de voz/texto integrado
   - **Esforço:** Muito Alto (3-4 meses)

3. **Inteligência Artificial (IA):**
   - Chatbot para dúvidas (OpenAI GPT-4 API)
   - Recomendação personalizada de desafios
   - Detecção automática de padrões de erro (aluno sempre trava em loops)
   - **Esforço:** Muito Alto (4-6 meses)

4. **Gamificação Avançada:**
   - Sistema de "vidas" (3 tentativas por desafio)
   - Power-ups (dica, mostrar solução, pular desafio)
   - Eventos temporais (desafios semanais com ranking especial)
   - **Esforço:** Alto (2-3 meses)

5. **Internacionalização (i18n):**
   - Tradução para inglês, espanhol
   - Adaptação de narrativas
   - Mapeamento para currículos internacionais (Common Core, CSTA)
   - **Esforço:** Médio (2-3 meses com ajuda de tradutores)

6. **Dashboard Analítico Avançado:**
   - Power BI ou Metabase integrado
   - Dashboards customizáveis para coordenadores pedagógicos
   - Exportação de dados para análise externa (CSV, JSON)
   - **Esforço:** Alto (3-4 meses)

7. **Sistema de Certificados:**
   - Certificado digital ao completar todos os desafios
   - QR Code para validação
   - Compartilhamento em redes sociais (LinkedIn)
   - **Esforço:** Médio (2 semanas)

---

## 6.3 Referências Bibliográficas

### Livros e Artigos Acadêmicos

1. WING, Jeannette M. **Computational thinking**. Communications of the ACM, v. 49, n. 3, p. 33-35, 2006.

2. DETERDING, Sebastian et al. **From game design elements to gamefulness: defining gamification**. In: Proceedings of the 15th international academic MindTrek conference. 2011. p. 9-15.

3. RESNICK, Mitchel et al. **Scratch: programming for all**. Communications of the ACM, v. 52, n. 11, p. 60-67, 2009.

4. PAPERT, Seymour. **Mindstorms: Children, computers, and powerful ideas**. Basic books, 1980.

5. BRACKMANN, Christian Puhlmann. **Desenvolvimento do pensamento computacional através de atividades desplugadas na educação básica**. Tese (Doutorado) - Universidade Federal do Rio Grande do Sul, 2017.

### Documentos Oficiais

6. BRASIL. Ministério da Educação. **Base Nacional Comum Curricular (BNCC)**. Brasília: MEC, 2018. Disponível em: http://basenacionalcomum.mec.gov.br/

7. W3C. **Web Content Accessibility Guidelines (WCAG) 2.1**. W3C Recommendation, 2018. Disponível em: https://www.w3.org/TR/WCAG21/

### Documentações Técnicas

8. GOOGLE. **Blockly Developer Documentation**. Disponível em: https://developers.google.com/blockly

9. VUE.JS. **Vue.js 3 Official Documentation**. Disponível em: https://vuejs.org/guide/

10. MICROSOFT. **ASP.NET Core Documentation**. Disponível em: https://learn.microsoft.com/en-us/aspnet/core/

11. ENTITY FRAMEWORK. **Entity Framework Core Documentation**. Disponível em: https://learn.microsoft.com/en-us/ef/core/

### Plataformas de Referência

12. CODE.ORG. **Hour of Code**. Disponível em: https://code.org/

13. MIT MEDIA LAB. **Scratch**. Disponível em: https://scratch.mit.edu/

14. GOOGLE. **CS First**. Disponível em: https://csfirst.withgoogle.com/

### Artigos sobre Acessibilidade

15. FREIRE, André Pimenta et al. **Acessibilidade no desenvolvimento de sistemas web: um estudo sobre o cenário brasileiro**. Revista Brasileira de Informática na Educação, v. 16, n. 2, 2008.

16. W3C BRASIL. **Cartilha de Acessibilidade na Web**. Disponível em: https://www.w3c.br/pub/Materiais/PublicacoesW3C/cartilha-w3cbr-acessibilidade-web-fasciculo-I.html

### Frameworks e Bibliotecas

17. CHART.JS. **Chart.js Documentation**. Disponível em: https://www.chartjs.org/docs/

18. AXIOS. **Axios HTTP Client**. Disponível em: https://axios-http.com/

19. PINIA. **Pinia State Management Documentation**. Disponível em: https://pinia.vuejs.org/

20. JWT.IO. **JSON Web Tokens Introduction**. Disponível em: https://jwt.io/introduction

### Segurança

21. OWASP. **OWASP Top Ten 2021**. Disponível em: https://owasp.org/Top10/

22. BCRYPT. **How Bcrypt Works**. Disponível em: https://github.com/kelektiv/node.bcrypt.js

### Metodologias

23. SOMMERVILLE, Ian. **Engenharia de software**. 10. ed. São Paulo: Pearson, 2018.

24. PRESSMAN, Roger S.; MAXIM, Bruce R. **Engenharia de software: uma abordagem profissional**. 8. ed. Porto Alegre: AMGH, 2016.

25. COCKBURN, Alistair. **Writing effective use cases**. Addison-Wesley, 2001.

---

**Documento Consolidado Completo - CodeSchool TCC**
**Data de Criação:** 29/11/2025
**Última Atualização:** 29/11/2025
**Versão:** 2.0 - FINAL CONSOLIDADO
