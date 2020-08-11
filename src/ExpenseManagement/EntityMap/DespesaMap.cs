using System.Collections.Immutable;
using ExpenseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.EntityMap
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesa").HasKey(d => d.Id);

            builder.Property(d => d.Valor).IsRequired();

            builder.HasOne(d => d.Mes)
                .WithMany(m => m.Despesas)
                .HasForeignKey(d => d.IdMes);

            builder.HasOne(d => d.TipoDespesa)
                .WithMany(t => t.Despesas)
                .HasForeignKey(d => d.IdTipoDespesa);
        }
    }
}