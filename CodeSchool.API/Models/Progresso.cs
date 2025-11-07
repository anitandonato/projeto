namespace CodeSchool.API.Models
{
    // Registra o progresso do aluno em cada desafio
    public class Progresso
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int DesafioId { get; set; }
        public bool Concluido { get; set; } = false;
        public DateTime? DataConclusao { get; set; }
        public string? CodigoSolucao { get; set; } // XML do Blockly que o aluno usou
        
        // Relacionamentos
        public Usuario Aluno { get; set; } = null!;
        public Desafio Desafio { get; set; } = null!;
    }
}