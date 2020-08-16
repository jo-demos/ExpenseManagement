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
            EstatisticasViewModel estatisticas = new EstatisticasViewModel()
            {
                MenorDespesa = _Context.Despesas.Min(d => d.Valor),
                MaiorDespesa = _Context.Despesas.Max(d => d.Valor),
                QtdeDespesas = _Context.Despesas.Count()
            };

            return View(estatisticas);
        }
    }
}