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
    public class SalariosController : Controller
    {
        private readonly Contexto _context;

        public SalariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Salarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Salarios.Include(s => s.Mes);
            return View(await contexto.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string procurar)
        {
            if (!String.IsNullOrWhiteSpace(procurar))
            {
                return View(_context.Salarios.Include(s => s.Mes).Where(s => s.Mes.Nome.Contains(procurar, StringComparison.OrdinalIgnoreCase)));
            }

            return View(await _context.Salarios.Include(s => s.Mes).ToListAsync());
        }

        // GET: Salarios/Create
        public IActionResult Create()
        {
            ViewData["IdMes"] = new SelectList(_context.Meses.Where(m => m.Id != m.Salario.IdMes), "Id", "Nome");
            return View();
        }

        // POST: Salarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdMes,Valor")] Salario salario)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "Salário cadastrado com sucesso.";
                _context.Add(salario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMes"] = new SelectList(_context.Meses.Where(m => m.Id != m.Salario.IdMes), "Id", "Nome", salario.IdMes);
            return View(salario);
        }

        // GET: Salarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salario = await _context.Salarios.FindAsync(id);
            if (salario == null)
            {
                return NotFound();
            }
            ViewData["IdMes"] = new SelectList(_context.Meses.Where(m => m.Id != m.Salario.IdMes), "Id", "Nome", salario.IdMes);
            return View(salario);
        }

        // POST: Salarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdMes,Valor")] Salario salario)
        {
            if (id != salario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salario);
                    await _context.SaveChangesAsync();
                    TempData["Confirmacao"] = "Salário atualizado com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarioExists(salario.Id))
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
            ViewData["IdMes"] = new SelectList(_context.Meses.Where(m => m.Id != m.Salario.IdMes), "Id", "Nome", salario.IdMes);
            return View(salario);
        }

        // POST: Salarios/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var salario = await _context.Salarios.FindAsync(id);
            _context.Salarios.Remove(salario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarioExists(int id)
        {
            return _context.Salarios.Any(e => e.Id == id);
        }
    }
}
