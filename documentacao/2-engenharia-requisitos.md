# 2. ENGENHARIA DE REQUISITOS

## 2.1 Levantamento e Análise de Requisitos

### 2.1.1 Requisitos Funcionais (RF)

#### RF01 - Sistema de Autenticação
- **RF01.1:** O sistema deve permitir que usuários se cadastrem informando nome, email, senha e tipo de usuário (Aluno ou Professor)
- **RF01.2:** O sistema deve validar emails únicos no cadastro
- **RF01.3:** O sistema deve autenticar usuários através de email e senha
- **RF01.4:** O sistema deve gerar e retornar token JWT após autenticação bem-sucedida
- **RF01.5:** O sistema deve permitir logout, invalidando a sessão do usuário

#### RF02 - Gestão de Turmas
- **RF02.1:** Professores devem poder criar turmas informando um nome
- **RF02.2:** O sistema deve gerar automaticamente um código único de 6 caracteres para cada turma
- **RF02.3:** Alunos devem poder entrar em turmas utilizando o código gerado
- **RF02.4:** Professores devem poder visualizar lista de todos os alunos de suas turmas
- **RF02.5:** O sistema deve exibir quantidade de alunos por turma
- **RF02.6:** Alunos podem participar de múltiplas turmas simultaneamente

#### RF03 - Sistema de Desafios
- **RF03.1:** O sistema deve disponibilizar 10 desafios progressivos de programação visual
- **RF03.2:** Cada desafio deve conter: título, descrição, grid visual, objetivo claro
- **RF03.3:** Desafios devem ser desbloqueados sequencialmente (desafio N+1 após completar N)
- **RF03.4:** O sistema deve validar se a solução do aluno atende ao objetivo do desafio
- **RF03.5:** O sistema deve fornecer feedback visual em tempo real (animação do robô no grid)
- **RF03.6:** Alunos devem poder salvar o progresso de cada desafio

#### RF04 - Programação Visual (Blockly)
- **RF04.1:** O sistema deve disponibilizar blocos de movimentação (mover, virar_direita, virar_esquerda)
- **RF04.2:** O sistema deve disponibilizar blocos de controle (repetir N vezes)
- **RF04.3:** O sistema deve disponibilizar blocos condicionais (se, se_senão)
- **RF04.4:** O sistema deve disponibilizar blocos de sensores (tem_parede_frente, esta_no_objetivo)
- **RF04.5:** O sistema deve disponibilizar blocos de variáveis (definir, obter, incrementar)
- **RF04.6:** O sistema deve disponibilizar operadores de comparação (=, >, <, >=, <=, !=)
- **RF04.7:** O sistema deve interpretar e executar os blocos montados pelo aluno
- **RF04.8:** O sistema deve permitir arrastar, soltar e reorganizar blocos livremente

#### RF05 - Sistema de Gamificação
- **RF05.1:** O sistema deve atribuir pontos automaticamente ao completar desafios
- **RF05.2:** O sistema deve conceder badges ao atingir marcos específicos (ex: 3 desafios, 7 desafios, todos os desafios)
- **RF05.3:** O sistema deve calcular nível do aluno baseado em pontos totais (5 níveis: 1-5)
- **RF05.4:** Alunos devem poder escolher avatares dentre opções disponíveis
- **RF05.5:** O sistema deve exibir progresso visual do aluno (barra de progresso, badges conquistados)
- **RF05.6:** O sistema deve manter ranking de alunos por turma
- **RF05.7:** O sistema deve manter ranking geral de todos os alunos
- **RF05.8:** O sistema deve exibir posição do aluno no ranking de sua turma

#### RF06 - Dashboard do Aluno
- **RF06.1:** Alunos devem visualizar seu progresso geral (desafios completados, pontos, nível)
- **RF06.2:** Alunos devem visualizar lista de todos os desafios com status (bloqueado, em progresso, completo)
- **RF06.3:** Alunos devem visualizar suas turmas ativas
- **RF06.4:** Alunos devem poder acessar narrativa do Explorador Digital
- **RF06.5:** Alunos devem visualizar seu ranking na turma
- **RF06.6:** Dashboard deve exibir mensagens motivacionais baseadas no progresso

#### RF07 - Dashboard do Professor
- **RF07.1:** Professores devem visualizar lista de todas as suas turmas
- **RF07.2:** Professores devem poder criar novas turmas
- **RF07.3:** Professores devem acessar detalhes de cada turma (lista de alunos, estatísticas)
- **RF07.4:** Professores devem poder acessar recursos pedagógicos
- **RF07.5:** Professores devem visualizar código da turma para compartilhar com alunos

#### RF08 - Relatórios do Professor
- **RF08.1:** O sistema deve gerar relatórios de progresso individual de cada aluno
- **RF08.2:** O sistema deve gerar relatórios de desempenho da turma
- **RF08.3:** Relatórios devem incluir gráficos visuais de: distribuição de pontos, progresso por desafio, badges conquistados
- **RF08.4:** O sistema deve identificar alunos com dificuldades (poucos desafios completos)
- **RF08.5:** O sistema deve calcular média de tentativas por desafio
- **RF08.6:** Relatórios devem ser visualizados na interface web
- **RF08.7:** Sistema deve permitir exportação de relatórios (futuro)

#### RF09 - Narrativa Gamificada
- **RF09.1:** O sistema deve disponibilizar história completa do "Explorador Digital"
- **RF09.2:** Narrativa deve estar dividida em 4 capítulos progressivos
- **RF09.3:** Cada capítulo deve contextualizar um conjunto de desafios
- **RF09.4:** Sistema deve exibir recompensas narrativas ao completar capítulos
- **RF09.5:** Narrativa deve incluir glossário de termos de programação

#### RF10 - Recursos Pedagógicos
- **RF10.1:** Professores devem poder visualizar narrativa completa
- **RF10.2:** Professores devem poder visualizar 8 atividades desplugadas com instruções completas
- **RF10.3:** Professores devem poder visualizar mapeamento BNCC de todos os desafios
- **RF10.4:** Sistema deve disponibilizar botões de download para cada recurso (PDF)
- **RF10.5:** Recursos devem incluir dicas pedagógicas de uso em sala de aula
- **RF10.6:** Sistema deve exibir modal de preview antes do download

#### RF11 - Acessibilidade
- **RF11.1:** O sistema deve disponibilizar menu de acessibilidade em todas as páginas
- **RF11.2:** Usuários devem poder ativar modo Alto Contraste
- **RF11.3:** Usuários devem poder aumentar tamanho das fontes (+1, +2, +3)
- **RF11.4:** Usuários devem poder diminuir tamanho das fontes
- **RF11.5:** Usuários devem poder ativar narração de tela (Text-to-Speech)
- **RF11.6:** Sistema deve fornecer atalhos de teclado: Alt+C (contraste), Alt++ (aumentar), Alt+- (diminuir), Alt+1 (pular para conteúdo)
- **RF11.7:** Todas as páginas devem ter link "Pular para conteúdo principal"
- **RF11.8:** Preferências de acessibilidade devem ser persistidas no localStorage

### 2.1.2 Requisitos Não-Funcionais (RNF)

#### RNF01 - Usabilidade
- **RNF01.1:** Interface deve ser intuitiva para alunos de 11-14 anos
- **RNF01.2:** Navegação deve seguir padrões visuais consistentes em todas as páginas
- **RNF01.3:** Feedback visual deve ser imediato para todas as ações do usuário (<500ms)
- **RNF01.4:** Mensagens de erro devem ser claras e orientar o usuário na correção
- **RNF01.5:** Sistema deve usar linguagem simples e adequada à faixa etária

#### RNF02 - Acessibilidade (WCAG 2.1 Nível AA)
- **RNF02.1:** Contraste de cores deve atender razão mínima 4.5:1
- **RNF02.2:** Todos os elementos interativos devem ser operáveis por teclado
- **RNF02.3:** Elementos focáveis devem ter indicação visual clara de foco
- **RNF02.4:** Textos devem permitir zoom até 200% sem perda de funcionalidade
- **RNF02.5:** Imagens decorativas devem ter alt="" e imagens informativas devem ter descrição
- **RNF02.6:** Sistema deve suportar leitores de tela (NVDA, JAWS)

#### RNF03 - Performance
- **RNF03.1:** Tempo de carregamento inicial não deve exceder 3 segundos
- **RNF03.2:** Execução de código Blockly deve ser processada em <1 segundo
- **RNF03.3:** Animações do robô devem rodar a 60fps para fluidez
- **RNF03.4:** Requisições à API devem retornar em <2 segundos
- **RNF03.5:** Sistema deve suportar até 100 usuários simultâneos sem degradação

#### RNF04 - Segurança
- **RNF04.1:** Senhas devem ser criptografadas com BCrypt antes de armazenamento
- **RNF04.2:** Autenticação deve usar tokens JWT com expiração de 24 horas
- **RNF04.3:** Rotas protegidas devem validar token em cada requisição
- **RNF04.4:** Sistema deve validar tipos de usuário (Aluno/Professor) antes de permitir acesso a recursos
- **RNF04.5:** Dados sensíveis não devem ser expostos em logs ou mensagens de erro
- **RNF04.6:** Comunicação deve usar HTTPS em produção

#### RNF05 - Compatibilidade
- **RNF05.1:** Sistema deve funcionar em navegadores modernos: Chrome 90+, Firefox 88+, Edge 90+, Safari 14+
- **RNF05.2:** Interface deve ser responsiva para telas de 1024px ou superior
- **RNF05.3:** Sistema deve funcionar em Windows 10/11, macOS 11+, Linux Ubuntu 20.04+

#### RNF06 - Manutenibilidade
- **RNF06.1:** Código frontend deve seguir padrões Vue.js 3 Composition API
- **RNF06.2:** Código backend deve seguir princípios SOLID e Clean Code
- **RNF06.3:** Cada componente deve ter responsabilidade única e bem definida
- **RNF06.4:** Nomes de variáveis, funções e classes devem ser descritivos
- **RNF06.5:** Código crítico deve conter comentários explicativos

#### RNF07 - Confiabilidade
- **RNF07.1:** Sistema deve validar dados de entrada em frontend e backend
- **RNF07.2:** Erros devem ser tratados graciosamente sem quebrar a aplicação
- **RNF07.3:** Dados críticos (progresso do aluno) devem ter backup automático
- **RNF07.4:** Sistema deve manter consistência de dados entre frontend e backend

#### RNF08 - Escalabilidade
- **RNF08.1:** Arquitetura deve permitir adição de novos desafios sem refatoração
- **RNF08.2:** Sistema deve permitir adição de novos tipos de blocos Blockly
- **RNF08.3:** Banco de dados deve suportar crescimento para 1000+ usuários
- **RNF08.4:** Código deve permitir internacionalização (i18n) futura

#### RNF09 - Portabilidade
- **RNF09.1:** Backend deve ser executável em qualquer sistema com .NET 8.0
- **RNF09.2:** Banco de dados SQLite deve ser portável entre sistemas operacionais
- **RNF09.3:** Aplicação deve poder ser empacotada em container Docker (futuro)

---

## 2.2 Histórias de Usuário

### Épico 1: Gestão de Acesso

**US01 - Cadastro de Aluno**
> Como um **aluno do ensino fundamental**, eu quero **me cadastrar na plataforma informando meu nome, email e senha** para **começar a aprender programação de forma gamificada**.

**Critérios de Aceitação:**
- Sistema valida se email já está em uso
- Senha deve ter no mínimo 6 caracteres
- Após cadastro bem-sucedido, aluno é redirecionado para dashboard
- Tipo de usuário "Aluno" é atribuído automaticamente

**US02 - Cadastro de Professor**
> Como um **professor**, eu quero **me cadastrar na plataforma selecionando o tipo "Professor"** para **gerenciar turmas e acompanhar o progresso dos meus alunos**.

**Critérios de Aceitação:**
- Sistema permite seleção de tipo de usuário no cadastro
- Professor tem acesso a funcionalidades exclusivas após cadastro
- Dashboard exibido é diferente do dashboard de aluno

**US03 - Login no Sistema**
> Como um **usuário cadastrado**, eu quero **fazer login com email e senha** para **acessar minha conta e continuar minhas atividades**.

**Critérios de Aceitação:**
- Sistema valida credenciais contra banco de dados
- Token JWT é gerado e armazenado no navegador
- Usuário é redirecionado para dashboard apropriado (aluno ou professor)
- Mensagem de erro clara é exibida em caso de credenciais inválidas

---

### Épico 2: Sistema de Turmas

**US04 - Criar Turma**
> Como um **professor**, eu quero **criar turmas informando um nome** para **organizar meus alunos por classe ou período**.

**Critérios de Aceitação:**
- Professor pode criar múltiplas turmas
- Sistema gera código único de 6 caracteres automaticamente
- Código é exibido ao professor imediatamente após criação
- Turma aparece na lista de turmas do professor

**US05 - Entrar em Turma**
> Como um **aluno**, eu quero **entrar em uma turma usando o código fornecido pelo professor** para **ter meu progresso acompanhado e participar do ranking**.

**Critérios de Aceitação:**
- Aluno pode entrar em múltiplas turmas
- Sistema valida se código existe antes de adicionar aluno
- Mensagem de confirmação é exibida após entrada bem-sucedida
- Turma aparece na seção "Minhas Turmas" do dashboard

**US06 - Visualizar Alunos da Turma**
> Como um **professor**, eu quero **ver lista completa de alunos em cada turma** para **saber quem está participando das atividades**.

**Critérios de Aceitação:**
- Lista exibe nome, avatar e progresso de cada aluno
- Dados são atualizados em tempo real
- Professor pode clicar no aluno para ver detalhes individuais

---

### Épico 3: Desafios de Programação

**US07 - Visualizar Desafios Disponíveis**
> Como um **aluno**, eu quero **ver lista de todos os desafios com status de desbloqueio** para **saber quais posso resolver e quais estão bloqueados**.

**Critérios de Aceitação:**
- Desafios bloqueados têm aparência visual diferenciada (opacidade, cadeado)
- Desafios completos exibem badge de conclusão
- Sistema mostra progresso geral (X de 10 completos)

**US08 - Resolver Desafio com Blockly**
> Como um **aluno**, eu quero **arrastar blocos visuais para programar o robô** para **resolver os desafios sem precisar escrever código textual**.

**Critérios de Aceitação:**
- Interface Blockly é intuitiva e responsiva
- Blocos são organizados por categorias (Movimento, Controle, Condicionais, etc.)
- Aluno pode executar código a qualquer momento clicando "Executar"
- Animação mostra robô executando comandos no grid

**US09 - Validar Solução**
> Como um **aluno**, eu quero **receber feedback imediato se minha solução está correta** para **saber se completei o desafio ou preciso ajustar**.

**Critérios de Aceitação:**
- Sistema verifica se robô chegou ao objetivo
- Mensagem de sucesso é exibida com pontos ganhos
- Mensagem de erro orienta aluno a tentar novamente
- Próximo desafio é desbloqueado automaticamente após sucesso

**US10 - Salvar Progresso**
> Como um **aluno**, eu quero **que meu progresso seja salvo automaticamente** para **poder sair e voltar depois sem perder meus blocos**.

**Critérios de Aceitação:**
- Solução é salva no banco ao completar desafio
- Pontos e badges são persistidos
- Estado de desbloqueio é mantido entre sessões

---

### Épico 4: Gamificação

**US11 - Ganhar Pontos**
> Como um **aluno**, eu quero **ganhar pontos ao completar desafios** para **subir de nível e competir no ranking**.

**Critérios de Aceitação:**
- Cada desafio concede pontos específicos
- Pontos são somados ao total do aluno
- Dashboard exibe total de pontos em destaque

**US12 - Conquistar Badges**
> Como um **aluno**, eu quero **conquistar badges ao atingir marcos importantes** para **me sentir recompensado pelo progresso**.

**Critérios de Aceitação:**
- Badges são desbloqueados automaticamente (3, 7, 10 desafios)
- Notificação visual celebra conquista de novo badge
- Badges aparecem no perfil do aluno

**US13 - Subir de Nível**
> Como um **aluno**, eu quero **ver meu nível aumentar conforme acumulo pontos** para **ter senso de progressão**.

**Critérios de Aceitação:**
- Sistema calcula nível baseado em pontos (1-5)
- Barra de progresso mostra quanto falta para próximo nível
- Mudança de nível é celebrada visualmente

**US14 - Escolher Avatar**
> Como um **aluno**, eu quero **escolher um avatar que me representa** para **personalizar minha experiência**.

**Critérios de Aceitação:**
- Painel de seleção exibe 12+ avatares disponíveis
- Avatar selecionado aparece em dashboard e ranking
- Escolha é salva e pode ser alterada a qualquer momento

**US15 - Ver Ranking da Turma**
> Como um **aluno**, eu quero **ver minha posição no ranking da turma** para **me motivar a melhorar e competir de forma saudável**.

**Critérios de Aceitação:**
- Ranking ordena alunos por pontos (decrescente)
- Top 3 tem destaque visual (medalhas ouro, prata, bronze)
- Minha posição é destacada na lista
- Ranking atualiza em tempo real

---

### Épico 5: Acompanhamento do Professor

**US16 - Visualizar Relatório da Turma**
> Como um **professor**, eu quero **ver relatório de desempenho da turma com gráficos** para **identificar tendências e dificuldades gerais**.

**Critérios de Aceitação:**
- Relatório inclui gráfico de distribuição de pontos
- Relatório mostra progresso por desafio (quantos completaram cada um)
- Relatório lista alunos com dificuldades
- Gráficos são gerados com Chart.js

**US17 - Visualizar Progresso Individual**
> Como um **professor**, eu quero **ver detalhes de progresso de cada aluno** para **oferecer suporte personalizado**.

**Critérios de Aceitação:**
- Detalhes mostram desafios completos, pontos, badges, nível
- Detalhes incluem média de tentativas por desafio
- Professor pode navegar entre alunos facilmente

**US18 - Acessar Recursos Pedagógicos**
> Como um **professor**, eu quero **acessar atividades desplugadas e mapeamento BNCC** para **enriquecer minhas aulas**.

**Critérios de Aceitação:**
- Página Recursos Pedagógicos lista 3 materiais principais
- Cada recurso tem botões de visualizar e baixar
- Preview em modal permite leitura antes do download
- Materiais incluem narrativas, atividades desplugadas, BNCC

---

### Épico 6: Narrativa e Engajamento

**US19 - Ler História do Explorador Digital**
> Como um **aluno**, eu quero **ler a história que contextualiza os desafios** para **me sentir imerso em uma aventura de programação**.

**Critérios de Aceitação:**
- Narrativa é dividida em 4 capítulos progressivos
- Cada capítulo relaciona conjunto de desafios
- Página tem design visual atrativo com ilustrações
- Glossário de termos está disponível ao final

**US20 - Receber Mensagens Motivacionais**
> Como um **aluno**, eu quero **receber mensagens de incentivo baseadas no meu progresso** para **me manter motivado a continuar**.

**Critérios de Aceitação:**
- Mensagens são contextuais (início, meio, quase fim)
- Tom é positivo e encorajador
- Mensagens aparecem em momentos estratégicos (após falha, após sucesso)

---

### Épico 7: Acessibilidade

**US21 - Ativar Alto Contraste**
> Como um **usuário com baixa visão**, eu quero **ativar modo de alto contraste** para **enxergar melhor os elementos da interface**.

**Critérios de Aceitação:**
- Contraste pode ser ativado via menu ou atalho Alt+C
- Cores mudam para esquema preto/branco/amarelo
- Preferência é salva para próximas sessões

**US22 - Ajustar Tamanho da Fonte**
> Como um **usuário com dificuldade visual**, eu quero **aumentar o tamanho das fontes** para **ler textos com mais facilidade**.

**Critérios de Aceitação:**
- Fontes podem ser aumentadas em 3 níveis (+1, +2, +3)
- Fontes podem ser diminuídas até tamanho padrão
- Atalhos Alt++ e Alt+- funcionam corretamente
- Layout não quebra com fontes aumentadas

**US23 - Usar Narração de Tela**
> Como um **usuário com deficiência visual**, eu quero **ativar narração de elementos ao passar o mouse** para **navegar sem depender apenas da visão**.

**Critérios de Aceitação:**
- Narração usa Web Speech API nativa
- Elementos importantes são narrados ao hover
- Narração pode ser ativada/desativada facilmente
- Compatível com leitores de tela

**US24 - Navegar por Teclado**
> Como um **usuário com dificuldade motora**, eu quero **navegar toda a plataforma usando apenas teclado** para **não depender de mouse**.

**Critérios de Aceitação:**
- Tab navega entre elementos focáveis
- Enter/Space ativa botões e links
- Escape fecha modais
- Indicação visual clara de elemento focado

---

## 2.3 Regras de Negócio

### RN01 - Autenticação e Autorização
- **RN01.1:** Emails devem ser únicos no sistema (não pode haver duplicatas)
- **RN01.2:** Senhas devem ter no mínimo 6 caracteres
- **RN01.3:** Token JWT expira após 24 horas, exigindo novo login
- **RN01.4:** Alunos não podem acessar funcionalidades de Professor e vice-versa
- **RN01.5:** Usuários não autenticados são redirecionados para tela de login

### RN02 - Gestão de Turmas
- **RN02.1:** Código de turma deve ser único no sistema
- **RN02.2:** Código de turma deve ter exatamente 6 caracteres alfanuméricos
- **RN02.3:** Apenas professores podem criar turmas
- **RN02.4:** Alunos podem participar de ilimitadas turmas
- **RN02.5:** Aluno não pode entrar na mesma turma duas vezes

### RN03 - Progressão de Desafios
- **RN03.1:** Aluno deve completar desafio N para desbloquear desafio N+1
- **RN03.2:** Desafio 1 está sempre desbloqueado para novos alunos
- **RN03.3:** Desafios só podem ser marcados como completos se robô atingir objetivo
- **RN03.4:** Desafios completados não podem ser "desmarcados"
- **RN03.5:** Sistema valida solução no backend (não apenas frontend)

### RN04 - Pontuação e Gamificação
- **RN04.1:** Cada desafio concede pontos fixos ao ser completado pela primeira vez
- **RN04.2:** Re-completar desafio não concede pontos adicionais
- **RN04.3:** Pontuação por desafio: Desafios 1-3 = 100pts, 4-6 = 150pts, 7-10 = 200pts
- **RN04.4:** Badges são concedidos automaticamente ao atingir marcos:
  - "Primeiros Passos": 3 desafios completos
  - "Programador em Ascensão": 7 desafios completos
  - "Mestre Explorador": 10 desafios completos
- **RN04.5:** Nível é calculado baseado em pontos totais:
  - Nível 1: 0-299 pontos
  - Nível 2: 300-699 pontos
  - Nível 3: 700-1199 pontos
  - Nível 4: 1200-1699 pontos
  - Nível 5: 1700+ pontos
- **RN04.6:** Avatar pode ser alterado a qualquer momento sem custo

### RN05 - Ranking
- **RN05.1:** Ranking é ordenado por pontos totais (decrescente)
- **RN05.2:** Em caso de empate, aluno que atingiu pontuação primeiro fica à frente
- **RN05.3:** Ranking de turma só inclui alunos daquela turma específica
- **RN05.4:** Ranking geral inclui todos os alunos do sistema
- **RN05.5:** Ranking atualiza em tempo real após cada desafio completado

### RN06 - Relatórios
- **RN06.1:** Professor só pode ver relatórios de suas próprias turmas
- **RN06.2:** Relatório de turma calcula estatísticas apenas dos alunos daquela turma
- **RN06.3:** Aluno com dificuldade é identificado como tendo <3 desafios completos
- **RN06.4:** Média de tentativas é calculada dividindo total de tentativas por número de desafios

### RN07 - Validação de Código Blockly
- **RN07.1:** Robô inicia sempre na posição definida no desafio
- **RN07.2:** Robô não pode sair dos limites do grid (erro se tentar)
- **RN07.3:** Movimentação através de paredes não é permitida
- **RN07.4:** Desafio só é completo se robô parar exatamente na célula objetivo
- **RN07.5:** Loops infinitos são interrompidos após 1000 iterações

### RN08 - Persistência de Dados
- **RN08.1:** Progresso do aluno é salvo imediatamente ao completar desafio
- **RN08.2:** Logout não apaga dados, apenas invalida sessão
- **RN08.3:** Preferências de acessibilidade são salvas no localStorage do navegador
- **RN08.4:** Exclusão de usuário remove todos os dados relacionados (cascata)

### RN09 - Acessibilidade
- **RN09.1:** Preferências de acessibilidade aplicam-se apenas ao navegador atual
- **RN09.2:** Contraste alto sobrescreve tema padrão
- **RN09.3:** Tamanho de fonte não pode ser reduzido abaixo do padrão original
- **RN09.4:** Narração usa idioma português (pt-BR)

### RN10 - Recursos Pedagógicos
- **RN10.1:** Apenas professores têm acesso à página de Recursos Pedagógicos
- **RN10.2:** Narrativa completa está disponível para alunos e professores (páginas diferentes)
- **RN10.3:** Download de PDFs exige que professor esteja autenticado
- **RN10.4:** Materiais pedagógicos são estáticos (não editáveis pelos professores)

---

## 2.4 Matriz de Rastreabilidade

| Requisito Funcional | Histórias de Usuário Relacionadas | Regras de Negócio Relacionadas |
|---------------------|-----------------------------------|-------------------------------|
| RF01 - Sistema de Autenticação | US01, US02, US03 | RN01.1, RN01.2, RN01.3, RN01.4, RN01.5 |
| RF02 - Gestão de Turmas | US04, US05, US06 | RN02.1, RN02.2, RN02.3, RN02.4, RN02.5 |
| RF03 - Sistema de Desafios | US07, US08, US09, US10 | RN03.1, RN03.2, RN03.3, RN03.4, RN03.5 |
| RF04 - Programação Visual | US08 | RN07.1, RN07.2, RN07.3, RN07.4, RN07.5 |
| RF05 - Sistema de Gamificação | US11, US12, US13, US14, US15 | RN04.1, RN04.2, RN04.3, RN04.4, RN04.5, RN04.6, RN05.1, RN05.2, RN05.3, RN05.4, RN05.5 |
| RF06 - Dashboard do Aluno | US07, US15, US19 | RN03.1, RN05.3 |
| RF07 - Dashboard do Professor | US06, US16, US18 | RN06.1 |
| RF08 - Relatórios do Professor | US16, US17 | RN06.1, RN06.2, RN06.3, RN06.4 |
| RF09 - Narrativa Gamificada | US19, US20 | RN10.2 |
| RF10 - Recursos Pedagógicos | US18 | RN10.1, RN10.2, RN10.3, RN10.4 |
| RF11 - Acessibilidade | US21, US22, US23, US24 | RN09.1, RN09.2, RN09.3, RN09.4 |

---

## 2.5 Priorização de Requisitos (MoSCoW)

### Must Have (Deve Ter) - Essencial para MVP
- RF01 - Sistema de Autenticação
- RF02 - Gestão de Turmas
- RF03 - Sistema de Desafios
- RF04 - Programação Visual (blocos básicos)
- RF05 - Sistema de Gamificação (pontos, badges, níveis)
- RF06 - Dashboard do Aluno
- RF07 - Dashboard do Professor
- RNF01 - Usabilidade
- RNF04 - Segurança

### Should Have (Deveria Ter) - Importante mas não bloqueante
- RF08 - Relatórios do Professor
- RF09 - Narrativa Gamificada
- RF11 - Acessibilidade (menu básico)
- RNF02 - Acessibilidade WCAG 2.1 AA
- RNF03 - Performance
- RNF05 - Compatibilidade

### Could Have (Poderia Ter) - Desejável se houver tempo
- RF10 - Recursos Pedagógicos
- RF04 - Programação Visual (blocos avançados: variáveis, condicionais)
- RF05 - Ranking (geral e por turma)
- RNF06 - Manutenibilidade
- RNF07 - Confiabilidade

### Won't Have (Não Terá) - Fora do escopo atual
- Exportação de relatórios em PDF
- Edição de desafios pelos professores
- Modo multiplayer em tempo real
- Integração com Google Classroom
- Aplicativo mobile nativo
- Containerização Docker
- Internacionalização (i18n)

---

**Documento criado em:** 29/11/2025
**Última atualização:** 29/11/2025
**Versão:** 1.0
