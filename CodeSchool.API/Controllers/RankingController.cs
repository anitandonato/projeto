using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using System.Security.Claims;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RankingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RankingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ranking/turma/{turmaId}
        [HttpGet("turma/{turmaId}")]
        public async Task<ActionResult> ObterRankingTurma(int turmaId)
        {
            // Verificar se usuário tem acesso à turma
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Se for aluno, verificar se está na turma
            if (userRole == "Aluno")
            {
                var alunoNaTurma = await _context.AlunosTurmas
                    .AnyAsync(at => at.AlunoId == usuarioId && at.TurmaId == turmaId);

                if (!alunoNaTurma)
                {
                    return Forbid();
                }
            }
            // Se for professor, verificar se a turma é dele
            else if (userRole == "Professor")
            {
                var turmaDoProfessor = await _context.Turmas
                    .AnyAsync(t => t.Id == turmaId && t.ProfessorId == usuarioId);

                if (!turmaDoProfessor)
                {
                    return Forbid();
                }
            }

            // Buscar ranking da turma
            var alunosDaTurma = await _context.AlunosTurmas
                .Where(at => at.TurmaId == turmaId)
                .Include(at => at.Aluno)
                .Select(at => at.Aluno)
                .ToListAsync();

            var ranking = new List<object>();

            foreach (var aluno in alunosDaTurma)
            {
                var pontosTotal = await _context.Progressos
                    .Where(p => p.AlunoId == aluno.Id && p.Concluido)
                    .Include(p => p.Desafio)
                    .SumAsync(p => p.Desafio.Pontos);

                var desafiosCompletos = await _context.Progressos
                    .CountAsync(p => p.AlunoId == aluno.Id && p.Concluido);

                var badgesCount = await _context.UsuariosBadges
                    .CountAsync(ub => ub.UsuarioId == aluno.Id);

                ranking.Add(new
                {
                    alunoId = aluno.Id,
                    nome = aluno.Nome,
                    avatarId = aluno.AvatarId,
                    pontos = pontosTotal,
                    desafiosCompletos = desafiosCompletos,
                    badges = badgesCount,
                    nivel = CalcularNivel(pontosTotal)
                });
            }

            // Ordenar por pontos (decrescente)
            var rankingOrdenado = ranking
                .OrderByDescending(r => ((dynamic)r).pontos)
                .ThenByDescending(r => ((dynamic)r).desafiosCompletos)
                .Select((r, index) => new
                {
                    posicao = index + 1,
                    alunoId = ((dynamic)r).alunoId,
                    nome = ((dynamic)r).nome,
                    avatarId = ((dynamic)r).avatarId,
                    pontos = ((dynamic)r).pontos,
                    desafiosCompletos = ((dynamic)r).desafiosCompletos,
                    badges = ((dynamic)r).badges,
                    nivel = ((dynamic)r).nivel
                })
                .ToList();

            return Ok(rankingOrdenado);
        }

        // GET: api/ranking/geral
        [HttpGet("geral")]
        public async Task<ActionResult> ObterRankingGeral()
        {
            // Buscar todos os alunos
            var alunos = await _context.Usuarios
                .Where(u => u.Tipo == Models.TipoUsuario.Aluno)
                .ToListAsync();

            var ranking = new List<object>();

            foreach (var aluno in alunos)
            {
                var pontosTotal = await _context.Progressos
                    .Where(p => p.AlunoId == aluno.Id && p.Concluido)
                    .Include(p => p.Desafio)
                    .SumAsync(p => p.Desafio.Pontos);

                var desafiosCompletos = await _context.Progressos
                    .CountAsync(p => p.AlunoId == aluno.Id && p.Concluido);

                var badgesCount = await _context.UsuariosBadges
                    .CountAsync(ub => ub.UsuarioId == aluno.Id);

                ranking.Add(new
                {
                    alunoId = aluno.Id,
                    nome = aluno.Nome,
                    avatarId = aluno.AvatarId,
                    pontos = pontosTotal,
                    desafiosCompletos = desafiosCompletos,
                    badges = badgesCount,
                    nivel = CalcularNivel(pontosTotal)
                });
            }

            // Ordenar e pegar top 50
            var rankingOrdenado = ranking
                .OrderByDescending(r => ((dynamic)r).pontos)
                .ThenByDescending(r => ((dynamic)r).desafiosCompletos)
                .Take(50)
                .Select((r, index) => new
                {
                    posicao = index + 1,
                    alunoId = ((dynamic)r).alunoId,
                    nome = ((dynamic)r).nome,
                    avatarId = ((dynamic)r).avatarId,
                    pontos = ((dynamic)r).pontos,
                    desafiosCompletos = ((dynamic)r).desafiosCompletos,
                    badges = ((dynamic)r).badges,
                    nivel = ((dynamic)r).nivel
                })
                .ToList();

            return Ok(rankingOrdenado);
        }

        // GET: api/ranking/minha-posicao/{turmaId}
        [HttpGet("minha-posicao/{turmaId}")]
        [Authorize(Roles = "Aluno")]
        public async Task<ActionResult> ObterMinhaPosicao(int turmaId)
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Verificar se está na turma
            var alunoNaTurma = await _context.AlunosTurmas
                .AnyAsync(at => at.AlunoId == alunoId && at.TurmaId == turmaId);

            if (!alunoNaTurma)
            {
                return Forbid();
            }

            // Buscar pontos do aluno
            var meusPontos = await _context.Progressos
                .Where(p => p.AlunoId == alunoId && p.Concluido)
                .Include(p => p.Desafio)
                .SumAsync(p => p.Desafio.Pontos);

            // Contar quantos alunos têm mais pontos
            var alunosDaTurma = await _context.AlunosTurmas
                .Where(at => at.TurmaId == turmaId)
                .Select(at => at.AlunoId)
                .ToListAsync();

            int posicao = 1;
            foreach (var outroAlunoId in alunosDaTurma)
            {
                if (outroAlunoId == alunoId) continue;

                var pontosOutro = await _context.Progressos
                    .Where(p => p.AlunoId == outroAlunoId && p.Concluido)
                    .Include(p => p.Desafio)
                    .SumAsync(p => p.Desafio.Pontos);

                if (pontosOutro > meusPontos)
                {
                    posicao++;
                }
            }

            return Ok(new
            {
                posicao = posicao,
                pontos = meusPontos,
                totalAlunos = alunosDaTurma.Count
            });
        }

        private int CalcularNivel(int pontos)
        {
            if (pontos < 50) return 1;
            if (pontos < 150) return 2;
            if (pontos < 300) return 3;
            if (pontos < 500) return 4;
            return 5;
        }
    }
}
