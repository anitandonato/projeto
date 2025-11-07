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
    }
}