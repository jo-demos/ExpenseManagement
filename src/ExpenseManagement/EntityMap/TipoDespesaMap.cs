using ExpenseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.EntityMap
{
    public class TipoDespesaMap : IEntityTypeConfiguration<TipoDespesa>
    {
        public void Configure(EntityTypeBuilder<TipoDespesa> builder)
        {
            builder.ToTable("TipoDespesas").HasKey(t => t.Id);

            builder.Property(t => t.Nome).HasMaxLength(50).IsRequired();

            builder.HasMany(t => t.Despesas)
                .WithOne(d => d.TipoDespesa)
                .HasForeignKey(d => d.IdTipoDespesa);
        }
    }
}