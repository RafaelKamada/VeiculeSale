using Domain.Entities; 

namespace Application.UseCases.Clientes.DTOs
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public static ClienteDto FromEntity(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,  
                Email = cliente.Email,
                Telefone = cliente.Telefone
            };
        }
    }
}
