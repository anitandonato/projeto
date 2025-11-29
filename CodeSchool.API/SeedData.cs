using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.Models;

namespace CodeSchool.API
{
    public static class SeedData
    {
        public static async Task PopularDadosTeste(AppDbContext context)
        {
            // Verificar se já existem dados de teste
            var turma = await context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Id == 1);
            if (turma == null)
            {
                Console.WriteLine("❌ Turma não encontrada!");
                return;
            }

            // Se já tem mais de 1 aluno, não adicionar
            if (turma.Alunos.Count > 1)
            {
                Console.WriteLine("✅ Banco já possui dados de teste!");
                return;
            }

            Console.WriteLine("🔄 Populando banco com dados de teste...");

            // Criar senha hash (senha123)
            string senhaHash = BCrypt.Net.BCrypt.HashPassword("senha123");

            // Criar 5 novos alunos
            var novosAlunos = new List<Usuario>
            {
                new Usuario
                {
                    Nome = "João Silva",
                    Email = "joao@aluno.com",
                    SenhaHash = senhaHash,
                    Tipo = TipoUsuario.Aluno,
                    AvatarId = 2,
                    DataCriacao = DateTime.UtcNow
                },
                new Usuario
                {
                    Nome = "Ana Costa",
                    Email = "ana@aluno.com",
                    SenhaHash = senhaHash,
                    Tipo = TipoUsuario.Aluno,
                    AvatarId = 3,
                    DataCriacao = DateTime.UtcNow
                },
                new Usuario
                {
                    Nome = "Pedro Oliveira",
                    Email = "pedro@aluno.com",
                    SenhaHash = senhaHash,
                    Tipo = TipoUsuario.Aluno,
                    AvatarId = 4,
                    DataCriacao = DateTime.UtcNow
                },
                new Usuario
                {
                    Nome = "Carla Souza",
                    Email = "carla@aluno.com",
                    SenhaHash = senhaHash,
                    Tipo = TipoUsuario.Aluno,
                    AvatarId = 5,
                    DataCriacao = DateTime.UtcNow
                },
                new Usuario
                {
                    Nome = "Lucas Ferreira",
                    Email = "lucas@aluno.com",
                    SenhaHash = senhaHash,
                    Tipo = TipoUsuario.Aluno,
                    AvatarId = 6,
                    DataCriacao = DateTime.UtcNow
                }
            };

            context.Usuarios.AddRange(novosAlunos);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Criados {novosAlunos.Count} novos alunos");

            // Adicionar alunos à turma
            foreach (var aluno in novosAlunos)
            {
                context.AlunosTurmas.Add(new AlunoTurma
                {
                    AlunoId = aluno.Id,
                    TurmaId = turma.Id
                });
            }
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Alunos adicionados à turma {turma.Nome}");

            // Obter todos os desafios
            var desafios = await context.Desafios.OrderBy(d => d.Id).ToListAsync();

            // Criar progressos variados para cada aluno
            var progressos = new List<Progresso>();

            // João - 3 desafios completos (iniciante)
            for (int i = 0; i < 3; i++)
            {
                progressos.Add(new Progresso
                {
                    AlunoId = novosAlunos[0].Id,
                    DesafioId = desafios[i].Id,
                    Concluido = true,
                    DataConclusao = DateTime.UtcNow.AddDays(-7 + i)
                });
            }

            // Ana - 5 desafios completos (intermediário)
            for (int i = 0; i < 5; i++)
            {
                progressos.Add(new Progresso
                {
                    AlunoId = novosAlunos[1].Id,
                    DesafioId = desafios[i].Id,
                    Concluido = true,
                    DataConclusao = DateTime.UtcNow.AddDays(-10 + i)
                });
            }

            // Pedro - 7 desafios completos (avançado)
            for (int i = 0; i < 7; i++)
            {
                progressos.Add(new Progresso
                {
                    AlunoId = novosAlunos[2].Id,
                    DesafioId = desafios[i].Id,
                    Concluido = true,
                    DataConclusao = DateTime.UtcNow.AddDays(-14 + i)
                });
            }

            // Carla - 10 desafios completos (expert!)
            for (int i = 0; i < 10; i++)
            {
                progressos.Add(new Progresso
                {
                    AlunoId = novosAlunos[3].Id,
                    DesafioId = desafios[i].Id,
                    Concluido = true,
                    DataConclusao = DateTime.UtcNow.AddDays(-20 + i * 2)
                });
            }

            // Lucas - 0 desafios completos (inativo)
            // Não adiciona nenhum progresso

            context.Progressos.AddRange(progressos);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Criados {progressos.Count} registros de progresso");
            Console.WriteLine("✅ Dados de teste populados com sucesso!");
        }
    }
}
