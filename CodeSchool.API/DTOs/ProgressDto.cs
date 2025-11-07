namespace CodeSchool.API.DTOs
{
    // DTO para submeter solução de desafio
    public class SubmeterDesafioDto
    {
        public int DesafioId { get; set; }
        public string CodigoSolucao { get; set; } = string.Empty; // XML do Blockly
    }

    // DTO de resposta ao completar desafio
    public class ProgressoResponseDto
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public int PontosGanhos { get; set; }
        public List<string> BadgesNovas { get; set; } = new();
    }
}