namespace CodeSchool.API.Models
{
    // Representa um usuário do sistema (Aluno, Professor ou Admin)
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty; // Senha criptografada
        public TipoUsuario Tipo { get; set; } // Enum para definir o tipo
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        // Avatar do aluno (1 a 8, cada número representa uma imagem)
        public int? AvatarId { get; set; }
        
        // Relacionamentos
        public List<Progresso> Progressos { get; set; } = new();
        public List<UsuarioBadge> Badges { get; set; } = new();
    }

    // Enum: tipo de usuário
    public enum TipoUsuario
    {
        Aluno = 1,
        Professor = 2,
        Admin = 3
    }
}