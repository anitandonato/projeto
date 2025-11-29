using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.Models;

namespace CodeSchool.API.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }

        // Verificar se o aluno ganhou novas badges
        public async Task<List<Badge>> VerificarBadges(int alunoId)
        {
            var badgesNovas = new List<Badge>();
            
            // Contar desafios completos
            var desafiosCompletos = await _context.Progressos
                .Where(p => p.AlunoId == alunoId && p.Concluido)
                .CountAsync();

            // Badge: Primeira Conquista
            if (desafiosCompletos >= 1)
            {
                await AdicionarBadge(alunoId, 1, badgesNovas);
            }

            // Badge: Explorador (10 desafios)
            if (desafiosCompletos >= 10)
            {
                await AdicionarBadge(alunoId, 3, badgesNovas);
            }

            return badgesNovas;
        }

        private async Task AdicionarBadge(int alunoId, int badgeId, List<Badge> badgesNovas)
        {
            // Verificar se já tem a badge
            var jaTemBadge = await _context.UsuariosBadges
                .AnyAsync(ub => ub.UsuarioId == alunoId && ub.BadgeId == badgeId);

            if (!jaTemBadge)
            {
                var usuarioBadge = new UsuarioBadge
                {
                    UsuarioId = alunoId,
                    BadgeId = badgeId
                };
                
                _context.UsuariosBadges.Add(usuarioBadge);
                await _context.SaveChangesAsync();

                var badge = await _context.Badges.FindAsync(badgeId);
                if (badge != null)
                {
                    badgesNovas.Add(badge);
                }
            }
        }

        // Calcular nível do aluno baseado em pontos
        public int CalcularNivel(int pontos)
        {
            if (pontos < 50) return 1;
            if (pontos < 150) return 2;
            if (pontos < 300) return 3;
            if (pontos < 500) return 4;
            return 5;
        }

        // Atualizar avatar baseado no nível atual
        public async Task AtualizarAvatar(int alunoId, int pontos)
        {
            var aluno = await _context.Usuarios.FindAsync(alunoId);
            if (aluno == null) return;

            var nivel = CalcularNivel(pontos);

            // Mapear nível para avatarId (Nível 1 = Avatar 1, Nível 2 = Avatar 2, etc.)
            int novoAvatarId = nivel;

            // Se o avatar mudou, atualizar
            if (aluno.AvatarId != novoAvatarId)
            {
                aluno.AvatarId = novoAvatarId;
                await _context.SaveChangesAsync();
            }
        }

        // Recalcular avatares de TODOS os alunos baseado nos pontos
        public async Task<int> RecalcularTodosAvatares()
        {
            var alunos = await _context.Usuarios
                .Where(u => u.Tipo == Models.TipoUsuario.Aluno)
                .ToListAsync();

            int atualizados = 0;

            foreach (var aluno in alunos)
            {
                // Calcular pontos totais do aluno
                var pontosTotal = await _context.Progressos
                    .Where(p => p.AlunoId == aluno.Id && p.Concluido)
                    .Include(p => p.Desafio)
                    .SumAsync(p => p.Desafio.Pontos);

                var nivel = CalcularNivel(pontosTotal);
                int avatarCorreto = nivel;

                if (aluno.AvatarId != avatarCorreto)
                {
                    aluno.AvatarId = avatarCorreto;
                    atualizados++;
                }
            }

            if (atualizados > 0)
            {
                await _context.SaveChangesAsync();
            }

            return atualizados;
        }
    }
}