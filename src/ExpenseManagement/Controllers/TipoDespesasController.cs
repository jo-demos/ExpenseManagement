using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseManagement.Models;

namespace ExpenseManagement.Controllers
{
    public class TipoDespesasController : Controller
    {
        private readonly Contexto _context;

        public TipoDespesasController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoDespesa
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposDespesa.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string procurar)
        {
            if (!String.IsNullOrWhiteSpace(procurar))
            {
                return View(await _context.TiposDespesa.Where(t => t.Nome.Contains(procurar, StringComparison.OrdinalIgnoreCase)).ToListAsync().ConfigureAwait(false));
            }

            return View(await _context.TiposDespesa.ToListAsync());
        }

        // GET: TipoDespesa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDespesa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                TempData["Conformacao"] = tipoDespesa.Nome + " foi cadastrado com sucesso.";

                _context.Add(tipoDespesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDespesa = await _context.TiposDespesa.FindAsync(id);
            if (tipoDespesa == null)
            {
                return NotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoDespesa tipoDespesa)
        {
            if (id != tipoDespesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Conformacao"] = tipoDespesa.Nome + " foi atualizado com sucesso.";

                    _context.Update(tipoDespesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TipoDespesaExistsAsync(tipoDespesa.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesa/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tipoDespesa = await _context.TiposDespesa.FindAsync(id);
            TempData["Conformacao"] = tipoDespesa.Nome + " foi excluído com sucesso.";
            _context.TiposDespesa.Remove(tipoDespesa);
            await _context.SaveChangesAsync();
            return Json(TempData["Conformacao"]);
        }

        private async Task<bool> TipoDespesaExistsAsync(int id)
        {
            return await _context.TiposDespesa.AnyAsync(e => e.Id == id);
        }

        public async Task<JsonResult> TipoDespesaExistsAsync(string nome)
        {
            if (await _context.TiposDespesa.AnyAsync(e => string.Equals(e.Nome, nome, StringComparison.OrdinalIgnoreCase)))
            {
                return Json($"Tipo de despesa '{nome}' já existente");
            }
            return Json(true);
        }
    }
}
