using CadastroDeUsinas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeUsinas.Data
{
    public class UsinasContext : DbContext
    {
        public UsinasContext(DbContextOptions<UsinasContext> options) : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Usina> Usinas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedor");
            modelBuilder.Entity<Usina>().ToTable("Usina");
        }
    }
}
