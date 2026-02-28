namespace AutoEscolaAPI.Models
{
    public class Aluno
    {
        // 1. Identificador Único (Chave Primária)
        public int Id { get; set; }

        // 2. Nome do Aluno (Texto opcional)
        public string? Nome { get; set; }

        // 3. CPF (Documento único para buscas eficazes)
        public string? CPF { get; set; }

        // 4. Categoria da Habilitação (A, B, C, D, E)
        public string? Categoria { get; set; }

        // 5. Data e Hora da Matrícula
        public DateTime DataMatricula { get; set; }

        // 6. Estado do aluno no sistema com valor padrão
        public string Status { get; set; } = "Ativo";
    }
}
