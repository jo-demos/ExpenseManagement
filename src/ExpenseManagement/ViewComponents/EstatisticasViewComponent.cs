using System.Linq;
using System.Threading.Tasks;
using ExpenseManagement.Models;
using ExpenseManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.ViewComponents
{
    public class EstatisticasViewComponent : ViewComponent
    {
        private readonly Contexto _Context;

        public EstatisticasViewComponent(Contexto context)
        {
            _Context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int count = _Context.Despesas.Count();
            EstatisticasViewModel estatisticas = new EstatisticasViewModel();

            if (count > 0)
            {
                estatisticas.MenorDespesa = _Context.Despesas.Min(d => d.Valor);
                estatisticas.MaiorDespesa = _Context.Despesas.Max(d => d.Valor);
                estatisticas.QtdeDespesas = count;
            };

            return View(estatisticas);
        }
    }
}