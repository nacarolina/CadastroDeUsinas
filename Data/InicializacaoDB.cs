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
            new Fornecedor{Nome="SOLARIAN"},
            new Fornecedor{Nome="FUTURA"},
            new Fornecedor{Nome="CENTRAL GERADORA FAZENDA MODELO"},
            new Fornecedor{Nome="NOVA MUNDO"},
            new Fornecedor{Nome="SOLARE"},
            new Fornecedor{Nome="UNISOL"}
            };
            foreach (Fornecedor s in fornecedores)
            {
                context.Fornecedores.Add(s);
            }
            context.SaveChanges();
        }
    }
}
