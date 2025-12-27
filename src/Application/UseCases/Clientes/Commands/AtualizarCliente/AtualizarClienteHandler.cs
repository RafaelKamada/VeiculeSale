using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Clientes.Commands.AtualizarCliente
{
    public class AtualizarClienteHandler : IRequestHandler<AtualizarClienteCommand, bool>
    {
        private readonly IClienteRepository _repository;

        public AtualizarClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.ObterPorIdAsync(request.Id);

            if (cliente == null) throw new Exception("Cliente não encontrado.");
             
            cliente.AtualizarDados(request.Nome, request.Email, request.Telefone);
             
            await _repository.AtualizarAsync(cliente);

            return true;
        }
    }
}
