using System.Collections.Generic;

namespace ExpenseManagement.Models
{
    public class Mes
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public ICollection<Despesa> Despesas { get; set; }

        public Salario Salario { get; set; }
    }
}