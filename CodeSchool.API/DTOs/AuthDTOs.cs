namespace CodeSchool.API.DTOs
{
    // DTO para registro de novo usuário
    public class RegistroDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = "Aluno"; // "Aluno" ou "Professor"
    }

    // DTO para login
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    // DTO de resposta após login/registro
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}