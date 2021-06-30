using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDeUsinas.Data;
using CadastroDeUsinas.Models;
using System.Collections;

namespace CadastroDeUsinas.Controllers
{
    public class UsinasController : Controller
    {
        private readonly UsinasContext _context;

        public UsinasController(UsinasContext context)
        {
            _context = context;
        }
        public List<Fornecedor> ObterFornecedores()
        {
            return _context.Fornecedores.ToList();
        }

        // GET: Usinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usinas.ToListAsync());
        }

        [HttpGet]
        public List<Usina> ObterUsinas()
        {
            return  _context.Usinas.ToList();
        }

        [HttpPost]
        public string Salvar(string UC, string Ativo, string IdFornecedor)
        {
            #region valida se a usina ja existe
            var usina = _context.Usinas
                   .FirstOrDefault(m => m.UC == UC && m.IDFornecedor.ToString() == IdFornecedor);
            if (usina != null)
            {
                return "existe";
            }
            #endregion

            _context.Add(new Usina
            {
                UC = UC,
                Ativo = Convert.ToBoolean(Ativo),
                IDFornecedor = Convert.ToInt32(IdFornecedor)
            });

            _context.SaveChanges();
            return "";
        }

        [HttpPost]
        public string SalvarAlteracoes(string UC, string Ativo, string IdFornecedor, string Id)
        {
            #region valida se a usina ja existe
            var ExisteUsina = _context.Usinas
                   .FirstOrDefault(m => m.UC == UC && m.IDFornecedor.ToString() == IdFornecedor && m.ID != Convert.ToInt32(Id));
            if (ExisteUsina != null)
            {
                return "existe";
            }
            #endregion

            var usina = _context.Usinas.FirstOrDefault(m => m.ID == Convert.ToInt32(Id));

            usina.Ativo = Convert.ToBoolean(Ativo);
            usina.IDFornecedor = Convert.ToInt32(IdFornecedor);
            usina.UC = UC;

            _context.Update(usina);
            _context.SaveChanges();
            return "";
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
    }
}
