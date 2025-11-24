namespace CodeSchool.API.Models
{
    // Representa um desafio de lógica/programação
    public class Desafio
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Nivel { get; set; } // 1 = Fácil, 2 = Médio, 3 = Difícil
        public int Pontos { get; set; } // Pontos ganhos ao completar
        public int Ordem { get; set; } // Ordem de liberação (desafio 1, 2, 3...)
        
        // Configuração do Blockly (blocos disponíveis)
        public string BlocosDisponiveis { get; set; } = "move,turn,repeat"; 
        
        // Objetivo do desafio (ex: "mover 5 casas")
        public string Objetivo { get; set; } = string.Empty;
        
        // ========== NOVA PROPRIEDADE ==========
        // Configuração do grid em JSON (posição inicial, objetivo, etc)
        public string ConfiguracaoGrid { get; set; } = string.Empty;
        
        // Relacionamentos
        public List<Progresso> Progressos { get; set; } = new();
    }
}