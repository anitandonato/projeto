# 3. MODELAGEM E ARQUITETURA

## 3.1 Diagramas UML

### 3.1.1 Diagrama de Casos de Uso

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        SISTEMA CODESCHOOL                                │
└─────────────────────────────────────────────────────────────────────────┘

    ┌──────────┐                                              ┌──────────┐
    │  Aluno   │                                              │Professor │
    └────┬─────┘                                              └────┬─────┘
         │                                                         │
         │                                                         │
         ├──────────────> (Fazer Cadastro)                        │
         │                       │                                 │
         │                       │                                 │
         ├──────────────> (Fazer Login) <─────────────────────────┤
         │                       │                                 │
         │                       │                                 │
         ├──────────────> (Entrar em Turma)                       │
         │                       │                                 │
         │                       │ <<include>>                     │
         │                  (Validar Código)                       │
         │                                                         │
         │                                                         │
         ├──────────────> (Visualizar Desafios)                   │
         │                       │                                 │
         │                       │ <<extend>>                      │
         │                  (Filtrar por                           │
         │                   Dificuldade)                          │
         │                                                         │
         │                                                         │
         ├──────────────> (Resolver Desafio)                      │
         │                       │                                 │
         │                       │ <<include>>                     │
         │                  (Programar com                         │
         │                     Blockly) ──────────┐                │
         │                       │                │                │
         │                       │ <<include>>    │                │
         │                  (Validar Solução)     │                │
         │                       │                │                │
         │                       │ <<include>>    │                │
         │                  (Atualizar Progresso) │                │
         │                                        │                │
         │                                   ┌────▼─────┐          │
         │                                   │  Sistema │          │
         │                                   │   de     │          │
         │                                   │Execução  │          │
         │                                   └──────────┘          │
         │                                                         │
         ├──────────────> (Ver Ranking)                            │
         │                       │                                 │
         │                       │ <<extend>>                      │
         │                  (Filtrar por Turma)                    │
         │                                                         │
         │                                                         │
         ├──────────────> (Escolher Avatar)                        │
         │                                                         │
         │                                                         │
         ├──────────────> (Visualizar Narrativa)                   │
         │                                                         │
         │                                                         │
         ├──────────────> (Ajustar Acessibilidade)                 │
         │                       │                                 │
         │                       │ <<extend>>                      │
         │                  (Ativar Alto Contraste)                │
         │                       │                                 │
         │                       │ <<extend>>                      │
         │                  (Ajustar Fonte)                        │
         │                       │                                 │
         │                       │ <<extend>>                      │
         │                  (Ativar Narração)                      │
         │                                                         │
         │                                                         │
         │                                                         ├──────> (Criar Turma)
         │                                                         │              │
         │                                                         │              │ <<include>>
         │                                                         │         (Gerar Código
         │                                                         │            Único)
         │                                                         │
         │                                                         │
         │                                                         ├──────> (Visualizar Alunos)
         │                                                         │              │
         │                                                         │              │ <<extend>>
         │                                                         │         (Ver Detalhes
         │                                                         │           do Aluno)
         │                                                         │
         │                                                         │
         │                                                         ├──────> (Gerar Relatórios)
         │                                                         │              │
         │                                                         │              │ <<include>>
         │                                                         │         (Calcular
         │                                                         │        Estatísticas)
         │                                                         │              │
         │                                                         │              │ <<extend>>
         │                                                         │         (Exportar PDF)
         │                                                         │
         │                                                         │
         │                                                         ├──────> (Acessar Recursos
         │                                                         │         Pedagógicos)
         │                                                         │              │
         │                                                         │              │ <<extend>>
         │                                                         │         (Visualizar
         │                                                         │          Narrativas)
         │                                                         │              │
         │                                                         │              │ <<extend>>
         │                                                         │         (Baixar
         │                                                         │          Atividades)
         │                                                         │              │
         │                                                         │              │ <<extend>>
         │                                                         │         (Ver Mapeamento
         │                                                         │            BNCC)
```

**Atores:**
- **Aluno:** Estudante do ensino fundamental (11-14 anos) que resolve desafios
- **Professor:** Educador que gerencia turmas e acompanha progresso
- **Sistema de Execução:** Componente automatizado que valida e executa código Blockly

**Casos de Uso Principais:**
1. **Fazer Cadastro/Login:** Autenticação no sistema
2. **Entrar em Turma:** Aluno se associa a turma via código
3. **Resolver Desafio:** Fluxo completo de programação e validação
4. **Ver Ranking:** Visualizar classificação de alunos
5. **Criar Turma:** Professor cria e gerencia turmas
6. **Gerar Relatórios:** Professor analisa desempenho
7. **Acessar Recursos Pedagógicos:** Professor obtém materiais de apoio
8. **Ajustar Acessibilidade:** Usuário personaliza interface

---

### 3.1.2 Diagrama de Classes (Modelo de Domínio)

```
┌────────────────────────┐
│      Usuario           │
├────────────────────────┤
│ - Id: int              │
│ - Nome: string         │
│ - Email: string        │
│ - SenhaHash: string    │
│ - Tipo: TipoUsuario    │
│ - DataCriacao: DateTime│
├────────────────────────┤
│ + Autenticar()         │
│ + AlterarSenha()       │
└───────────┬────────────┘
            │
            │ <<enumeration>>
            │ TipoUsuario
            │ {Professor, Aluno}
            │
     ┌──────┴──────┐
     │             │
┌────▼─────────┐ ┌▼──────────────────┐
│   Aluno      │ │    Professor      │
├──────────────┤ ├───────────────────┤
│ - Avatar:str │ │                   │
│ - Nivel: int │ │                   │
│ - Pontos:int │ │                   │
├──────────────┤ ├───────────────────┤
│+EscolherAvatar()│+CriarTurma()     │
│+SubirNivel() │ │+GerarRelatorio()  │
└──────┬───────┘ └─────┬─────────────┘
       │               │
       │ 0..*          │ 1..*
       │   participa   │ cria
       │               │
       │         ┌─────▼─────────────────┐
       │         │       Turma           │
       │         ├───────────────────────┤
       │         │ - Id: int             │
       └─────────┤ - Nome: string        │
                 │ - Codigo: string (6)  │
                 │ - ProfessorId: int    │
                 │ - DataCriacao: Date   │
                 ├───────────────────────┤
                 │ + GerarCodigo()       │
                 │ + AdicionarAluno()    │
                 │ + RemoverAluno()      │
                 └───────────────────────┘


┌─────────────────────────────┐
│         Desafio             │
├─────────────────────────────┤
│ - Id: int                   │
│ - Titulo: string            │
│ - Descricao: string         │
│ - Ordem: int                │
│ - GridInicial: string (JSON)│
│ - ObjetivoX: int            │
│ - ObjetivoY: int            │
│ - Pontos: int               │
│ - Dificuldade: string       │
├─────────────────────────────┤
│ + ValidarSolucao()          │
│ + ObterProximo()            │
└──────────────┬──────────────┘
               │
               │ 1
               │ realiza
               │ 0..*
         ┌─────▼────────────────────┐
         │   ProgressoDesafio       │
         ├──────────────────────────┤
         │ - Id: int                │
         │ - AlunoId: int           │
         │ - DesafioId: int         │
         │ - Completado: bool       │
         │ - Solucao: string (XML)  │
         │ - DataConclusao: DateTime│
         │ - Tentativas: int        │
         ├──────────────────────────┤
         │ + MarcarCompleto()       │
         │ + SalvarSolucao()        │
         └────────┬─────────────────┘
                  │
                  │ pertence a
                  │ 0..*
            ┌─────▼─────────┐
            │     Aluno     │
            └───────────────┘


┌────────────────────────┐
│        Badge           │
├────────────────────────┤
│ - Id: int              │
│ - Nome: string         │
│ - Descricao: string    │
│ - Icone: string        │
│ - Criterio: string     │
├────────────────────────┤
│ + VerificarConquista() │
└─────────┬──────────────┘
          │
          │ pode conquistar
          │ 0..*
    ┌─────▼─────────────────┐
    │    AlunoBadge         │
    ├───────────────────────┤
    │ - Id: int             │
    │ - AlunoId: int        │
    │ - BadgeId: int        │
    │ - DataConquista: Date │
    └───────────────────────┘


┌─────────────────────────────┐
│   PreferenciaAcessibilidade │
├─────────────────────────────┤
│ - UserId: int               │
│ - AltoContraste: bool       │
│ - TamanhoFonte: int (0-3)   │
│ - NarracaoAtiva: bool       │
├─────────────────────────────┤
│ + Salvar()                  │
│ + Carregar()                │
└─────────────────────────────┘
```

**Relacionamentos:**
- **Usuario** ◄──┤►── **Aluno** / **Professor** (herança/especialização)
- **Aluno** ──< participa >── **Turma** (muitos-para-muitos)
- **Professor** ──< cria >── **Turma** (um-para-muitos)
- **Aluno** ──< realiza >── **ProgressoDesafio** (um-para-muitos)
- **Desafio** ──< referenciado >── **ProgressoDesafio** (um-para-muitos)
- **Aluno** ──< conquista >── **AlunoBadge** (um-para-muitos)
- **Badge** ──< referenciado >── **AlunoBadge** (um-para-muitos)

---

### 3.1.3 Diagrama de Sequência - Resolver Desafio

```
Aluno          Frontend (Vue)      Backend (API)      Database       Sistema Execução
  │                  │                   │                │                 │
  │  Clica desafio   │                   │                │                 │
  ├─────────────────>│                   │                │                 │
  │                  │ GET /Desafio/{id} │                │                 │
  │                  ├──────────────────>│                │                 │
  │                  │                   │ SELECT desafio │                 │
  │                  │                   ├───────────────>│                 │
  │                  │                   │<───────────────┤                 │
  │                  │<──────────────────┤                │                 │
  │                  │ { desafio, grid } │                │                 │
  │                  │                   │                │                 │
  │<─────────────────┤                   │                │                 │
  │ Exibe workspace  │                   │                │                 │
  │   Blockly        │                   │                │                 │
  │                  │                   │                │                 │
  │ Arrasta blocos   │                   │                │                 │
  ├─────────────────>│                   │                │                 │
  │                  │ (Blockly.js)      │                │                 │
  │                  │ Monta XML         │                │                 │
  │                  │                   │                │                 │
  │ Clica Executar   │                   │                │                 │
  ├─────────────────>│                   │                │                 │
  │                  │ Gera código JS    │                │                 │
  │                  ├──────────────────────────────────────────────────────>│
  │                  │                   │                │  Interpreta e   │
  │                  │                   │                │  executa comandos│
  │                  │                   │                │  (mover, virar) │
  │                  │                   │                │                 │
  │                  │<──────────────────────────────────────────────────────┤
  │                  │ { sucesso: true/false, posicaoFinal }                 │
  │<─────────────────┤                   │                │                 │
  │  Animação robô   │                   │                │                 │
  │                  │                   │                │                 │
  │                  │ POST /Desafio/{id}/validar        │                 │
  │                  │ { solucaoXML }    │                │                 │
  │                  ├──────────────────>│                │                 │
  │                  │                   │ Valida objetivo│                 │
  │                  │                   │ (x, y corretos)│                 │
  │                  │                   │                │                 │
  │                  │                   │ INSERT/UPDATE  │                 │
  │                  │                   │ ProgressoDesafio│                │
  │                  │                   ├───────────────>│                 │
  │                  │                   │                │                 │
  │                  │                   │ UPDATE Aluno   │                 │
  │                  │                   │ (pontos += N)  │                 │
  │                  │                   ├───────────────>│                 │
  │                  │                   │                │                 │
  │                  │                   │ Verifica badges│                 │
  │                  │                   │ INSERT AlunoBadge│               │
  │                  │                   ├───────────────>│                 │
  │                  │                   │<───────────────┤                 │
  │                  │                   │                │                 │
  │                  │<──────────────────┤                │                 │
  │                  │ { sucesso, pontos, novoBadge }     │                 │
  │<─────────────────┤                   │                │                 │
  │ Modal Parabéns!  │                   │                │                 │
  │ +150 pontos      │                   │                │                 │
  │ Badge conquistado│                   │                │                 │
  │                  │                   │                │                 │
```

**Fluxo:**
1. Aluno seleciona desafio
2. Frontend carrega dados do desafio e grid inicial
3. Aluno programa com Blockly (arrastar/soltar blocos)
4. Ao executar, Sistema de Execução interpreta comandos e anima robô
5. Frontend valida visualmente se objetivo foi atingido
6. Backend valida solução e atualiza banco de dados
7. Sistema calcula pontos, badges, nível
8. Feedback visual é exibido ao aluno

---

### 3.1.4 Diagrama de Sequência - Criar Turma

```
Professor    Frontend (Vue)      Backend (API)      Database
    │              │                   │                │
    │ Clica "Criar │                   │                │
    │  Nova Turma" │                   │                │
    ├─────────────>│                   │                │
    │              │ Exibe modal       │                │
    │<─────────────┤                   │                │
    │              │                   │                │
    │ Preenche nome│                   │                │
    │ "6º Ano A"   │                   │                │
    ├─────────────>│                   │                │
    │              │                   │                │
    │ Clica Criar  │                   │                │
    ├─────────────>│                   │                │
    │              │ POST /Turma       │                │
    │              │ { nome: "6º Ano A"│                │
    │              │   professorId: X }│                │
    │              ├──────────────────>│                │
    │              │                   │ Gera código    │
    │              │                   │ único (6 chars)│
    │              │                   │ Ex: "AB12CD"   │
    │              │                   │                │
    │              │                   │ Verifica       │
    │              │                   │ unicidade      │
    │              │                   ├───────────────>│
    │              │                   │ SELECT WHERE   │
    │              │                   │ codigo = "..."│
    │              │                   │<───────────────┤
    │              │                   │ (vazio = único)│
    │              │                   │                │
    │              │                   │ INSERT Turma   │
    │              │                   ├───────────────>│
    │              │                   │<───────────────┤
    │              │                   │                │
    │              │<──────────────────┤                │
    │              │ { id, nome, codigo: "AB12CD" }     │
    │              │                   │                │
    │<─────────────┤                   │                │
    │ Modal exibe: │                   │                │
    │ "Turma criada!│                  │                │
    │ Código: AB12CD"│                 │                │
    │              │                   │                │
    │ Após 2s      │                   │                │
    │ fecha modal  │                   │                │
    │              │ GET /Turma/minhas │                │
    │              ├──────────────────>│                │
    │              │                   │ SELECT turmas  │
    │              │                   │ WHERE profId=X │
    │              │                   ├───────────────>│
    │              │                   │<───────────────┤
    │              │<──────────────────┤                │
    │              │ [lista atualizada]│                │
    │<─────────────┤                   │                │
    │ Lista de turmas│                 │                │
    │ atualizada    │                  │                │
```

---

### 3.1.5 Fluxograma - Validação de Solução de Desafio

```
         ┌─────────┐
         │ INÍCIO  │
         └────┬────┘
              │
              ▼
    ┌─────────────────────┐
    │ Aluno clica         │
    │ "Executar Código"   │
    └──────────┬──────────┘
               │
               ▼
    ┌──────────────────────┐
    │ Blockly gera código  │
    │ JavaScript a partir  │
    │ dos blocos           │
    └──────────┬───────────┘
               │
               ▼
    ┌──────────────────────┐
    │ Sistema de Execução  │
    │ interpreta comandos  │
    └──────────┬───────────┘
               │
               ▼
    ╔══════════════════════╗
    ║ Executar próximo     ║
    ║ comando da fila      ║
    ╚══════════╦═══════════╝
               ║
               ▼
      ┌────────────────┐
      │ Comando é       │ ─── SIM ──> ┌─────────────┐
      │ "mover"?        │             │ Calcular    │
      └────────┬───────┘              │ nova posição│
               │ NÃO                   │ baseado na  │
               ▼                       │ direção     │
      ┌────────────────┐               └──────┬──────┘
      │ Comando é       │                      │
      │ "virar"?        │ ─── SIM ──> ┌────────▼──────┐
      └────────┬───────┘              │ Validar se    │
               │ NÃO                   │ nova posição  │
               ▼                       │ tem parede    │
      ┌────────────────┐               └───────┬───────┘
      │ Comando é       │                       │
      │ "repetir"?      │ ─── SIM ──>          ◆ Tem parede?
      └────────┬───────┘                      / \
               │ NÃO                     SIM /   \ NÃO
               ▼                            /     \
      ┌────────────────┐                  ▼       ▼
      │ Comando         │          ┌─────────┐  ┌──────────┐
      │ condicional?    │          │ ERRO:   │  │ Mover    │
      └────────┬───────┘           │ Parede  │  │ robô para│
               │                   │ no      │  │ nova     │
               ▼                   │ caminho │  │ posição  │
         ┌──────────┐              └────┬────┘  └────┬─────┘
         │ Próximo  │                   │            │
         │ comando  │                   │            │
         └──────────┘                   │            │
               │                        │            │
               │ ◄──────────────────────┴────────────┘
               │
               ▼
         ◆ Há mais comandos?
        / \
  SIM  /   \  NÃO
      /     \
     ▼       ▼
  (volta)  ┌────────────────────┐
           │ Animação concluída │
           └──────────┬─────────┘
                      │
                      ▼
            ◆ Posição final == Objetivo?
           / \
     SIM  /   \  NÃO
         /     \
        ▼       ▼
  ┌──────────┐  ┌──────────────┐
  │ SUCESSO! │  │ FALHA!       │
  │          │  │ Tente        │
  │ Enviar   │  │ novamente    │
  │ para API │  └──────┬───────┘
  └────┬─────┘         │
       │               │
       ▼               ▼
  ┌──────────────────┐  ┌─────────┐
  │ API valida e     │  │  FIM    │
  │ salva progresso  │  └─────────┘
  └────┬─────────────┘
       │
       ▼
  ┌──────────────────┐
  │ Atualiza pontos, │
  │ badges, nível    │
  └────┬─────────────┘
       │
       ▼
  ┌──────────────────┐
  │ Exibe modal      │
  │ "Parabéns!"      │
  └────┬─────────────┘
       │
       ▼
  ┌─────────┐
  │  FIM    │
  └─────────┘
```

---

### 3.1.6 Diagrama BPMN - Processo de Acompanhamento do Professor

```
┌─────────────────────────────────────────────────────────────────────────┐
│                    ACOMPANHAMENTO DE TURMA - PROFESSOR                   │
└─────────────────────────────────────────────────────────────────────────┘

    ⚫ ──────> [ Acessar Dashboard ] ──────> ◆ Selecionar Ação
   INÍCIO                                   │
                                            │
                  ┌─────────────────────────┼──────────────────────────┐
                  │                         │                          │
                  ▼                         ▼                          ▼
          [ Criar Nova Turma ]    [ Ver Detalhes Turma ]    [ Acessar Recursos ]
                  │                         │                          │
                  ▼                         ▼                          │
          [ Gerar Código  ]         [ Visualizar Alunos ]              │
          [ Compartilhar  ]                 │                          │
                  │                         ▼                          │
                  │               ◆ Precisa analisar                   │
                  │              / desempenho?                          │
                  │             /                                       │
                  │       SIM  /  \ NÃO                                 │
                  │           /    \                                    │
                  │          ▼      ▼                                   │
                  │  [ Gerar     ]  │                                   │
                  │  [ Relatório ]  │                                   │
                  │         │       │                                   │
                  │         ▼       │                                   │
                  │  ┌──────────────────────┐                           │
                  │  │ Sistema calcula:     │                           │
                  │  │ - Média de pontos    │                           │
                  │  │ - Progresso por      │                           │
                  │  │   desafio            │                           │
                  │  │ - Alunos com         │                           │
                  │  │   dificuldades       │                           │
                  │  └─────────┬────────────┘                           │
                  │            │                                        │
                  │            ▼                                        │
                  │  [ Visualizar Gráficos ]                            │
                  │            │                                        │
                  │            ▼                                        │
                  │  ◆ Identificou aluno                                │
                  │ / com dificuldade?                                  │
                  │/                                                    │
            SIM  /  \ NÃO                                               │
                /    \                                                  │
               ▼      ▼                                                 │
     [ Planejar     ] │                                                 │
     [ Intervenção  ] │                                                 │
     [ Pedagógica   ] │                                                 ▼
               │      │                                         [ Visualizar  ]
               │      │                                         [ Narrativas  ]
               │      │                                                 │
               │      │                                                 ▼
               │      │                                         [ Baixar      ]
               │      │                                         [ Atividades  ]
               │      │                                         [ Desplugadas ]
               │      │                                                 │
               │      │                                                 ▼
               │      │                                         [ Ver BNCC    ]
               │      │                                         [ Mapeamento  ]
               │      │                                                 │
               └──────┴─────────────────────────────────────────────────┘
                                            │
                                            ▼
                                    ◆ Finalizar Sessão?
                                   /
                             SIM  /  \ NÃO
                                 /    \
                                ▼      (volta ao início)
                            [ Logout ]
                                │
                                ▼
                               ⚫
                              FIM
```

**Elementos do BPMN:**
- **⚫ Eventos:** Início/Fim
- **[ Tarefas ]:** Ações executadas pelo usuário ou sistema
- **◆ Gateways:** Decisões/bifurcações no processo
- **→ Fluxo de sequência:** Ordem das atividades

---

## 3.2 Diagrama de Entidade-Relacionamento (DER)

```
┌─────────────────────────────────────────────────────────────────────────┐
│                    MODELO ENTIDADE-RELACIONAMENTO                        │
└─────────────────────────────────────────────────────────────────────────┘


    ┌─────────────────────────┐
    │       USUARIO           │
    ├─────────────────────────┤
    │ PK  Id                  │
    │     Nome                │
    │ UK  Email               │
    │     SenhaHash           │
    │     Tipo (enum)         │──────┐
    │     DataCriacao         │      │ Herança
    └────────────┬────────────┘      │ (TPH - Table Per Hierarchy)
                 │                   │
                 │                   │
       ┌─────────┴──────────┐        │
       │                    │        │
       ▼                    ▼        │
┌──────────────┐     ┌──────────────┐│
│    ALUNO     │     │  PROFESSOR   ││◄────────────────┐
│  (Tipo=1)    │     │  (Tipo=2)    ││                 │
├──────────────┤     └──────────────┘│                 │
│ Avatar       │                     │                 │
│ Nivel        │                     │                 │
│ PontosTotal  │                     │                 │
└──────┬───────┘                     │                 │
       │                             │                 │
       │ 1                           │ 1               │
       │                             │                 │
       │ participa                   │ cria            │
       │                             │                 │
       │ N                           │ N               │
       │                             │                 │
       ▼                             ▼                 │
┌──────────────────────┐      ┌─────────────────────┐ │
│   ALUNO_TURMA        │      │       TURMA         │ │
│  (Tabela Associativa)│      ├─────────────────────┤ │
├──────────────────────┤      │ PK  Id              │ │
│ PK  Id               │      │     Nome            │ │
│ FK  AlunoId          │──┐   │ UK  Codigo (6 char) │ │
│ FK  TurmaId          │──┼──>│ FK  ProfessorId     │─┘
│     DataEntrada      │  │   │     DataCriacao     │
└──────────────────────┘  │   └─────────────────────┘
                          │
                          │
       ┌──────────────────┘
       │
       │ 1
       ▼
┌──────────────────────────┐         ┌─────────────────────┐
│   PROGRESSO_DESAFIO      │   N     │      DESAFIO        │
├──────────────────────────┤   ────> ├─────────────────────┤
│ PK  Id                   │    1    │ PK  Id              │
│ FK  AlunoId              │         │     Titulo          │
│ FK  DesafioId            │<────────│     Descricao       │
│     Completado (bool)    │         │     Ordem (1-10)    │
│     SolucaoXML (texto)   │         │     GridInicial(JSON│
│     DataConclusao        │         │     ObjetivoX       │
│     NumeroTentativas     │         │     ObjetivoY       │
└──────────────────────────┘         │     Pontos          │
                                     │     Dificuldade     │
                                     └─────────────────────┘


┌──────────────────────────┐         ┌─────────────────────┐
│      ALUNO_BADGE         │   N     │       BADGE         │
│  (Tabela Associativa)    │   ────> ├─────────────────────┤
├──────────────────────────┤    1    │ PK  Id              │
│ PK  Id                   │         │     Nome            │
│ FK  AlunoId              │<────────│     Descricao       │
│ FK  BadgeId              │         │     Icone           │
│     DataConquista        │         │     Criterio        │
└──────────────────────────┘         │     (ex: "3_desafios│
       │                             └─────────────────────┘
       │ N
       │
       │ pertence a
       │
       │ 1
       ▼
┌──────────────────────────┐
│        ALUNO             │
│   (referência acima)     │
└──────────────────────────┘
```

**Cardinalidades:**
- **USUARIO (1) ──< cria >── (N) TURMA**
  - Um professor cria várias turmas
  - Uma turma pertence a um professor

- **ALUNO (N) ──< participa >── (N) TURMA**
  - Um aluno pode estar em várias turmas
  - Uma turma tem vários alunos
  - Implementado via tabela associativa ALUNO_TURMA

- **ALUNO (1) ──< realiza >── (N) PROGRESSO_DESAFIO**
  - Um aluno tem vários progressos de desafio
  - Um progresso pertence a um aluno

- **DESAFIO (1) ──< referenciado >── (N) PROGRESSO_DESAFIO**
  - Um desafio pode ter vários progressos (de diferentes alunos)
  - Um progresso refere-se a um desafio específico

- **ALUNO (1) ──< conquista >── (N) ALUNO_BADGE**
  - Um aluno pode conquistar vários badges
  - Uma conquista pertence a um aluno

- **BADGE (1) ──< referenciado >── (N) ALUNO_BADGE**
  - Um badge pode ser conquistado por vários alunos
  - Uma conquista refere-se a um badge específico

**Chaves:**
- **PK (Primary Key):** Chave primária, identificador único
- **FK (Foreign Key):** Chave estrangeira, referência a outra tabela
- **UK (Unique Key):** Chave única, valor não pode ser duplicado

---

## 3.3 Modelo Relacional (Schema SQL Simplificado)

```sql
-- Tabela principal de usuários (TPH: Table Per Hierarchy)
CREATE TABLE Usuarios (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    SenhaHash TEXT NOT NULL,
    Tipo TEXT NOT NULL CHECK(Tipo IN ('Aluno', 'Professor')),
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,

    -- Campos específicos de Aluno (nullable para Professores)
    Avatar TEXT,
    Nivel INTEGER DEFAULT 1,
    PontosTotal INTEGER DEFAULT 0
);

CREATE INDEX idx_usuarios_email ON Usuarios(Email);
CREATE INDEX idx_usuarios_tipo ON Usuarios(Tipo);


-- Tabela de Turmas
CREATE TABLE Turmas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Codigo TEXT UNIQUE NOT NULL CHECK(LENGTH(Codigo) = 6),
    ProfessorId INTEGER NOT NULL,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (ProfessorId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

CREATE INDEX idx_turmas_codigo ON Turmas(Codigo);
CREATE INDEX idx_turmas_professor ON Turmas(ProfessorId);


-- Tabela associativa Aluno-Turma (N:N)
CREATE TABLE AlunoTurma (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AlunoId INTEGER NOT NULL,
    TurmaId INTEGER NOT NULL,
    DataEntrada DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (AlunoId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (TurmaId) REFERENCES Turmas(Id) ON DELETE CASCADE,

    UNIQUE(AlunoId, TurmaId) -- Aluno não pode entrar 2x na mesma turma
);

CREATE INDEX idx_aluno_turma_aluno ON AlunoTurma(AlunoId);
CREATE INDEX idx_aluno_turma_turma ON AlunoTurma(TurmaId);


-- Tabela de Desafios
CREATE TABLE Desafios (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Titulo TEXT NOT NULL,
    Descricao TEXT NOT NULL,
    Ordem INTEGER UNIQUE NOT NULL CHECK(Ordem BETWEEN 1 AND 10),
    GridInicial TEXT NOT NULL, -- JSON string
    ObjetivoX INTEGER NOT NULL,
    ObjetivoY INTEGER NOT NULL,
    Pontos INTEGER NOT NULL CHECK(Pontos > 0),
    Dificuldade TEXT NOT NULL CHECK(Dificuldade IN ('Fácil', 'Médio', 'Difícil'))
);

CREATE INDEX idx_desafios_ordem ON Desafios(Ordem);


-- Tabela de Progresso do Aluno nos Desafios
CREATE TABLE ProgressoDesafios (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AlunoId INTEGER NOT NULL,
    DesafioId INTEGER NOT NULL,
    Completado INTEGER DEFAULT 0 CHECK(Completado IN (0, 1)), -- Boolean
    SolucaoXML TEXT, -- Blocos Blockly em formato XML
    DataConclusao DATETIME,
    NumeroTentativas INTEGER DEFAULT 1,

    FOREIGN KEY (AlunoId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (DesafioId) REFERENCES Desafios(Id) ON DELETE CASCADE,

    UNIQUE(AlunoId, DesafioId) -- Aluno tem apenas um progresso por desafio
);

CREATE INDEX idx_progresso_aluno ON ProgressoDesafios(AlunoId);
CREATE INDEX idx_progresso_desafio ON ProgressoDesafios(DesafioId);
CREATE INDEX idx_progresso_completado ON ProgressoDesafios(Completado);


-- Tabela de Badges
CREATE TABLE Badges (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Descricao TEXT NOT NULL,
    Icone TEXT NOT NULL, -- Emoji ou caminho de imagem
    Criterio TEXT NOT NULL -- Ex: "3_desafios", "7_desafios", "10_desafios"
);


-- Tabela associativa Aluno-Badge (N:N)
CREATE TABLE AlunoBadge (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AlunoId INTEGER NOT NULL,
    BadgeId INTEGER NOT NULL,
    DataConquista DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (AlunoId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (BadgeId) REFERENCES Badges(Id) ON DELETE CASCADE,

    UNIQUE(AlunoId, BadgeId) -- Aluno não pode conquistar o mesmo badge 2x
);

CREATE INDEX idx_aluno_badge_aluno ON AlunoBadge(AlunoId);
CREATE INDEX idx_aluno_badge_badge ON AlunoBadge(BadgeId);
```

**Decisões de Design:**
1. **Table Per Hierarchy (TPH):** Aluno e Professor compartilham tabela Usuarios com coluna discriminadora "Tipo"
2. **Soft Delete:** Uso de ON DELETE CASCADE para manter integridade referencial
3. **Constraints:** CHECK constraints para validar valores (ex: Código tem 6 chars, Ordem entre 1-10)
4. **Indexes:** Criados em colunas frequentemente consultadas (email, tipo, código turma, etc.)
5. **UNIQUE Constraints:** Previnem duplicação de dados (email, código turma, aluno-turma)

---

**Documento criado em:** 29/11/2025
**Última atualização:** 29/11/2025
**Versão:** 1.0
