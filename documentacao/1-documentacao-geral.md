# CODESCHOOL
## Plataforma Gamificada e Acessível para Ensino de Pensamento Computacional

---

**Trabalho de Conclusão de Curso**
**Curso:** Ciência da Computação
**Instituição:** [Nome da Instituição]
**Ano:** 2025


**Grupo:**
- Anita [Sobrenome]
- [Outros integrantes se houver]

---

# SUMÁRIO

1. [INTRODUÇÃO](#1-introdução)
2. [SITUAÇÃO-PROBLEMA](#2-situação-problema)
3. [OBJETIVOS](#3-objetivos)
   - 3.1. Objetivo Geral
   - 3.2. Objetivos Específicos
4. [JUSTIFICATIVA E RELEVÂNCIA](#4-justificativa-e-relevância)
5. [PÚBLICO-ALVO E PERFIS DE USUÁRIOS](#5-público-alvo-e-perfis-de-usuários)
6. [REVISÃO DE CONCEITOS](#6-revisão-de-conceitos)
7. [METODOLOGIA](#7-metodologia)
8. [CRONOGRAMA](#8-cronograma)

---

# 1. INTRODUÇÃO

A Base Nacional Comum Curricular (BNCC) estabelece que o desenvolvimento do pensamento computacional deve ser trabalhado desde os anos iniciais do Ensino Fundamental. No entanto, a implementação prática dessa diretriz enfrenta diversos obstáculos nas escolas públicas brasileiras, especialmente em regiões como João Pessoa/PB.

Este projeto apresenta o **CodeSchool**, uma plataforma web educacional gamificada e acessível, desenvolvida para ensinar os pilares do pensamento computacional (decomposição, reconhecimento de padrões, abstração e algoritmos) a alunos do 6º ao 9º ano do Ensino Fundamental II, permitindo que professores sem conhecimento prévio em programação possam aplicá-la em sala de aula.

A solução combina:
- **Gamificação** (pontos, badges, avatares, níveis, ranking, narrativas)
- **Editor visual de blocos** (Blockly) para programação intuitiva
- **Acessibilidade** (WCAG) com alto contraste, navegação por teclado, narração de tela
- **Módulo do professor** com relatórios, gráficos e gestão de turmas
- **Atividades desplugadas** complementares
- **Alinhamento com BNCC** documentado

---

# 2. SITUAÇÃO-PROBLEMA

## 2.1. Contexto

A BNCC (Base Nacional Comum Curricular) prevê o desenvolvimento do pensamento computacional desde os anos iniciais. No entanto, muitas escolas públicas, especialmente em regiões como João Pessoa/PB, carecem de:

- **Recursos tecnológicos:** Falta de laboratórios de informática equipados
- **Formação docente:** Professores sem capacitação em programação ou pensamento computacional
- **Materiais adequados:** Ferramentas existentes não são inclusivas ou são muito complexas
- **Acessibilidade:** Plataformas não atendem alunos com deficiências (visuais, auditivas, cognitivas)

## 2.2. Problema Central

> **"Como criar uma plataforma educacional web, de baixo custo e alta acessibilidade, que utilize a gamificação para ensinar os pilares do pensamento computacional (decomposição, reconhecimento de padrões, abstração e algoritmos) a alunos do 6º ao 9º ano, permitindo que professores sem conhecimento prévio em programação possam aplicá-la em sala de aula?"**

## 2.3. Desafios Identificados

1. **Engajamento:** Como tornar o aprendizado de lógica de programação atrativo para adolescentes?
2. **Inclusão:** Como garantir que alunos com deficiências tenham acesso igual?
3. **Simplicidade:** Como criar uma ferramenta que professores não técnicos consigam usar?
4. **Custo:** Como desenvolver uma solução que funcione em dispositivos de baixo custo?
5. **Currículo:** Como alinhar os conteúdos com as competências da BNCC?

---

# 3. OBJETIVOS

## 3.1. Objetivo Geral

Desenvolver uma **plataforma web educacional gamificada e acessível** que ensine pensamento computacional a alunos do Ensino Fundamental II (6º ao 9º ano) de escolas públicas, através de desafios de programação visual, permitindo que professores sem conhecimento técnico possam gerenciar turmas e acompanhar o progresso dos alunos.

## 3.2. Objetivos Específicos

1. **Implementar sistema de gamificação** com:
   - Pontos, badges, avatares progressivos
   - Sistema de níveis (1 a 5)
   - Ranking colaborativo por turma
   - Narrativas contextualizadas

2. **Desenvolver editor visual de blocos** (Blockly) contendo:
   - Blocos de movimento básico
   - Estruturas de repetição (loops)
   - Condicionais (if/else)
   - Variáveis
   - Sensores virtuais

3. **Criar 10 desafios progressivos** que ensinem:
   - Decomposição de problemas
   - Reconhecimento de padrões
   - Abstração
   - Criação de algoritmos

4. **Garantir acessibilidade (WCAG 2.1 nível AA)** através de:
   - Alto contraste
   - Navegação completa por teclado
   - Compatibilidade com leitores de tela
   - Atalhos de teclado
   - Responsividade (mobile, tablet, desktop)

5. **Implementar módulo do professor** que permita:
   - Criar e gerenciar turmas
   - Acompanhar progresso individual e coletivo
   - Gerar relatórios visuais (gráficos)
   - Exportar dados (CSV)
   - Acessar recursos pedagógicos

6. **Fornecer materiais complementares:**
   - 8 atividades desplugadas (sem computador)
   - Mapeamento completo com BNCC
   - Narrativas contextualizadas

7. **Validar alinhamento com BNCC** mapeando:
   - Competências gerais atendidas
   - Habilidades de Matemática (EF06 a EF09)
   - Os 4 pilares do pensamento computacional

---

# 4. JUSTIFICATIVA E RELEVÂNCIA

## 4.1. Relevância Educacional

### **Alinhamento com BNCC**
A BNCC estabelece que alunos devem "compreender, utilizar e criar tecnologias digitais de forma crítica, significativa, reflexiva e ética" (Competência 5). O CodeSchool atende diretamente a essa competência, além de trabalhar:

- **Matemática:** 15+ habilidades (coordenadas, geometria, números, funções)
- **Pensamento Científico:** Experimentação, teste de hipóteses (debug)
- **Resolução de Problemas:** Decomposição, algoritmos

### **Inclusão Digital**
Segundo dados do IBGE (2022), apenas 58% dos alunos de escolas públicas têm acesso a computadores em casa. Uma plataforma web resolve isso:
- ✅ Funciona em qualquer navegador
- ✅ Compatível com dispositivos de baixo custo (Chromebooks, tablets antigos)
- ✅ Não requer instalação

### **Acessibilidade**
De acordo com o Censo Escolar (2021), cerca de 90 mil alunos com deficiência estão matriculados no Ensino Fundamental II. O CodeSchool é uma das poucas plataformas educacionais de programação que implementa:
- ✅ WCAG 2.1 nível AA
- ✅ Navegação por teclado
- ✅ Alto contraste
- ✅ Narração de tela
- ✅ Interface responsiva

## 4.2. Relevância Social

### **Combate à Desigualdade**
O ensino de programação nas escolas públicas é escasso. Segundo pesquisa da SBC (Sociedade Brasileira de Computação, 2020):
- Apenas 12% das escolas públicas ensinam programação
- Menos de 5% dos professores têm formação na área

O CodeSchool democratiza esse acesso oferecendo:
- ✅ Gratuito e open-source
- ✅ Sem necessidade de conhecimento prévio do professor
- ✅ Materiais pedagógicos inclusos

### **Preparação para o Futuro**
O mercado de trabalho digital cresce 20% ao ano no Brasil. Ensinar pensamento computacional prepara alunos para:
- Resolução lógica de problemas
- Pensamento crítico e estruturado
- Profissões do futuro (STEM)

## 4.3. Relevância Técnica

### **Contribuição Acadêmica**
O projeto aplica conhecimentos de:
- **Engenharia de Software:** Arquitetura MVC, padrões de projeto
- **Desenvolvimento Web:** Front-end (Vue.js) e Back-end (ASP.NET Core)
- **Banco de Dados:** Modelagem relacional, SQL
- **IHC:** Design centrado no usuário, acessibilidade
- **Game Design:** Mecânicas de gamificação

### **Inovação**
Diferencial em relação a plataformas existentes (Code.org, Scratch):
- ✅ Foco específico em pensamento computacional alinhado à BNCC
- ✅ Acessibilidade implementada desde o início
- ✅ Sistema completo de gestão para professores
- ✅ Atividades desplugadas integradas
- ✅ Narrativas contextualizadas
- ✅ Ranking colaborativo (não apenas competitivo)

## 4.4. Impacto Esperado

### **Curto Prazo (6 meses)**
- Uso em 5-10 turmas piloto
- 150-300 alunos alcançados
- Feedback de professores para melhorias

### **Médio Prazo (1-2 anos)**
- Expansão para rede municipal de ensino
- 50+ escolas usando
- Formação de professores multiplicadores

### **Longo Prazo (3-5 anos)**
- Adoção em nível estadual/nacional
- Integração com outras disciplinas (Física, Química)
- Comunidade de professores compartilhando desafios

---

# 5. PÚBLICO-ALVO E PERFIS DE USUÁRIOS

## 5.1. Público-Alvo Primário

### **Alunos do Ensino Fundamental II (6º ao 9º ano)**

**Características:**
- **Idade:** 11 a 15 anos
- **Nível de escolaridade:** 6º ao 9º ano
- **Contexto:** Escolas públicas da rede municipal/estadual
- **Conhecimento prévio:** Nenhum conhecimento de programação necessário
- **Acesso à tecnologia:** Variável (desde sem acesso em casa até uso frequente de smartphones)

**Necessidades:**
- Interface visual e intuitiva
- Feedback imediato
- Motivação através de gamificação
- Contextualização através de narrativas
- Desafios progressivos (do fácil ao difícil)

**Barreiras:**
- Baixa alfabetização digital em alguns casos
- Dificuldade de concentração
- Ansiedade com erros/falhas
- Possíveis deficiências (visual, auditiva, cognitiva)

## 5.2. Público-Alvo Secundário

### **Professores de Matemática, Tecnologia ou Disciplinas Afins**

**Características:**
- **Formação:** Licenciatura em Matemática, Pedagogia, ou áreas correlatas
- **Conhecimento técnico:** Variável (desde nenhum até básico em informática)
- **Contexto:** Rede pública de ensino
- **Carga de trabalho:** Alta (40h semanais, múltiplas turmas)

**Necessidades:**
- Ferramenta simples que não exija conhecimento de programação
- Materiais pedagógicos prontos
- Relatórios visuais de progresso
- Justificativa curricular (BNCC)
- Suporte e documentação clara

**Barreiras:**
- Resistência à tecnologia
- Falta de tempo para aprender novas ferramentas
- Medo de não conseguir resolver dúvidas dos alunos

## 5.3. Perfis de Usuários (Personas)

### **Persona 1: João, o Aluno Curioso**

**Perfil:**
- 12 anos, 7º ano
- Gosta de jogos de celular
- Tem dificuldade em Matemática
- Acessa internet pelo celular da mãe
- Nunca programou antes

**Objetivos:**
- Aprender algo novo de forma divertida
- Ganhar pontos e badges
- Competir de forma saudável com colegas

**Frustrações:**
- Aulas tradicionais "chatas"
- Não entende "para que serve" a Matemática
- Desiste fácil quando erra

**Como CodeSchool ajuda:**
- Gamificação mantém engajamento
- Narrativas dão contexto
- Feedback imediato evita frustração
- Validação de soluções ensina o "caminho certo"

---

### **Persona 2: Maria, a Aluna com Deficiência Visual**

**Perfil:**
- 13 anos, 8º ano
- Tem baixa visão (não cegueira total)
- Usa leitor de tela em casa
- Inteligente e dedicada
- Frustrada com plataformas inacessíveis

**Objetivos:**
- Participar das mesmas atividades que os colegas
- Aprender pensamento computacional
- Não depender de ajuda constante

**Frustrações:**
- Plataformas educacionais não acessíveis
- Professores que não sabem adaptar atividades
- Sensação de exclusão

**Como CodeSchool ajuda:**
- Alto contraste para baixa visão
- Navegação completa por teclado
- Narração de textos ao passar o mouse
- Atalhos de teclado (Alt+C, Alt+1, etc.)

---

### **Persona 3: Professora Ana, a Educadora Dedicada**

**Perfil:**
- 38 anos, professora de Matemática
- 15 anos de experiência
- Conhecimento básico de informática (Word, PowerPoint)
- Nunca programou
- Quer inovar nas aulas

**Objetivos:**
- Tornar aulas mais engajadoras
- Cumprir currículo da BNCC
- Acompanhar progresso dos alunos
- Justificar uso de tecnologia para coordenação

**Frustrações:**
- Falta de tempo para aprender programação
- Ferramentas complexas
- Alunos desengajados
- Cobrança por inovação sem recursos

**Como CodeSchool ajuda:**
- Não precisa saber programar
- Interface intuitiva
- Materiais pedagógicos prontos (atividades desplugadas)
- Relatórios automáticos
- Mapeamento BNCC completo

---

### **Persona 4: Pedro, o Aluno Avançado**

**Perfil:**
- 14 anos, 9º ano
- Já programa em Scratch em casa
- Gosta de desafios
- Quer seguir carreira em TI

**Objetivos:**
- Desafios mais complexos
- Aprender conceitos avançados (variáveis, condicionais)
- Competir no ranking

**Frustrações:**
- Aulas muito básicas
- Falta de desafios progressivos
- Não há onde aplicar o conhecimento

**Como CodeSchool ajuda:**
- 10 desafios progressivos (fácil → difícil)
- Blocos avançados (variáveis, condicionais, sensores)
- Ranking para medir progresso
- Validação rigorosa (não aceita "qualquer solução")

---

## 5.4. Público-Alvo Terciário

### **Gestores Educacionais e Coordenadores Pedagógicos**

**Interesse:**
- Ferramentas alinhadas com BNCC
- Evidências de aprendizado
- Baixo custo
- Escalabilidade

**Como CodeSchool atende:**
- Documentação completa de alinhamento BNCC
- Relatórios de progresso
- Gratuito e web (sem custos de infraestrutura)
- Pode ser usado em larga escala

---

# 6. REVISÃO DE CONCEITOS

## 6.1. Pensamento Computacional

**Definição (Wing, 2006):**
> "Pensamento Computacional é o processo de pensamento envolvido na formulação de problemas e suas soluções de modo que as soluções sejam representadas de forma que possam ser efetivamente realizadas por um agente de processamento de informação."

**Os 4 Pilares:**

1. **Decomposição:** Dividir problemas complexos em partes menores
2. **Reconhecimento de Padrões:** Identificar similaridades e tendências
3. **Abstração:** Focar no essencial, ignorar detalhes irrelevantes
4. **Algoritmos:** Criar sequência de passos para resolver o problema

**Aplicação no CodeSchool:**
- Desafios exigem decomposição (dividir movimentos do robô)
- Loops ensinam reconhecimento de padrões
- Funções e blocos representam abstração
- Sequências de comandos são algoritmos

## 6.2. Gamificação na Educação

**Definição (Deterding et al., 2011):**
> "Uso de elementos de design de jogos em contextos não-jogo."

**Elementos implementados:**
- **Pontos:** Recompensa por completar desafios
- **Badges:** Conquistas especiais (Primeira Conquista, Mestre dos Loops)
- **Níveis:** Progressão de 1 a 5 baseada em pontos
- **Avatares:** Representação visual do progresso
- **Ranking:** Competição saudável e colaborativa
- **Narrativas:** Contextualização (Explorador Digital)
- **Feedback imediato:** Validação instantânea

**Evidências de eficácia:**
- Hamari et al. (2014): Gamificação aumenta engajamento em 80% dos casos
- Domínguez et al. (2013): Melhora desempenho em atividades práticas

## 6.3. Acessibilidade Web (WCAG)

**WCAG 2.1 (Web Content Accessibility Guidelines):**

Níveis: A, AA, AAA
**CodeSchool implementa: Nível AA**

**Princípios WCAG:**

1. **Perceptível:**
   - ✅ Alto contraste (taxa 4.5:1)
   - ✅ Textos alternativos para emojis/ícones
   - ✅ Compatível com ampliação de texto

2. **Operável:**
   - ✅ Navegação completa por teclado
   - ✅ Atalhos customizados (Alt+C, Alt+1, Alt++, Alt+-)
   - ✅ Sem elementos que piscam (evita convulsões)

3. **Compreensível:**
   - ✅ Linguagem clara e simples
   - ✅ Mensagens de erro descritivas
   - ✅ Fluxo de navegação lógico

4. **Robusto:**
   - ✅ Compatível com leitores de tela
   - ✅ Código semântico (HTML5)
   - ✅ Funciona em navegadores antigos

## 6.4. Blockly (Editor Visual)

**Definição:**
> Biblioteca JavaScript desenvolvida pelo Google que permite criar editores de programação visual baseados em blocos.

**Vantagens para educação:**
- ✅ Sem erros de sintaxe (blocos encaixam apenas se fizer sentido)
- ✅ Visual e intuitivo
- ✅ Reduz barreira de entrada
- ✅ Ensina lógica sem código textual

**Blocos implementados no CodeSchool:**
- Movimento (mover, virar)
- Controle (repetir)
- Condicionais (se, se/senão)
- Sensores (tem parede?, está no objetivo?)
- Variáveis (definir, obter, incrementar)
- Lógica (comparações)

## 6.5. Alinhamento com BNCC

**Competências Gerais atendidas (5 de 10):**
- Competência 1: Conhecimento
- Competência 2: Pensamento Científico
- Competência 4: Comunicação
- Competência 5: Cultura Digital
- Competência 10: Responsabilidade

**Habilidades de Matemática (15+):**
- EF06MA16: Plano cartesiano
- EF06MA18: Polígonos
- EF07MA18: Operações numéricas
- EF08MA06: Porcentagem
- EF09MA06: Funções
- ... e mais

---

# 7. METODOLOGIA

## 7.1. Abordagem de Desenvolvimento

**Metodologia Híbrida:**
- **Design Centrado no Usuário (DCU):** Prioriza necessidades de alunos e professores
- **Scrum (adaptado):** Desenvolvimento incremental com sprints de 1 semana

**Justificativa:**
- DCU garante que a solução atende aos usuários reais
- Scrum permite adaptação rápida baseada em feedback
- Ciclos curtos permitem validação contínua

## 7.2. Fases do Projeto

### **Fase 1: Levantamento de Requisitos (3 semanas)**
**Atividades:**
- Entrevistas com professores (3 entrevistas)
- Questionário com alunos (20 respondentes)
- Análise de plataformas similares (Code.org, Scratch)
- Definição de personas
- Priorização de funcionalidades (MoSCoW)

**Entregas:**
- Documento de requisitos
- Personas documentadas
- Backlog inicial

### **Fase 2: Prototipação e Design (3 semanas)**
**Atividades:**
- Wireframes de baixa fidelidade (papel)
- Protótipo navegável no Figma
- Testes de usabilidade com 5 professores
- Definição da identidade visual
- Escolha de paleta de cores acessível

**Entregas:**
- Protótipo Figma interativo
- Guia de estilo visual
- Relatório de testes de usabilidade

### **Fase 3: Desenvolvimento Front-end (2 semanas)**
**Atividades:**
- Estruturação do projeto Vue.js
- Implementação de componentes reutilizáveis
- Integração do Blockly
- Implementação de acessibilidade
- Testes em diferentes navegadores

**Entregas:**
- Interface funcional
- Componentes documentados
- Testes de acessibilidade aprovados

### **Fase 4: Desenvolvimento Back-end (2 semanas)**
**Atividades:**
- Modelagem do banco de dados
- Implementação de APIs REST
- Autenticação JWT
- Sistema de gamificação
- Lógica de validação de desafios

**Entregas:**
- API documentada (Swagger)
- Banco de dados modelado
- Testes unitários

### **Fase 5: Integração e Gamificação (1 semana)**
**Atividades:**
- Integração front + back
- Sistema de pontos e badges
- Ranking
- Narrativas
- Relatórios visuais (Chart.js)

**Entregas:**
- Sistema integrado funcional
- Dashboards completos

### **Fase 6: Testes e Ajustes (2 semanas)**
**Atividades:**
- Testes funcionais (todas funcionalidades)
- Testes de acessibilidade (WCAG)
- Testes de responsividade
- Testes de segurança
- Correção de bugs
- Validação com usuários reais (3 turmas piloto)

**Entregas:**
- Relatório de testes
- Sistema estável e validado

### **Fase 7: Documentação e Entrega (1 semana)**
**Atividades:**
- Documentação técnica completa
- Mapeamento BNCC
- Atividades desplugadas
- Narrativas documentadas
- Manual do professor
- Manual do aluno
- Vídeo de demonstração

**Entregas:**
- Documentação completa
- Materiais pedagógicos
- Apresentação final

---

# 8. CRONOGRAMA

## 8.1. Visão Geral (12 semanas)

| Fase | Duração | Semanas |
|------|---------|---------|
| 1. Levantamento de Requisitos | 3 semanas | 1-3 |
| 2. Prototipação e Design | 3 semanas | 4-6 |
| 3. Desenvolvimento Front-end | 2 semanas | 7-8 |
| 4. Desenvolvimento Back-end | 2 semanas | 7-8 (paralelo) |
| 5. Integração e Gamificação | 1 semana | 9 |
| 6. Testes e Ajustes | 2 semanas | 10-11 |
| 7. Documentação e Entrega | 1 semana | 12 |

**Total:** 12 semanas (3 meses)

## 8.2. Cronograma Detalhado

### **Semanas 1-3: Levantamento**
- S1: Entrevistas e questionários
- S2: Análise e definição de personas
- S3: Documentação de requisitos

### **Semanas 4-6: Design**
- S4: Wireframes e prototipação
- S5: Testes de usabilidade
- S6: Ajustes e finalização

### **Semanas 7-8: Desenvolvimento**
- S7: Front-end (estrutura) + Back-end (API)
- S8: Front-end (componentes) + Back-end (gamificação)

### **Semana 9: Integração**
- Conexão front + back
- Sistema de pontos, badges, ranking
- Relatórios

### **Semanas 10-11: Testes**
- S10: Testes funcionais e de acessibilidade
- S11: Validação com usuários + correções

### **Semana 12: Entrega**
- Documentação final
- Materiais pedagógicos
- Apresentação

---

# 9. RESULTADOS ESPERADOS

## 9.1. Entregas Técnicas

✅ **Plataforma Web Completa:**
- Front-end responsivo (Vue.js)
- Back-end robusto (ASP.NET Core)
- Banco de dados relacional (SQLite)
- 10 desafios de programação visual
- Sistema completo de gamificação
- Módulo do professor
- Acessibilidade WCAG 2.1 nível AA

## 9.2. Entregas Pedagógicas

✅ **Materiais Complementares:**
- 8 atividades desplugadas
- Mapeamento BNCC completo
- Narrativas contextualizadas
- Manual do professor
- Manual do aluno

## 9.3. Impacto Educacional

**Espera-se que:**
- Alunos desenvolvam pensamento computacional
- Professores consigam ensinar sem conhecimento prévio
- Alunos com deficiências tenham acesso igualitário
- Escolas públicas implementem ensino de programação
- Haja aumento no engajamento dos alunos

## 9.4. Contribuição Científica

**Projeto contribui para:**
- Democratização do ensino de programação
- Inclusão digital e acessibilidade
- Aplicação prática de teorias de IHC e Game Design
- Modelo replicável para outras regiões

---

**FIM DO DOCUMENTO 1**
