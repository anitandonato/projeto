using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.Models;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Professor")]
    public class RelatorioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatorioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Relatorio/turma/{turmaId}
// GET: api/Relatorio/turma/{turmaId}
[HttpGet("turma/{turmaId}")]
public async Task<IActionResult> ObterEstatisticasTurma(int turmaId)
{
    var turma = await _context.Turmas
        .Include(t => t.Alunos)
            .ThenInclude(at => at.Aluno)
                .ThenInclude(a => a.Progressos)
                    .ThenInclude(p => p.Desafio)
        .FirstOrDefaultAsync(t => t.Id == turmaId);

    if (turma == null)
        return NotFound(new { mensagem = "Turma não encontrada" });

    var totalAlunos = turma.Alunos.Count;
    var totalDesafios = await _context.Desafios.CountAsync();

    // Obter IDs dos alunos da turma
    var alunosIds = turma.Alunos.Select(at => at.AlunoId).ToList();

    // Calcular estatísticas dos alunos
    var alunosComProgresso = turma.Alunos
        .Select(at => new
        {
            Nome = at.Aluno.Nome,
            Email = at.Aluno.Email,
            AvatarId = at.Aluno.AvatarId,
            DesafiosCompletos = at.Aluno.Progressos.Count(p => p.Concluido),
            TotalPontos = at.Aluno.Progressos
                .Where(p => p.Concluido)
                .Sum(p => p.Desafio.Pontos),
            UltimoDesafio = at.Aluno.Progressos
                .Where(p => p.Concluido)
                .OrderByDescending(p => p.DataConclusao)
                .Select(p => p.DataConclusao)
                .FirstOrDefault()
        })
        .OrderByDescending(a => a.TotalPontos)
        .ToList();

    // Calcular estatísticas por desafio (query simplificada)
    var todosDesafios = await _context.Desafios.ToListAsync();
    var todosProgressos = await _context.Progressos
        .Where(p => alunosIds.Contains(p.AlunoId) && p.Concluido)
        .ToListAsync();

    var estatisticasPorDesafio = todosDesafios
        .Select(d => new
        {
            DesafioId = d.Id,
            Titulo = d.Titulo,
            Nivel = d.Nivel,
            TotalAlunos = totalAlunos,
            AlunosCompletos = todosProgressos.Count(p => p.DesafioId == d.Id),
            TaxaConclusao = totalAlunos > 0 
                ? (double)todosProgressos.Count(p => p.DesafioId == d.Id) / totalAlunos * 100 
                : 0
        })
        .ToList();

    var mediaDesafiosCompletos = alunosComProgresso.Any() 
        ? alunosComProgresso.Average(a => a.DesafiosCompletos) 
        : 0;

    var mediaPontos = alunosComProgresso.Any() 
        ? alunosComProgresso.Average(a => a.TotalPontos) 
        : 0;

    var alunosAtivos = alunosComProgresso.Count(a => a.DesafiosCompletos > 0);
    var alunosInativos = totalAlunos - alunosAtivos;

    return Ok(new
    {
        turma = new
        {
            id = turma.Id,
            nome = turma.Nome,
            codigo = turma.Codigo
        },
        resumo = new
        {
            totalAlunos,
            totalDesafios,
            alunosAtivos,
            alunosInativos,
            mediaDesafiosCompletos = Math.Round(mediaDesafiosCompletos, 1),
            mediaPontos = Math.Round(mediaPontos, 0)
        },
        alunos = alunosComProgresso,
        desafios = estatisticasPorDesafio
    });
}

        // GET: api/Relatorio/aluno/{alunoId}
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> ObterProgressoAluno(int alunoId)
        {
            var aluno = await _context.Usuarios
                .Include(u => u.Progressos)
                    .ThenInclude(p => p.Desafio)
                .FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno == null)
                return NotFound(new { mensagem = "Aluno não encontrado" });

            var progressoDetalhado = aluno.Progressos
                .Where(p => p.Concluido)
                .Select(p => new
                {
                    desafioId = p.DesafioId,
                    titulo = p.Desafio.Titulo,
                    nivel = p.Desafio.Nivel,
                    pontos = p.Desafio.Pontos,
                    dataConclusao = p.DataConclusao
                })
                .OrderBy(p => p.dataConclusao)
                .ToList();

            var totalPontos = progressoDetalhado.Sum(p => p.pontos);
            var desafiosCompletos = progressoDetalhado.Count;
            var totalDesafios = await _context.Desafios.CountAsync();

            return Ok(new
            {
                aluno = new
                {
                    nome = aluno.Nome,
                    email = aluno.Email,
                    avatarId = aluno.AvatarId
                },
                resumo = new
                {
                    desafiosCompletos,
                    totalDesafios,
                    totalPontos,
                    percentualConclusao = totalDesafios > 0 
                        ? Math.Round((double)desafiosCompletos / totalDesafios * 100, 1) 
                        : 0
                },
                progresso = progressoDetalhado
            });
        }

        // GET: api/Relatorio/ranking/{turmaId}
        [HttpGet("ranking/{turmaId}")]
        public async Task<IActionResult> ObterRanking(int turmaId)
        {
            var turma = await _context.Turmas
                .Include(t => t.Alunos)
                    .ThenInclude(at => at.Aluno)
                        .ThenInclude(a => a.Progressos)
                            .ThenInclude(p => p.Desafio)
                .FirstOrDefaultAsync(t => t.Id == turmaId);

            if (turma == null)
                return NotFound(new { mensagem = "Turma não encontrada" });

            var ranking = turma.Alunos
                .Select(at => new
                {
                    alunoId = at.AlunoId,
                    nome = at.Aluno.Nome,
                    avatarId = at.Aluno.AvatarId,
                    desafiosCompletos = at.Aluno.Progressos.Count(p => p.Concluido),
                    totalPontos = at.Aluno.Progressos
                        .Where(p => p.Concluido)
                        .Sum(p => p.Desafio.Pontos)
                })
                .OrderByDescending(a => a.totalPontos)
                .ThenByDescending(a => a.desafiosCompletos)
                .Select((a, index) => new
                {
                    posicao = index + 1,
                    a.alunoId,
                    a.nome,
                    a.avatarId,
                    a.desafiosCompletos,
                    a.totalPontos
                })
                .ToList();

            return Ok(ranking);
        }
    }
}