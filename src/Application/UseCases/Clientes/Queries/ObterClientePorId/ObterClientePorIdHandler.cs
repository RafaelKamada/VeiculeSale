using Application.UseCases.Clientes.DTOs;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Clientes.Queries.ObterClientePorId
{
    public class ObterClientePorIdHandler : IRequestHandler<ObterClientePorIdQuery, ClienteDto>
    {
        private readonly IClienteRepository _repository;

        public ObterClientePorIdHandler(IClienteRepository repository)
        {
            _repository = repository;  
        }

        public async Task<ClienteDto> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.ObterPorIdAsync(request.Id);

            if (cliente == null) return null;

            return ClienteDto.FromEntity(cliente);
        }
    }
}
