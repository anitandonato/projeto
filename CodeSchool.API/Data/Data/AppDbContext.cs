using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Models;

namespace CodeSchool.API.Data
{
    // Contexto do banco de dados
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabelas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Desafio> Desafios { get; set; }
        public DbSet<Progresso> Progressos { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<AlunoTurma> AlunosTurmas { get; set; }
        public DbSet<UsuarioBadge> UsuariosBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relacionamento muitos-para-muitos (Alunos <-> Turmas)
            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany()
                .HasForeignKey(at => at.AlunoId);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(at => at.TurmaId);

            // Configurar relacionamento muitos-para-muitos (Usuários <-> Badges)
            modelBuilder.Entity<UsuarioBadge>()
                .HasKey(ub => new { ub.UsuarioId, ub.BadgeId });

            // Seed: Inserir dados iniciais no banco
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // ========== INSERIR 10 DESAFIOS ==========
            modelBuilder.Entity<Desafio>().HasData(
                // DESAFIO 1
                new Desafio
                {
                    Id = 1,
                    Titulo = "Primeiros Passos",
                    Descricao = "Mova o robô 3 passos para frente até alcançar o objetivo. Use apenas o bloco MOVER.",
                    Nivel = 1,
                    Pontos = 10,
                    Ordem = 1,
                    BlocosDisponiveis = "move",
                    Objetivo = "Alcançar a posição [3,0]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,0],\"direcaoInicial\":\"direita\",\"objetivo\":[3,0]}"
                },
                // DESAFIO 2
                new Desafio
                {
                    Id = 2,
                    Titulo = "Virando à Direita",
                    Descricao = "Faça o robô andar 2 passos para frente, virar à direita e andar mais 2 passos até o objetivo.",
                    Nivel = 1,
                    Pontos = 15,
                    Ordem = 2,
                    BlocosDisponiveis = "move,turn",
                    Objetivo = "Alcançar a posição [2,2]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,0],\"direcaoInicial\":\"direita\",\"objetivo\":[2,2]}"
                },
                // DESAFIO 3
                new Desafio
                {
                    Id = 3,
                    Titulo = "Repetindo Movimentos",
                    Descricao = "Use o bloco REPETIR para fazer o robô andar 5 passos sem repetir o bloco MOVER manualmente.",
                    Nivel = 2,
                    Pontos = 25,
                    Ordem = 3,
                    BlocosDisponiveis = "move,repeat",
                    Objetivo = "Alcançar a posição [4,0] usando loops",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,0],\"direcaoInicial\":\"direita\",\"objetivo\":[4,0]}"
                },
                // DESAFIO 4
                new Desafio
                {
                    Id = 4,
                    Titulo = "Quadrado Perfeito",
                    Descricao = "Faça o robô andar em forma de quadrado (1 passo para cada lado) e voltar à posição inicial. Use LOOPS!",
                    Nivel = 2,
                    Pontos = 20,
                    Ordem = 4,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Voltar para a posição inicial [1,1]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[1,1],\"direcaoInicial\":\"direita\",\"objetivo\":[1,1]}"
                },
                // DESAFIO 5
                new Desafio
                {
                    Id = 5,
                    Titulo = "Corredor em L",
                    Descricao = "Navegue pelo corredor em formato de L. Ande 4 passos para frente, vire à direita e ande mais 2 passos para baixo.",
                    Nivel = 2,
                    Pontos = 20,
                    Ordem = 5,
                    BlocosDisponiveis = "move,turn",
                    Objetivo = "Alcançar a posição [4,0]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,2],\"direcaoInicial\":\"direita\",\"objetivo\":[4,0]}"
                },
                // DESAFIO 6
                new Desafio
                {
                    Id = 6,
                    Titulo = "Escadaria",
                    Descricao = "Suba a escada diagonal fazendo um movimento em zigue-zague. Padrão: mover, virar esquerda, mover, virar direita.",
                    Nivel = 3,
                    Pontos = 30,
                    Ordem = 6,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Alcançar a posição [4,0]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,4],\"direcaoInicial\":\"direita\",\"objetivo\":[4,0]}"
                },
                // DESAFIO 7
                new Desafio
                {
                    Id = 7,
                    Titulo = "Zigue-Zague",
                    Descricao = "Percorra o grid em zigue-zague da posição [0,0] até [4,4]. Planeje bem seus movimentos e viradas!",
                    Nivel = 3,
                    Pontos = 35,
                    Ordem = 7,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Alcançar a posição [4,4]",
                    ConfiguracaoGrid = "{\"linhas\":5,\"colunas\":5,\"posicaoInicial\":[0,0],\"direcaoInicial\":\"direita\",\"objetivo\":[4,4]}"
                },
                // DESAFIO 8
                new Desafio
                {
                    Id = 8,
                    Titulo = "Explorador",
                    Descricao = "Explore o mapa grande (6x6) indo da posição inicial [0,0] até o canto oposto [5,5]. Planeje a rota mais eficiente!",
                    Nivel = 3,
                    Pontos = 40,
                    Ordem = 8,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Alcançar a posição [5,5]",
                    ConfiguracaoGrid = "{\"linhas\":6,\"colunas\":6,\"posicaoInicial\":[0,0],\"direcaoInicial\":\"direita\",\"objetivo\":[5,5]}"
                },
                // DESAFIO 9
                new Desafio
                {
                    Id = 9,
                    Titulo = "Espiral",
                    Descricao = "Crie um movimento em espiral saindo do centro [3,3] até a borda do grid [6,0]. Desafio avançado com loops complexos!",
                    Nivel = 3,
                    Pontos = 45,
                    Ordem = 9,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Alcançar a posição [6,0]",
                    ConfiguracaoGrid = "{\"linhas\":7,\"colunas\":7,\"posicaoInicial\":[3,3],\"direcaoInicial\":\"direita\",\"objetivo\":[6,0]}"
                },
                // DESAFIO 10
                new Desafio
                {
                    Id = 10,
                    Titulo = "Desafio Final",
                    Descricao = "O GRANDE DESAFIO FINAL! Percorra o grid 7x7 do canto superior esquerdo [0,6] até o canto inferior direito [6,0]. Use TUDO que aprendeu: loops, viradas estratégicas e sequências complexas!",
                    Nivel = 3,
                    Pontos = 50,
                    Ordem = 10,
                    BlocosDisponiveis = "move,turn,repeat",
                    Objetivo = "Alcançar a posição [6,0]",
                    ConfiguracaoGrid = "{\"linhas\":7,\"colunas\":7,\"posicaoInicial\":[0,6],\"direcaoInicial\":\"direita\",\"objetivo\":[6,0]}"
                }
            );

            // ========== INSERIR BADGES ==========
            modelBuilder.Entity<Badge>().HasData(
                new Badge
                {
                    Id = 1,
                    Nome = "Primeira Conquista",
                    Descricao = "Complete seu primeiro desafio!",
                    Icone = "🏆",
                    Condicao = "complete_1_desafio"
                },
                new Badge
                {
                    Id = 2,
                    Nome = "Mestre dos Loops",
                    Descricao = "Complete 3 desafios usando loops",
                    Icone = "🔁",
                    Condicao = "complete_3_com_loop"
                },
                new Badge
                {
                    Id = 3,
                    Nome = "Explorador",
                    Descricao = "Complete 10 desafios",
                    Icone = "🗺️",
                    Condicao = "complete_10_desafios"
                }
            );
        }
    }
}