namespace CodeSchool.API.Models
{
    // Representa uma turma criada pelo professor
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty; // Código único para alunos entrarem
        public int ProfessorId { get; set; }
        
        // Relacionamentos
        public Usuario Professor { get; set; } = null!;
        public List<AlunoTurma> Alunos { get; set; } = new();
    }

    // Tabela intermediária (muitos-para-muitos: Alunos <-> Turmas)
    public class AlunoTurma
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        
        public Usuario Aluno { get; set; } = null!;
        public Turma Turma { get; set; } = null!;
    }
}