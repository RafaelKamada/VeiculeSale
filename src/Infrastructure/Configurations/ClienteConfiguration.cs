using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(14);  

            builder.Property(c => c.Email)
                .HasMaxLength(150);
             
            builder.HasIndex(c => c.Cpf)
                .IsUnique();
             
            builder.HasMany(c => c.Compras)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
