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

        [HttpGet]
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
            List<Usina> lst = new List<Usina>();
            foreach (var item in _context.Usinas.ToList().OrderByDescending(u => u.UC))
            {
                lst.Add(new Usina
                {
                    ID = item.ID,
                    UC = item.UC,
                    Ativo = item.Ativo,
                    IDFornecedor = item.IDFornecedor,
                    Fornecedor = ObterFornecedorPeloId(item.IDFornecedor)
                });
            }
            return lst;
        }

        [HttpPost]
        public string Salvar(string UC, string Ativo, string IdFornecedor)
        {
            #region valida se a usina ja existe
            if (ExisteUsina(UC, Convert.ToInt32(IdFornecedor)))
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
            return "sucesso";
        }

        [HttpPost]
        public string SalvarAlteracoes(string UC, string Ativo, string IdFornecedor, string Id)
        {
            #region valida se a usina ja existe
            if (ExisteUsina(UC, Convert.ToInt32(IdFornecedor), Convert.ToInt32(Id)))
            {
                return "existe";
            }
            #endregion

            var usina = ObterUsinaPeloId(Convert.ToInt32(Id));

            usina.Ativo = Convert.ToBoolean(Ativo);
            usina.IDFornecedor = Convert.ToInt32(IdFornecedor);
            usina.UC = UC;

            _context.Update(usina);
            _context.SaveChanges();
            return "sucesso";
        }
        [HttpPost]
        public string Excluir(string id)
        {
            var usina = ObterUsinaPeloId(Convert.ToInt32(id));

            _context.Usinas.Remove(usina);
            _context.SaveChanges();
            return "sucesso";
        }
        private Usina ObterUsinaPeloId(int Id) { return _context.Usinas.FirstOrDefault(m => m.ID == Id); }
        private Fornecedor ObterFornecedorPeloId(int Id) { return _context.Fornecedores.FirstOrDefault(m => m.ID == Id); }

        private bool ExisteUsina(string UC, int idFornecedor)
        {
            var usina = _context.Usinas
                   .FirstOrDefault(m => m.UC == UC && m.IDFornecedor == idFornecedor);
            if (usina != null)
            {
                return true;
            }
            return false;
        }
        private bool ExisteUsina(string UC, int idFornecedor, int idUsina)
        {
            var usina = _context.Usinas
                   .FirstOrDefault(m => m.UC == UC && m.IDFornecedor == idFornecedor && m.ID != idUsina);
            if (usina != null)
            {
                return true;
            }
            return false;
        }
    }
}
