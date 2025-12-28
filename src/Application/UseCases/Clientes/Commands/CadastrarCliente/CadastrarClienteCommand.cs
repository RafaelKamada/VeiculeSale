using Application.UseCases.Clientes.DTOs;
using MediatR; 

namespace Application.UseCases.Clientes.Commands.CadastrarCliente
{
    public class CadastrarClienteCommand : IRequest<ClienteDto>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
