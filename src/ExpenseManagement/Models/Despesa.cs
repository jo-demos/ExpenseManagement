using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.Models
{
    public class Despesa
    {
        public int Id { get; set; }

        public int IdMes { get; set; }

        public Mes Mes { get; set; }

        public int IdTipoDespesa { get; set; }

        public TipoDespesa TipoDespesa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor inválido")]
        public double Valor { get; set; }
    }
}