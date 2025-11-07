using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.DTOs;
using CodeSchool.API.Models;
using CodeSchool.API.Services;
using System.Security.Claims;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Aluno")]
    public class ProgressoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GameService _gameService;

        public ProgressoController(AppDbContext context, GameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        // POST: api/progresso/submeter
        [HttpPost("submeter")]
        public async Task<ActionResult<ProgressoResponseDto>> SubmeterDesafio([FromBody] SubmeterDesafioDto dto)
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            var desafio = await _context.Desafios.FindAsync(dto.DesafioId);
            if (desafio == null)
            {
                return NotFound(new { mensagem = "Desafio não encontrado" });
            }

            // Verificar se já completou
            var progressoExistente = await _context.Progressos
                .FirstOrDefaultAsync(p => p.AlunoId == alunoId && p.DesafioId == dto.DesafioId);

            if (progressoExistente?.Concluido == true)
            {
                return BadRequest(new { mensagem = "Desafio já completado" });
            }

            // Validação simplificada (você pode melhorar isso)
            bool sucesso = ValidarSolucao(dto.CodigoSolucao, desafio.Objetivo);

            if (!sucesso)
            {
                return Ok(new ProgressoResponseDto
                {
                    Sucesso = false,
                    Mensagem = "Solução incorreta. Tente novamente!",
                    PontosGanhos = 0
                });
            }

            // Registrar progresso
            if (progressoExistente == null)
            {
                var progresso = new Progresso
                {
                    AlunoId = alunoId,
                    DesafioId = dto.DesafioId,
                    Concluido = true,
                    DataConclusao = DateTime.Now,
                    CodigoSolucao = dto.CodigoSolucao
                };
                _context.Progressos.Add(progresso);
            }
            else
            {
                progressoExistente.Concluido = true;
                progressoExistente.DataConclusao = DateTime.Now;
                progressoExistente.CodigoSolucao = dto.CodigoSolucao;
            }

            await _context.SaveChangesAsync();

            // Verificar badges
            var badgesNovas = await _gameService.VerificarBadges(alunoId);

            return Ok(new ProgressoResponseDto
            {
                Sucesso = true,
                Mensagem = "Parabéns! Desafio completado! 🎉",
                PontosGanhos = desafio.Pontos,
                BadgesNovas = badgesNovas.Select(b => b.Nome).ToList()
            });
        }

        // GET: api/progresso/dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> ObterDashboard()
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            var aluno = await _context.Usuarios
                .Include(u => u.Progressos)
                    .ThenInclude(p => p.Desafio)
                .Include(u => u.Badges)
                    .ThenInclude(ub => ub.Badge)
                .FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno == null)
            {
                return NotFound();
            }

            var pontosTotal = aluno.Progressos
                .Where(p => p.Concluido)
                .Sum(p => p.Desafio.Pontos);

            var nivel = _gameService.CalcularNivel(pontosTotal);

            return Ok(new
            {
                aluno.Nome,
                aluno.AvatarId,
                Pontos = pontosTotal,
                Nivel = nivel,
                DesafiosCompletos = aluno.Progressos.Count(p => p.Concluido),
                Badges = aluno.Badges.Select(ub => new
                {
                    ub.Badge.Nome,
                    ub.Badge.Icone,
                    ub.Badge.Descricao
                })
            });
        }

        // Validação simplificada da solução
        private bool ValidarSolucao(string codigo, string objetivo)
        {
            // Aqui você pode implementar lógica mais complexa
            // Por enquanto, vamos aceitar qualquer código que contenha os blocos necessários
            return codigo.Contains("block") && codigo.Length > 10;
        }
    }
}