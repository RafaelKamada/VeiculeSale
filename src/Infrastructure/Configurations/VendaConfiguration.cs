using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Configurations
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.ValorTotal)
                .HasPrecision(18, 2) 
                .IsRequired();

            builder.Property(v => v.Status)
                .IsRequired(); 
             
            builder.HasOne(v => v.Veiculo)
                .WithMany()  
                .HasForeignKey(v => v.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);
             
            builder.HasMany(v => v.Pagamentos)
                .WithOne(p => p.Venda)
                .HasForeignKey(p => p.VendaId);
        }
    }
}
