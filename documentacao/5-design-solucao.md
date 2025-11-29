# 5. DESIGN DA SOLUÇÃO

## 5.1 Arquitetura do Sistema

### 5.1.1 Visão Geral da Arquitetura

O CodeSchool adota uma **arquitetura cliente-servidor de três camadas (3-tier)** com separação clara de responsabilidades:

```
┌────────────────────────────────────────────────────────────────────┐
│                        CAMADA DE APRESENTAÇÃO                       │
│                           (Frontend Web)                            │
│                                                                     │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐             │
│  │   Vue.js 3   │  │   Blockly    │  │  Chart.js    │             │
│  │ Composition  │  │  (Visual     │  │ (Gráficos)   │             │
│  │     API      │  │ Programming) │  │              │             │
│  └──────────────┘  └──────────────┘  └──────────────┘             │
│                                                                     │
│  ┌──────────────────────────────────────────────────────┐          │
│  │  Pinia (State Management) + Vue Router               │          │
│  └──────────────────────────────────────────────────────┘          │
└─────────────────────────────┬───────────────────────────────────────┘
                              │
                              │ HTTP/HTTPS (REST API)
                              │ JSON
                              │
┌─────────────────────────────▼───────────────────────────────────────┐
│                      CAMADA DE APLICAÇÃO                             │
│                         (Backend API)                                │
│                                                                      │
│  ┌──────────────────────────────────────────────────────────┐       │
│  │              ASP.NET Core 8.0 Web API                     │       │
│  └──────────────────────────────────────────────────────────┘       │
│                                                                      │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐                 │
│  │ Controllers │  │  Services   │  │ Middleware  │                 │
│  │  (Rotas)    │  │  (Lógica)   │  │   (Auth)    │                 │
│  └─────────────┘  └─────────────┘  └─────────────┘                 │
│                                                                      │
│  ┌──────────────────────────────────────────────────────────┐       │
│  │      Entity Framework Core (ORM)                          │       │
│  └──────────────────────────────────────────────────────────┘       │
└─────────────────────────────┬────────────────────────────────────────┘
                              │
                              │ LINQ Queries
                              │ ADO.NET
                              │
┌─────────────────────────────▼────────────────────────────────────────┐
│                       CAMADA DE DADOS                                │
│                      (Banco de Dados)                                │
│                                                                      │
│  ┌──────────────────────────────────────────────────────────┐       │
│  │                    SQLite Database                        │       │
│  │                  (codeschool.db)                          │       │
│  └──────────────────────────────────────────────────────────┘       │
│                                                                      │
│  Tabelas: Usuarios, Turmas, AlunoTurma, Desafios,                   │
│           ProgressoDesafios, Badges, AlunoBadge                      │
└──────────────────────────────────────────────────────────────────────┘
```

**Características da Arquitetura:**
- **Separação de Responsabilidades:** Frontend (UI), Backend (lógica de negócio), Database (persistência)
- **Stateless API:** Backend não mantém estado de sessão, usa JWT para autenticação
- **RESTful:** Endpoints seguem convenções REST (GET, POST, PUT, DELETE)
- **SPA (Single Page Application):** Frontend carrega uma vez, navegação via client-side routing

---

### 5.1.2 Diagrama de Arquitetura de Software

```
┌─────────────────────────────────────────────────────────────────────────┐
│                           USUÁRIO FINAL                                  │
│                    (Navegador Web - Chrome, Firefox, Edge)               │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │
                                 │ HTTP/HTTPS Request
                                 │
┌────────────────────────────────▼─────────────────────────────────────────┐
│                     FRONTEND (Vue.js SPA)                                │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                        Vue Router                                    │ │
│ │  /          → LoginView.vue                                          │ │
│ │  /dashboard → DashboardView.vue                                      │ │
│ │  /desafio/3 → DesafioView.vue                                        │ │
│ │  /professor → ProfessorDashboardView.vue                             │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌────────────────┐  ┌────────────────┐  ┌──────────────────┐            │
│ │ Views (Pages)  │  │   Components   │  │   Composables    │            │
│ │ - LoginView    │  │ - RankingCard  │  │ - useKeyboard    │            │
│ │ - DashboardView│  │ - DesafioCard  │  │ - useNarracao    │            │
│ │ - DesafioView  │  │ - AccessMenu   │  │ - useBlockly     │            │
│ └────────────────┘  └────────────────┘  └──────────────────┘            │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                    Pinia Stores (State)                              │ │
│ │  - userStore (usuário logado, token)                                 │ │
│ │  - accessibilityStore (contraste, fonte, narração)                   │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                    API Service (axios)                               │ │
│ │  - authService.login(email, senha)                                   │ │
│ │  - desafioService.obter(id)                                          │ │
│ │  - turmaService.criarTurma(nome)                                     │ │
│ │  - rankingService.obterRankingTurma(turmaId)                         │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │              Bibliotecas de Terceiros                                │ │
│ │  - Blockly (Google) - Programação visual                             │ │
│ │  - Chart.js - Gráficos de relatórios                                 │ │
│ │  - Web Speech API - Narração de acessibilidade                       │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │
                                 │ REST API Calls
                                 │ axios.post('/api/Auth/login', {email, senha})
                                 │ Authorization: Bearer <JWT>
                                 │
┌────────────────────────────────▼─────────────────────────────────────────┐
│                   BACKEND (ASP.NET Core 8.0 API)                         │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                     API Controllers                                  │ │
│ │  [Route("api/[controller]")]                                         │ │
│ │  - AuthController        : Login, Cadastro                           │ │
│ │  - DesafioController     : CRUD Desafios, Validar Solução            │ │
│ │  - TurmaController       : Criar, Listar, Entrar, Detalhes           │ │
│ │  - ProgressoController   : Salvar, Atualizar Progresso               │ │
│ │  - RankingController     : Ranking Turma, Geral, Minha Posição       │ │
│ │  - RelatorioController   : Relatórios de Turma e Aluno               │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                      Middleware Pipeline                             │ │
│ │  1. Exception Handling                                               │ │
│ │  2. Authentication (JWT Bearer Token)                                │ │
│ │  3. Authorization ([Authorize(Roles = "Aluno")])                     │ │
│ │  4. CORS Policy (permitir frontend localhost:5173)                   │ │
│ │  5. Routing                                                          │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                    Models (Entidades)                                │ │
│ │  - Usuario { Id, Nome, Email, SenhaHash, Tipo }                      │ │
│ │  - Aluno : Usuario { Avatar, Nivel, PontosTotal }                    │ │
│ │  - Professor : Usuario                                               │ │
│ │  - Turma { Id, Nome, Codigo, ProfessorId }                           │ │
│ │  - Desafio { Id, Titulo, Descricao, Ordem, Grid, Pontos }            │ │
│ │  - ProgressoDesafio { AlunoId, DesafioId, Completado, Solucao }      │ │
│ │  - Badge { Id, Nome, Criterio }                                      │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                   AppDbContext (EF Core)                             │ │
│ │  DbSet<Usuario>, DbSet<Turma>, DbSet<Desafio>,                       │ │
│ │  DbSet<ProgressoDesafio>, DbSet<Badge>, DbSet<AlunoTurma>            │ │
│ │                                                                      │ │
│ │  OnModelCreating() → Configurações de relacionamentos                │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
│                                                                          │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │                    Serviços Auxiliares                               │ │
│ │  - TokenService : Gerar/validar JWT                                  │ │
│ │  - PasswordService : Hash BCrypt                                     │ │
│ │  - CodigoService : Gerar códigos únicos de turma                     │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │
                                 │ LINQ to Entities
                                 │ EF Core Queries
                                 │
┌────────────────────────────────▼─────────────────────────────────────────┐
│                    BANCO DE DADOS (SQLite)                               │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │  Arquivo: codeschool.db                                              │ │
│ │                                                                      │ │
│ │  Tabelas:                                                            │ │
│ │  - Usuarios (TPH: Aluno + Professor na mesma tabela)                 │ │
│ │  - Turmas                                                            │ │
│ │  - AlunoTurma (N:N)                                                  │ │
│ │  - Desafios                                                          │ │
│ │  - ProgressoDesafios                                                 │ │
│ │  - Badges                                                            │ │
│ │  - AlunoBadge (N:N)                                                  │ │
│ │                                                                      │ │
│ │  Migrations: Versionamento do schema                                 │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                    ARMAZENAMENTO LOCAL (Browser)                         │
│ ┌──────────────────────────────────────────────────────────────────────┐ │
│ │  localStorage:                                                       │ │
│ │  - token: "eyJhbGciOiJIUzI1NiIs..."                                  │ │
│ │  - usuario: { id, nome, email, tipo }                                │ │
│ │  - preferenciasAcessibilidade: { contraste, fonte, narracao }        │ │
│ └──────────────────────────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────────────────────┘
```

---

### 5.1.3 Fluxo de Dados - Exemplo: Resolver Desafio

```
┌──────────┐                                                      ┌──────────┐
│  Aluno   │                                                      │ Database │
└────┬─────┘                                                      └────┬─────┘
     │                                                                 │
     │ 1. Clica "Desafio 3"                                            │
     ├────────────────────────────────────────────┐                    │
     │                                            │                    │
     │                                     ┌──────▼──────┐             │
     │                                     │ Vue Router  │             │
     │                                     │ navega para │             │
     │                                     │ /desafio/3  │             │
     │                                     └──────┬──────┘             │
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │ DesafioView.vue │         │
     │                                     │ onMounted()     │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │                                            │ 2. GET /api/Desafio/3
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │ DesafioController│        │
     │                                     │ ObterPorId(3)   │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │                                            │ 3. LINQ Query      │
     │                                            ├───────────────────>│
     │                                            │ SELECT * FROM      │
     │                                            │ Desafios WHERE     │
     │                                            │ Id = 3             │
     │                                            │<───────────────────┤
     │                                            │ { Desafio data }   │
     │                                     ┌──────┴──────────┐         │
     │                                     │ Return 200 OK   │         │
     │                                     │ { desafio }     │         │
     │<────────────────────────────────────┤                 │         │
     │ 4. Recebe JSON do desafio           └─────────────────┘         │
     │                                                                 │
     │ 5. Renderiza workspace Blockly                                  │
     │    e grid visual                                                │
     │                                                                 │
     │ 6. Arrasta blocos (mover, repetir)                              │
     │                                                                 │
     │ 7. Clica "Executar"                                             │
     ├────────────────────────────────────────────┐                    │
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │ blocosCustomizados│        │
     │                                     │ .interpretador() │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │ Executa comandos│         │
     │                                     │ (mover, virar)  │         │
     │                                     │ Anima robô      │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │<────────────────────────────────────────────┘                   │
     │ 8. Vê animação do robô                                          │
     │                                                                 │
     │ 9. Robô chega ao objetivo                                       │
     ├────────────────────────────────────────────┐                    │
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │ validarSolucao()│         │
     │                                     │ POST /api/      │         │
     │                                     │ Desafio/3/      │         │
     │                                     │ validar         │         │
     │                                     │ { solucaoXML }  │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │                                     ┌──────▼──────────┐         │
     │                                     │DesafioController│         │
     │                                     │ValidarSolucao() │         │
     │                                     └──────┬──────────┘         │
     │                                            │                    │
     │                                            │ 10. INSERT/UPDATE  │
     │                                            │ ProgressoDesafio   │
     │                                            ├───────────────────>│
     │                                            │ Completado = true  │
     │                                            │                    │
     │                                            │ 11. UPDATE Usuario │
     │                                            │ PontosTotal += 100 │
     │                                            ├───────────────────>│
     │                                            │                    │
     │                                            │ 12. Verifica badges│
     │                                            │ (3 desafios?)      │
     │                                            ├───────────────────>│
     │                                            │ INSERT AlunoBadge  │
     │                                            │<───────────────────┤
     │                                            │                    │
     │                                     ┌──────┴──────────┐         │
     │                                     │ Return 200 OK   │         │
     │                                     │ { sucesso: true,│         │
     │                                     │   pontos: 100,  │         │
     │                                     │   novoBadge }   │         │
     │<────────────────────────────────────┤                 │         │
     │ 13. Recebe confirmação              └─────────────────┘         │
     │                                                                 │
     │ 14. Modal "Parabéns! +100 pts"                                  │
     │     "Badge conquistado!"                                        │
     │                                                                 │
     │ 15. Clica "Próximo Desafio"                                     │
     │     → Navega para /desafio/4                                    │
     │                                                                 │
```

---

## 5.2 Tecnologias Utilizadas

### 5.2.1 Frontend

| Tecnologia | Versão | Finalidade | Licença |
|------------|--------|------------|---------|
| **Vue.js** | 3.4.x | Framework progressivo JavaScript para construção de interfaces reativas | MIT |
| **Vue Router** | 4.x | Roteamento client-side para SPA | MIT |
| **Pinia** | 2.x | Gerenciamento de estado global (substituto do Vuex) | MIT |
| **Vite** | 5.x | Bundler e dev server ultra-rápido | MIT |
| **Blockly** | 10.x (Google) | Biblioteca de programação visual drag-and-drop | Apache 2.0 |
| **Chart.js** | 4.x | Biblioteca de gráficos para visualização de dados | MIT |
| **Axios** | 1.6.x | Cliente HTTP para requisições à API | MIT |
| **CSS3** | - | Estilização (Flexbox, Grid, Animations) | - |
| **Web Speech API** | - | Síntese de voz para narração de acessibilidade | W3C Standard |

**Estrutura de Pastas (Frontend):**
```
CodeSchool.Frontend/
├── public/
│   └── index.html
├── src/
│   ├── assets/
│   │   └── styles/
│   │       └── global.css
│   ├── components/
│   │   ├── AccessibilityMenu.vue
│   │   ├── RankingCard.vue
│   │   ├── DesafioCard.vue
│   │   └── ...
│   ├── composables/
│   │   ├── useKeyboardShortcuts.js
│   │   ├── useNarracao.js
│   │   └── useBlockly.js
│   ├── router/
│   │   └── index.js
│   ├── services/
│   │   ├── api.js
│   │   └── blocosCustomizados.js
│   ├── stores/
│   │   ├── user.js
│   │   └── accessibility.js
│   ├── views/
│   │   ├── LoginView.vue
│   │   ├── DashboardView.vue
│   │   ├── DesafioView.vue
│   │   ├── ProfessorDashboardView.vue
│   │   ├── TurmaDetalhesView.vue
│   │   ├── RelatorioTurmaView.vue
│   │   ├── RecursosPedagogicosView.vue
│   │   └── NarrativaView.vue
│   ├── App.vue
│   └── main.js
├── package.json
└── vite.config.js
```

---

### 5.2.2 Backend

| Tecnologia | Versão | Finalidade | Licença |
|------------|--------|------------|---------|
| **ASP.NET Core** | 8.0 | Framework web para construção de APIs RESTful | MIT |
| **C#** | 12.0 | Linguagem de programação orientada a objetos | MIT |
| **Entity Framework Core** | 8.0 | ORM (Object-Relational Mapping) para acesso a dados | MIT |
| **SQLite** | 3.x | Banco de dados relacional leve e embarcado | Public Domain |
| **BCrypt.Net** | 0.1.x | Biblioteca para hash seguro de senhas | MIT |
| **JWT (JSON Web Tokens)** | - | Autenticação stateless via tokens | RFC 7519 |
| **Microsoft.AspNetCore.Authentication.JwtBearer** | 8.0 | Middleware de autenticação JWT | MIT |
| **Swashbuckle (Swagger)** | 6.x | Documentação interativa da API | MIT |

**Estrutura de Pastas (Backend):**
```
CodeSchool.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── DesafioController.cs
│   ├── TurmaController.cs
│   ├── ProgressoController.cs
│   ├── RankingController.cs
│   └── RelatorioController.cs
├── Data/
│   ├── AppDbContext.cs
│   └── Migrations/
│       ├── 20250101_InitialCreate.cs
│       └── ...
├── Models/
│   ├── Usuario.cs
│   ├── Aluno.cs
│   ├── Professor.cs
│   ├── Turma.cs
│   ├── Desafio.cs
│   ├── ProgressoDesafio.cs
│   ├── Badge.cs
│   └── AlunoTurma.cs
├── DTOs/
│   ├── LoginDto.cs
│   ├── CadastroDto.cs
│   ├── CriarTurmaDto.cs
│   └── ...
├── Services/
│   ├── TokenService.cs
│   ├── PasswordService.cs
│   └── CodigoService.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
├── Program.cs
├── appsettings.json
└── CodeSchool.API.csproj
```

---

### 5.2.3 Banco de Dados

**SQLite 3.x**

**Características:**
- **Tipo:** Relacional (SQL)
- **Arquivo:** `codeschool.db` (local)
- **Tamanho:** ~5MB (estimado com 1000 usuários)
- **Portabilidade:** Funciona em Windows, Linux, macOS sem configuração
- **ACID:** Transações atômicas, consistentes, isoladas, duráveis

**Schema (resumido):**
```sql
Usuarios (Id, Nome, Email, SenhaHash, Tipo, Avatar, Nivel, PontosTotal)
Turmas (Id, Nome, Codigo, ProfessorId, DataCriacao)
AlunoTurma (Id, AlunoId, TurmaId, DataEntrada)
Desafios (Id, Titulo, Descricao, Ordem, GridInicial, ObjetivoX, ObjetivoY, Pontos)
ProgressoDesafios (Id, AlunoId, DesafioId, Completado, SolucaoXML, DataConclusao)
Badges (Id, Nome, Descricao, Icone, Criterio)
AlunoBadge (Id, AlunoId, BadgeId, DataConquista)
```

---

### 5.2.4 Ferramentas de Desenvolvimento

| Ferramenta | Finalidade |
|------------|------------|
| **Visual Studio 2022** | IDE para desenvolvimento backend (C# / .NET) |
| **Visual Studio Code** | Editor de código para frontend (Vue.js / JavaScript) |
| **Node.js 20.x** | Runtime JavaScript para executar Vite e ferramentas frontend |
| **npm / pnpm** | Gerenciador de pacotes JavaScript |
| **Git** | Controle de versão |
| **GitHub** | Hospedagem de repositório |
| **Postman** | Testes manuais de API REST |
| **DB Browser for SQLite** | Visualização e edição do banco SQLite |
| **Chrome DevTools** | Debug frontend, inspeção de elementos, performance |
| **axe DevTools** | Auditoria de acessibilidade WCAG |

---

## 5.3 Justificativa das Escolhas Tecnológicas

### 5.3.1 Por que Vue.js 3?

**Prós:**
- ✅ **Curva de aprendizado suave:** Sintaxe intuitiva, fácil para iniciantes
- ✅ **Performance excelente:** Virtual DOM otimizado, renderização reativa eficiente
- ✅ **Composition API:** Melhor organização de código, reutilização de lógica via composables
- ✅ **Ecossistema maduro:** Vue Router, Pinia, Vite integrados perfeitamente
- ✅ **Documentação de qualidade:** Guia oficial completo e didático
- ✅ **Tamanho pequeno:** ~34KB minified + gzipped (rápido de carregar)
- ✅ **Reatividade nativa:** Sistema de reatividade simplifica gestão de estado

**Comparação com alternativas:**
- **React:** Mais complexo (JSX, hooks), ecossistema fragmentado
- **Angular:** Curva de aprendizado mais íngreme, mais "pesado" (TypeScript obrigatório)
- **Svelte:** Menos maduro, menor ecossistema de bibliotecas

**Conclusão:** Vue.js 3 é ideal para projetos educacionais pela simplicidade e performance.

---

### 5.3.2 Por que Blockly (Google)?

**Prós:**
- ✅ **Padrão da indústria:** Usado em Scratch, Code.org, Tynker
- ✅ **Drag-and-drop visual:** Perfeito para iniciantes, sem necessidade de sintaxe
- ✅ **Altamente customizável:** Criação de blocos personalizados (mover, virar, loops)
- ✅ **Geração de código:** Pode exportar para JavaScript, Python, etc.
- ✅ **Acessível:** Suporte a teclado, leitores de tela (melhorado na v10+)
- ✅ **Open-source:** Licença Apache 2.0, gratuito
- ✅ **Documentação rica:** Guias, exemplos, playground

**Comparação com alternativas:**
- **Scratch Blocks:** Baseado em Blockly, mas mais limitado
- **Snap!:** Mais acadêmico, menos usado industrialmente
- **Bibliotecas próprias:** Requereriam meses de desenvolvimento

**Conclusão:** Blockly é a escolha óbvia para programação visual educacional.

---

### 5.3.3 Por que ASP.NET Core 8.0?

**Prós:**
- ✅ **Performance excepcional:** Um dos frameworks web mais rápidos (benchmark TechEmpower)
- ✅ **Cross-platform:** Roda em Windows, Linux, macOS
- ✅ **Tipagem forte:** C# previne muitos bugs em tempo de compilação
- ✅ **Entity Framework Core:** ORM maduro, migrations automáticas
- ✅ **Segurança integrada:** Middleware de autenticação/autorização robusto
- ✅ **Escalabilidade:** Async/await nativo, suporta milhares de requisições simultâneas
- ✅ **Ecossistema .NET:** NuGet com milhares de pacotes confiáveis
- ✅ **LTS (Long-Term Support):** .NET 8 tem suporte até 2026

**Comparação com alternativas:**
- **Node.js/Express:** Menos tipado, mais propenso a erros em runtime
- **Django (Python):** Mais lento, menos adequado para alta concorrência
- **Spring Boot (Java):** Verboso, curva de aprendizado maior

**Conclusão:** ASP.NET Core oferece melhor equilíbrio entre produtividade, performance e confiabilidade.

---

### 5.3.4 Por que SQLite?

**Prós:**
- ✅ **Zero configuração:** Não precisa de servidor de banco, é apenas um arquivo
- ✅ **Portabilidade:** Arquivo .db pode ser copiado entre sistemas
- ✅ **Suficiente para o escopo:** Suporta milhares de usuários sem problemas
- ✅ **Relacional completo:** SQL padrão, transações ACID
- ✅ **Leve:** Footprint mínimo, ideal para aplicações educacionais
- ✅ **Backup simples:** Copiar arquivo .db = backup completo

**Limitações conhecidas:**
- ❌ **Concorrência limitada:** Escritas bloqueiam banco inteiro (aceitável para <100 usuários simultâneos)
- ❌ **Não distribuído:** Um único arquivo (não problema para MVP)

**Comparação com alternativas:**
- **PostgreSQL:** Requer servidor, mais complexo de configurar
- **MySQL:** Idem, overhead desnecessário para o escopo
- **MongoDB (NoSQL):** Não relacional, perde benefícios de SQL (JOINs, constraints)

**Conclusão:** SQLite é perfeito para MVPs e aplicações educacionais com centenas de usuários.

---

### 5.3.5 Por que JWT para Autenticação?

**Prós:**
- ✅ **Stateless:** Backend não precisa armazenar sessões, escala melhor
- ✅ **Portável:** Token pode ser enviado em qualquer plataforma (web, mobile futuro)
- ✅ **Self-contained:** Token contém todas as informações (userId, role), reduz queries ao DB
- ✅ **Padrão da indústria:** RFC 7519, amplamente adotado
- ✅ **Seguro:** Assinado com chave secreta, não pode ser forjado

**Comparação com alternativas:**
- **Cookies de sessão:** Stateful, requer armazenamento no servidor
- **OAuth2:** Complexo demais para aplicação interna

**Conclusão:** JWT é ideal para APIs stateless modernas.

---

### 5.3.6 Por que Chart.js?

**Prós:**
- ✅ **Simples de usar:** Configuração via objetos JavaScript
- ✅ **Responsivo:** Gráficos se adaptam ao tamanho da tela
- ✅ **8 tipos de gráficos:** Barra, linha, pizza, radar, etc.
- ✅ **Customizável:** Cores, tooltips, legendas totalmente configuráveis
- ✅ **Open-source:** Licença MIT, gratuito
- ✅ **Leve:** ~200KB, performance excelente

**Comparação com alternativas:**
- **D3.js:** Muito complexo para gráficos simples
- **Google Charts:** Requer conexão com Google, menos privacidade
- **Recharts:** Específico para React

**Conclusão:** Chart.js é a melhor opção para gráficos educacionais simples e bonitos.

---

## 5.4 Padrões de Projeto Aplicados

### 5.4.1 Backend (ASP.NET Core)

**1. Repository Pattern (implícito via EF Core)**
```csharp
// DbContext age como Repository
public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    // EF Core fornece Add, Update, Remove, Find, etc.
}

// Uso nos Controllers
var turma = await _context.Turmas.FindAsync(id);
```

**2. Dependency Injection**
```csharp
// Program.cs - Registro de serviços
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PasswordService>();

// Controller - Injeção via construtor
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
}
```

**3. DTO (Data Transfer Object)**
```csharp
// Separação entre modelo de domínio e dados de API
public class LoginDto
{
    public string Email { get; set; }
    public string Senha { get; set; }
}

// Evita expor modelo completo (Usuario) na API
[HttpPost("login")]
public async Task<ActionResult> Login([FromBody] LoginDto dto)
{
    // Valida dto, não Usuario diretamente
}
```

**4. Middleware Pipeline**
```csharp
// Processamento em cadeia de requisições
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

**5. Service Layer**
```csharp
// Lógica de negócio separada dos Controllers
public class TokenService
{
    public string GerarToken(Usuario usuario)
    {
        // Lógica isolada, reutilizável, testável
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Tipo)
        };
        // ...
    }
}
```

---

### 5.4.2 Frontend (Vue.js)

**1. Component-Based Architecture**
```vue
<!-- Componentes reutilizáveis e isolados -->
<template>
  <RankingCard :turmas="minhasTurmas" />
  <DesafioCard v-for="desafio in desafios" :key="desafio.id" :desafio="desafio" />
</template>
```

**2. Composition API (Composables)**
```javascript
// Lógica reutilizável via composables
export function useKeyboardShortcuts() {
  onMounted(() => {
    window.addEventListener('keydown', handleKeydown)
  })

  onUnmounted(() => {
    window.removeEventListener('keydown', handleKeydown)
  })
}

// Uso em qualquer componente
import { useKeyboardShortcuts } from '@/composables/useKeyboardShortcuts'
useKeyboardShortcuts()
```

**3. Singleton Pattern (Stores Pinia)**
```javascript
// Store global único
export const useUserStore = defineStore('user', {
  state: () => ({
    usuario: null,
    token: null
  }),
  actions: {
    setUsuario(usuario, token) {
      this.usuario = usuario
      this.token = token
    }
  }
})

// Acesso de qualquer componente
const userStore = useUserStore()
```

**4. Observer Pattern (Reatividade Vue)**
```javascript
// ref cria observável
const pontos = ref(0)

// Watch observa mudanças
watch(pontos, (novoPontos, pontosAntigos) => {
  console.log(`Pontos mudaram de ${pontosAntigos} para ${novoPontos}`)
})
```

**5. Factory Pattern (Blockly Blocks)**
```javascript
// Função factory para criar blocos
function criarBlocoMovimento(tipo) {
  Blockly.Blocks[tipo] = {
    init: function() {
      this.appendDummyInput().appendField(tipo);
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
    }
  };
}

criarBlocoMovimento('mover');
criarBlocoMovimento('virar_direita');
```

---

## 5.5 Segurança

### 5.5.1 Medidas de Segurança Implementadas

**1. Autenticação JWT**
- ✅ Tokens assinados com chave secreta (256-bit)
- ✅ Expiração de 24 horas
- ✅ Validação em cada requisição protegida

**2. Hash de Senhas**
- ✅ BCrypt com salt automático (cost factor 12)
- ✅ Senhas nunca armazenadas em texto plano
- ✅ Impossível reverter hash para senha original

**3. Autorização Baseada em Roles**
```csharp
[Authorize(Roles = "Professor")]
public async Task<ActionResult> CriarTurma(...)
{
    // Só professores podem criar turmas
}
```

**4. Validação de Entrada**
- ✅ Frontend: validação de formulários (email válido, senha >= 6 chars)
- ✅ Backend: validação duplicada (never trust client)
- ✅ SQL Injection: prevenido por EF Core (queries parametrizadas)

**5. CORS Configurado**
```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

**6. HTTPS (Produção)**
- ✅ Comunicação criptografada em produção
- ✅ Previne man-in-the-middle attacks

---

### 5.5.2 Vulnerabilidades Mitigadas

| Vulnerabilidade (OWASP Top 10) | Mitigação |
|-------------------------------|-----------|
| **A01: Broken Access Control** | Autorização via `[Authorize(Roles)]`, validação de ownership (professor só vê suas turmas) |
| **A02: Cryptographic Failures** | BCrypt para senhas, JWT assinado, HTTPS em produção |
| **A03: Injection** | EF Core usa queries parametrizadas, validação de entrada |
| **A04: Insecure Design** | Autenticação stateless, separação de responsabilidades |
| **A05: Security Misconfiguration** | CORS restrito, exceções não expõem stack traces em produção |
| **A06: Vulnerable Components** | Dependências atualizadas (npm audit, NuGet) |
| **A07: Authentication Failures** | JWT com expiração, senhas com requisitos mínimos |
| **A08: Software Integrity Failures** | Uso de pacotes oficiais (Google Blockly, Microsoft EF Core) |
| **A09: Logging Failures** | Middleware de exception logging (futuro: Application Insights) |
| **A10: SSRF** | Não aplicável (sem requisições server-side a URLs externas) |

---

## 5.6 Performance e Escalabilidade

### 5.6.1 Otimizações de Performance

**Frontend:**
- ✅ **Code Splitting:** Vite automaticamente divide bundle por rota
- ✅ **Lazy Loading:** Componentes carregados sob demanda (`() => import()`)
- ✅ **Virtual Scrolling:** Listas grandes (ranking) renderizam apenas items visíveis (futuro)
- ✅ **Debounce:** Buscas e filtros têm delay de 300ms para reduzir requisições

**Backend:**
- ✅ **Async/Await:** Todas as operações de I/O são assíncronas
- ✅ **Índices no DB:** Colunas frequentemente consultadas têm índices (email, código turma)
- ✅ **Eager Loading:** `Include()` para evitar N+1 queries
```csharp
var turma = await _context.Turmas
    .Include(t => t.Alunos)
    .FirstOrDefaultAsync(t => t.Id == id);
// 1 query em vez de 1 + N
```
- ✅ **Paginação:** Endpoints de lista limitam resultados (TOP 100)

**Banco de Dados:**
- ✅ **Constraints:** UNIQUE constraints em email/código evitam duplicatas
- ✅ **Foreign Keys:** Integridade referencial sem queries extras

---

### 5.6.2 Escalabilidade

**Capacidade Atual (SQLite):**
- ✅ **Usuários simultâneos:** ~100 (limitado por escritas concorrentes)
- ✅ **Total de usuários:** 10.000+ (tamanho do DB não é problema)
- ✅ **Requisições por segundo:** ~500 (ASP.NET Core com async)

**Plano de Escalabilidade Futura:**
1. **Migrar para PostgreSQL:** Se > 100 usuários simultâneos
   - Suporta milhares de escritas concorrentes
   - Mudança trivial (apenas connection string)

2. **Adicionar Cache (Redis):**
   - Cachear rankings, progresso de alunos
   - Reduz queries ao DB em 70%

3. **CDN para Assets:**
   - Servir JS/CSS via CloudFlare ou AWS CloudFront
   - Latência < 50ms globalmente

4. **Load Balancer:**
   - Múltiplas instâncias do backend
   - Nginx balanceando carga

---

## 5.7 Testes (Planejamento Futuro)

### 5.7.1 Tipos de Testes

**Unit Tests:**
- `PasswordService.HashSenha()` → deve gerar hash BCrypt válido
- `TokenService.GerarToken()` → deve retornar JWT com claims corretos
- `CodigoService.GerarCodigo()` → deve gerar código alfanumérico de 6 chars

**Integration Tests:**
- `POST /api/Auth/login` → deve retornar token ao enviar credenciais válidas
- `POST /api/Turma` → deve criar turma e gerar código único

**E2E Tests (Cypress/Playwright):**
- Fluxo completo: Cadastro → Login → Resolver Desafio → Ver Ranking
- Acessibilidade: Navegação por teclado, contraste, narração

**Ferramentas:**
- **xUnit:** Testes unitários backend (.NET)
- **Cypress:** Testes E2E frontend
- **axe-core:** Auditoria de acessibilidade automatizada

---

## 5.8 Deployment (Plano Futuro)

### 5.8.1 Opções de Hospedagem

**Backend:**
- **Azure App Service:** Deploy com 1 clique do Visual Studio
- **Railway / Render:** Alternativas gratuitas com suporte a .NET

**Frontend:**
- **Vercel / Netlify:** Deploy automático do GitHub, SSL gratuito
- **GitHub Pages:** Alternativa gratuita para sites estáticos

**Banco de Dados:**
- **SQLite:** Arquivo .db incluído no deploy do backend
- **Futura migração:** PostgreSQL no Azure Database ou Supabase

**CI/CD:**
- **GitHub Actions:** Build e deploy automático a cada push
```yaml
# .github/workflows/deploy.yml
on: push
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - run: dotnet build
      - run: dotnet publish -c Release
      - run: npm run build
      - uses: azure/webapps-deploy@v2
```

---

**Documento criado em:** 29/11/2025
**Última atualização:** 29/11/2025
**Versão:** 1.0
