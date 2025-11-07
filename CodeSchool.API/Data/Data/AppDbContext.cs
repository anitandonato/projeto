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
            // Inserir desafios padrão
            modelBuilder.Entity<Desafio>().HasData(
                new Desafio
                {
                    Id = 1,
                    Titulo = "Primeiros Passos",
                    Descricao = "Faça o robô andar 3 passos para frente",
                    Nivel = 1,
                    Pontos = 10,
                    Ordem = 1,
                    BlocosDisponiveis = "move",
                    Objetivo = "move:3"
                },
                new Desafio
                {
                    Id = 2,
                    Titulo = "Virando à Direita",
                    Descricao = "Faça o robô virar à direita e andar 2 passos",
                    Nivel = 1,
                    Pontos = 15,
                    Ordem = 2,
                    BlocosDisponiveis = "move,turn",
                    Objetivo = "turn_right:1,move:2"
                },
                new Desafio
                {
                    Id = 3,
                    Titulo = "Repetindo Movimentos",
                    Descricao = "Use um loop para fazer o robô andar 5 passos",
                    Nivel = 2,
                    Pontos = 25,
                    Ordem = 3,
                    BlocosDisponiveis = "move,repeat",
                    Objetivo = "repeat:5,move"
                }
            );

            // Inserir badges padrão
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