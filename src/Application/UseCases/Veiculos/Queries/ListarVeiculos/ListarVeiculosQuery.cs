using Application.UseCases.Veiculos.DTOs;
using Domain.Enums;
using MediatR; 

namespace Application.UseCases.Veiculos.Queries.ListarVeiculos
{
    public class ListarVeiculosQuery : IRequest<IEnumerable<VeiculoDto>>
    { 
        public VeiculoStatus Status { get; set; }

        public ListarVeiculosQuery(VeiculoStatus status)
        {
            Status = status;
        }
    }
}
