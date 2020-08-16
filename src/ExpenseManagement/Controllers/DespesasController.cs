using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseManagement.Models;
using X.PagedList;
using ExpenseManagement.ViewModel;

namespace ExpenseManagement.Controllers
{
    public class DespesasController : Controller
    {
        private readonly Contexto _context;

        public DespesasController(Contexto context)
        {
            _context = context;
        }

        // GET: Despesas
        public async Task<IActionResult> Index(int pagina = 1)
        {
            const int itensPag = 10;

            ViewData["Meses"] = new SelectList(_context.Meses.Where(m => m.Id == m.Salario.IdMes), "Id", "Nome");

            var contexto = _context.Despesas.Include(d => d.Mes).Include(d => d.TipoDespesa).OrderBy(d => d.IdMes);
            return View(await contexto.ToPagedListAsync(pagina, itensPag));
        }

        // GET: Despesas/Create
        public IActionResult Create()
        {
            ViewData["IdMes"] = new SelectList(_context.Meses, "Id", "Nome");
            ViewData["IdTipoDespesa"] = new SelectList(_context.TiposDespesa, "Id", "Nome");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdMes,IdTipoDespesa,Valor")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "Registro cadastrado com sucesso.";
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "Id", "Nome", despesa.IdMes);
            ViewData["IdTipoDespesa"] = new SelectList(_context.TiposDespesa, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "Id", "Nome", despesa.IdMes);
            ViewData["IdTipoDespesa"] = new SelectList(_context.TiposDespesa, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdMes,IdTipoDespesa,Valor")] Despesa despesa)
        {
            if (id != despesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = "Registro atualizado com sucesso.";
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "Id", "Nome", despesa.IdMes);
            ViewData["IdTipoDespesa"] = new SelectList(_context.TiposDespesa, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);
            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaExists(int id)
        {
            return _context.Despesas.Any(e => e.Id == id);
        }

        public JsonResult GastosTotaisMes(int idMes)
        {
            var gastos = new GastosTotaisMesViewModel()
            {
                ValorTotalGasto = _context.Despesas.Where(d => d.Mes.Id == idMes).Sum(d => d.Valor),
                Salario = _context.Salarios.Where(s => s.Mes.Id == idMes).Select(s => s.Valor).FirstOrDefault()
            };

            return Json(gastos);
        }

        public JsonResult GastosMes(int idMes)
        {
            var gastosMes = from despesas in _context.Despesas
                            where despesas.Mes.Id == idMes
                            group despesas by despesas.TipoDespesa.Nome into g
                            select new
                            {
                                TiposDespesas = g.Key,
                                Valores = g.Sum(d => d.Valor)
                            };

            return Json(gastosMes);
        }

        public JsonResult GastosTotais()
        {
            var gastosTotais = from despesas in _context.Despesas
                               group despesas by despesas.TipoDespesa.Nome into g
                               select new
                               {
                                   NomeMeses = g.Key,
                                   Valores = g.Sum(d => d.Valor)
                               };
            // var gastosTotais = _context.Despesas
            //     .GroupBy(d => d.Mes.Nome)
            //     .Select(d => new
            //     {
            //         NomeMeses = d.Select(x => x.Mes.Nome).Distinct(),
            //         Valores = d.Sum(x => x.Valor)
            //     }).ToList();
            //.OrderBy(d => d.IdMes)
            return Json(gastosTotais);
        }
    }
}
