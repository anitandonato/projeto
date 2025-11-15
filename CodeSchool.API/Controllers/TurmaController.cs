using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.Models;
using System.Security.Claims;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurmaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/turma/criar
        [HttpPost("criar")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> CriarTurma([FromBody] CriarTurmaDto dto)
        {
            var professorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Gerar código único de 6 caracteres
            var codigo = GerarCodigoUnico();

            var turma = new Turma
            {
                Nome = dto.Nome,
                Codigo = codigo,
                ProfessorId = professorId
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                turma.Id,
                turma.Nome,
                turma.Codigo,
                Mensagem = "Turma criada com sucesso!"
            });
        }

        // GET: api/turma/minhas-turmas
        [HttpGet("minhas-turmas")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> MinhasTurmas()
        {
            var professorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var turmas = await _context.Turmas
                .Where(t => t.ProfessorId == professorId)
                .Select(t => new
                {
                    t.Id,
                    t.Nome,
                    t.Codigo,
                    TotalAlunos = t.Alunos.Count
                })
                .ToListAsync();

            return Ok(turmas);
        }

        // GET: api/turma/{id}/alunos
        [HttpGet("{id}/alunos")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> ObterAlunosDaTurma(int id)
        {
            var professorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var turma = await _context.Turmas
                .Include(t => t.Alunos)
                    .ThenInclude(at => at.Aluno)
                        .ThenInclude(a => a.Progressos)
                            .ThenInclude(p => p.Desafio)
                .FirstOrDefaultAsync(t => t.Id == id && t.ProfessorId == professorId);

            if (turma == null)
            {
                return NotFound(new { mensagem = "Turma não encontrada" });
            }

            var alunos = turma.Alunos.Select(at => new
            {
                at.Aluno.Id,
                at.Aluno.Nome,
                at.Aluno.Email,
                DesafiosCompletos = at.Aluno.Progressos.Count(p => p.Concluido),
                TotalPontos = at.Aluno.Progressos
                    .Where(p => p.Concluido)
                    .Sum(p => p.Desafio.Pontos)
            }).ToList();

            return Ok(new
            {
                turma.Id,
                turma.Nome,
                turma.Codigo,
                Alunos = alunos
            });
        }

        // POST: api/turma/entrar
        [HttpPost("entrar")]
        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> EntrarNaTurma([FromBody] EntrarTurmaDto dto)
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var turma = await _context.Turmas
                .FirstOrDefaultAsync(t => t.Codigo == dto.Codigo.ToUpper());

            if (turma == null)
            {
                return NotFound(new { mensagem = "Código de turma inválido" });
            }

            // Verificar se já está na turma
            var jaEstaNaTurma = await _context.AlunosTurmas
                .AnyAsync(at => at.AlunoId == alunoId && at.TurmaId == turma.Id);

            if (jaEstaNaTurma)
            {
                return BadRequest(new { mensagem = "Você já está nesta turma" });
            }

            // Adicionar aluno à turma
            var alunoTurma = new AlunoTurma
            {
                AlunoId = alunoId,
                TurmaId = turma.Id
            };

            _context.AlunosTurmas.Add(alunoTurma);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                turma.Nome,
                Mensagem = $"Você entrou na turma {turma.Nome}!"
            });
        }

        // GET: api/turma/minhas-turmas-aluno
        [HttpGet("minhas-turmas-aluno")]
        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> MinhasTurmasAluno()
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var turmas = await _context.AlunosTurmas
                .Where(at => at.AlunoId == alunoId)
                .Include(at => at.Turma)
                    .ThenInclude(t => t.Professor)
                .Select(at => new
                {
                    at.Turma.Id,
                    at.Turma.Nome,
                    at.Turma.Codigo,
                    Professor = at.Turma.Professor.Nome
                })
                .ToListAsync();

            return Ok(turmas);
        }

        private string GerarCodigoUnico()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string codigo;

            do
            {
                codigo = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (_context.Turmas.Any(t => t.Codigo == codigo));

            return codigo;
        }
    }

    // DTOs
    public class CriarTurmaDto
    {
        public string Nome { get; set; } = string.Empty;
    }

    public class EntrarTurmaDto
    {
        public string Codigo { get; set; } = string.Empty;
    }
}