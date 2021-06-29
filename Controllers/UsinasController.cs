using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDeUsinas.Data;
using CadastroDeUsinas.Models;

namespace CadastroDeUsinas.Controllers
{
    public class UsinasController : Controller
    {
        private readonly UsinasContext _context;

        public UsinasController(UsinasContext context)
        {
            _context = context;
        }

        // GET: Usinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usinas.ToListAsync());
        }

        // GET: Usinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usina = await _context.Usinas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usina == null)
            {
                return NotFound();
            }

            return View(usina);
        }

        // GET: Usinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UC,Ativo,IDFornecedor")] Usina usina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usina);
        }

        // GET: Usinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usina = await _context.Usinas.FindAsync(id);
            if (usina == null)
            {
                return NotFound();
            }
            return View(usina);
        }

        // POST: Usinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UC,Ativo,IDFornecedor")] Usina usina)
        {
            if (id != usina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsinaExists(usina.ID))
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
            return View(usina);
        }

        // GET: Usinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usina = await _context.Usinas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usina == null)
            {
                return NotFound();
            }

            return View(usina);
        }

        // POST: Usinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usina = await _context.Usinas.FindAsync(id);
            _context.Usinas.Remove(usina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsinaExists(int id)
        {
            return _context.Usinas.Any(e => e.ID == id);
        }
    }
}
