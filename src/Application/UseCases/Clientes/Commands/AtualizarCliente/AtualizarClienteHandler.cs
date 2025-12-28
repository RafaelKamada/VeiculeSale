using Application.UseCases.Clientes.DTOs;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Clientes.Commands.AtualizarCliente
{
    public class AtualizarClienteHandler : IRequestHandler<AtualizarClienteCommand, ClienteDto>
    {
        private readonly IClienteRepository _repository;

        public AtualizarClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteDto> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.ObterPorIdAsync(request.Id);

            if (cliente == null) throw new KeyNotFoundException("Cliente não encontrado.");
             
            cliente.AtualizarDados(request.Nome, request.Email, request.Telefone);
             
            await _repository.AtualizarAsync(cliente);

            return ClienteDto.FromEntity(cliente);
        }
    }
}
