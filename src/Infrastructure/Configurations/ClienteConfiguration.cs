using Domain.Entities;
using Domain.ValueObjects;
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
                .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                 .HasMaxLength(20);

            builder.Property(c => c.Email)
            .HasConversion(
                email => email.Address,     
                address => new Email(address)  
            )
            .HasColumnName("Email")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(c => c.Cpf)
            .HasConversion(
                cpf => cpf.Numero,           
                numero => new Cpf(numero)    
            )
            .HasColumnName("Cpf")
            .HasMaxLength(11)
            .IsFixedLength()
            .IsRequired();
             
            builder.HasIndex(c => c.Cpf).IsUnique();
        }
    }
}
