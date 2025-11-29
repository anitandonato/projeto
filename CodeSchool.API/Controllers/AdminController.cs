using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.API.Services;

namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly GameService _gameService;

        public AdminController(GameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Admin/recalcular-avatares
        [HttpGet("recalcular-avatares")]
        public async Task<IActionResult> RecalcularAvatares()
        {
            var atualizados = await _gameService.RecalcularTodosAvatares();

            return Ok(new
            {
                mensagem = $"Avatares recalculados com sucesso!",
                alunosAtualizados = atualizados
            });
        }
    }
}
