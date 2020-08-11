using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Models
{
    public class TipoDespesa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "O máximo de caracteres permitido foi excedido")]
        [Remote(action: "TipoDespesaExists", controller: "TipoDespesas")]
        public string Nome { get; set; }

        public ICollection<Despesa> Despesas { get; set; }
    }
}