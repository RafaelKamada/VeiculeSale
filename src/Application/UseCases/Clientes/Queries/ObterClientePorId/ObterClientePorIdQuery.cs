using Application.UseCases.Clientes.DTOs;
using MediatR; 

namespace Application.UseCases.Clientes.Queries.ObterClientePorId
{
    public class ObterClientePorIdQuery : IRequest<ClienteDto>
    {
        public Guid Id { get; set; }

        public ObterClientePorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
