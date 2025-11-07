namespace CodeSchool.API.Models
{
    // Representa uma conquista/medalha
    public class Badge
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Icone { get; set; } = string.Empty; // Emoji ou URL da imagem
        public string Condicao { get; set; } = string.Empty; // Ex: "complete_5_desafios"
        
        // Relacionamentos
        public List<UsuarioBadge> Usuarios { get; set; } = new();
    }

    // Tabela intermediária (muitos-para-muitos: Usuários <-> Badges)
    public class UsuarioBadge
    {
        public int UsuarioId { get; set; }
        public int BadgeId { get; set; }
        public DateTime DataConquista { get; set; } = DateTime.Now;
        
        public Usuario Usuario { get; set; } = null!;
        public Badge Badge { get; set; } = null!;
    }
}