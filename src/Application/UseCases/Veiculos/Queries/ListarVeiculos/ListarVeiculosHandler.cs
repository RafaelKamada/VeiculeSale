using Application.UseCases.Veiculos.DTOs;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Veiculos.Queries.ListarVeiculos
{
    public class ListarVeiculosHandler : IRequestHandler<ListarVeiculosQuery, IEnumerable<VeiculoDto>>
    {
        private readonly IVeiculoRepository _repository;

        public ListarVeiculosHandler(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VeiculoDto>> Handle(ListarVeiculosQuery request, CancellationToken cancellationToken)
        { 
            var veiculos = await _repository.ListarPorStatusAsync(request.Status);
             
            return veiculos.Select(v => VeiculoDto.FromEntity(v));
        }
    }
}
