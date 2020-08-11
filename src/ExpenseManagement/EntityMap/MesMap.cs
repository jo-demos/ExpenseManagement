using ExpenseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.EntityMap
{
    public class MesMap : IEntityTypeConfiguration<Mes>
    {
        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.ToTable("Mes").HasKey(m => m.Id);

            builder.Property(m => m.Id).ValueGeneratedNever();
            builder.Property(m => m.Nome).HasMaxLength(50).IsRequired();

            builder.HasMany(d => d.Despesas)
                .WithOne(m => m.Mes)
                .HasForeignKey(d => d.IdMes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}