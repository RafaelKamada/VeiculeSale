using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Configurations
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.CodigoTransacao)
                .HasMaxLength(100);

            builder.Property(p => p.Status)
                .IsRequired();
             
            builder.HasIndex(p => p.CodigoTransacao);
        }
    }
}
