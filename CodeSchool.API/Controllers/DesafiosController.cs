using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using System.Security.Claims;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requer autenticação
    public class DesafiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DesafiosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/desafios
        [HttpGet]
        public async Task<IActionResult> ObterDesafios()
        {
            var alunoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            // Buscar todos os desafios com informação de conclusão
            var desafios = await _context.Desafios
                .OrderBy(d => d.Ordem)
                .Select(d => new
                {
                    d.Id,
                    d.Titulo,
                    d.Descricao,
                    d.Nivel,
                    d.Pontos,
                    d.Ordem,
                    Concluido = _context.Progressos.Any(p => 
                        p.DesafioId == d.Id && 
                        p.AlunoId == alunoId && 
                        p.Concluido
                    )
                })
                .ToListAsync();

            return Ok(desafios);
        }

        // GET: api/desafios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDesafio(int id)
        {
            var desafio = await _context.Desafios.FindAsync(id);
            
            if (desafio == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                desafio.Id,
                desafio.Titulo,
                desafio.Descricao,
                desafio.Nivel,
                desafio.Pontos,
                desafio.BlocosDisponiveis,
                desafio.Objetivo
            });
        }
    }
}