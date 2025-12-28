using Application.UseCases.Clientes.DTOs;
using MediatR;

namespace Application.UseCases.Clientes.Commands.AtualizarCliente
{
    public class AtualizarClienteCommand : IRequest<ClienteDto>
    {
        public Guid Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
