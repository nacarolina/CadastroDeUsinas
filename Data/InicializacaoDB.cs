using CadastroDeUsinas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeUsinas.Data
{
    public class InicializacaoDB
    {
        public static void Initialize(UsinasContext context)
        {
            context.Database.EnsureCreated();

            // Obtem qualquer fornecedor
            if (context.Fornecedores.Any())
            {
                return;   // Banco já está criado
            }

            var fornecedores = new Fornecedor[]
            {
            new Fornecedor{ID=1,Nome="SOLARIAN"},
            new Fornecedor{ID=2,Nome="FUTURA"},
            new Fornecedor{ID=3,Nome="CENTRAL GERADORA FAZENDA MODELO"},
            new Fornecedor{ID=4,Nome="NOVA MUNDO"},
            new Fornecedor{ID=5,Nome="SOLARE"},
            new Fornecedor{ID=6,Nome="UNISOL"}
            };
            foreach (Fornecedor s in fornecedores)
            {
                context.Fornecedores.Add(s);
            }
            context.SaveChanges();
        }
    }
}
