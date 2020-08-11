using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.Models
{
    public class Salario
    {
        public int Id { get; set; }
        public int IdMes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor inválido")]
        public double Valor { get; set; }

        public Mes Mes { get; set; }
    }
}