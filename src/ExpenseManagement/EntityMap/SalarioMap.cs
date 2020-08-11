using ExpenseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.EntityMap
{
    public class SalarioMap : IEntityTypeConfiguration<Salario>
    {
        public void Configure(EntityTypeBuilder<Salario> builder)
        {
            builder.ToTable("Salario").HasKey(s => s.Id);

            builder.Property(s => s.Valor).IsRequired();

            builder.HasOne(s => s.Mes)
                .WithOne(m => m.Salario)
                .HasForeignKey<Salario>(s => s.IdMes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}