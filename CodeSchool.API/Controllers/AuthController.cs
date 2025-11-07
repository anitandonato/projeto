using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeSchool.API.Data;
using CodeSchool.API.DTOs;
using CodeSchool.API.Models;
using CodeSchool.API.Services;
namespace CodeSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // POST: api/auth/registro
        [HttpPost("registro")]
        public async Task<ActionResult<AuthResponseDto>> Registrar([FromBody] RegistroDto dto)
        {
            // Verificar se email já existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(new { mensagem = "Email já cadastrado" });
            }

            // Criar novo usuário
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = _authService.HashSenha(dto.Senha),
                Tipo = dto.TipoUsuario == "Professor" ? TipoUsuario.Professor : TipoUsuario.Aluno,
                AvatarId = dto.TipoUsuario == "Aluno" ? 1 : null // Avatar padrão para aluno
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Gerar token
            var token = _authService.GerarToken(usuario);

            return Ok(new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Tipo = usuario.Tipo.ToString(),
                Token = token
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !_authService.VerificarSenha(dto.Senha, usuario.SenhaHash))
            {
                return Unauthorized(new { mensagem = "Email ou senha incorretos" });
            }

            var token = _authService.GerarToken(usuario);

            return Ok(new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Tipo = usuario.Tipo.ToString(),
                Token = token
            });
        }
    }
}