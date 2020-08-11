using System.Collections.Immutable;
using ExpenseManagement.EntityMap;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Salario> Salarios { get; set; }
        public DbSet<TipoDespesa> TiposDespesa { get; set; }

        public Contexto (DbContextOptions<Contexto> options) : base (options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DespesaMap());
            builder.ApplyConfiguration(new MesMap());
            builder.ApplyConfiguration(new SalarioMap());
            builder.ApplyConfiguration(new TipoDespesaMap());
        }
    }
}