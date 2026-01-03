using Application.UseCases.Vendas.DTOs;
using MediatR; 

namespace Application.UseCases.Vendas.Commands.RealizarVenda
{
    public class RealizarVendaCommand : IRequest<VendaRealizadaDto>
    {
        public Guid VeiculoId { get; set; }
        public Guid ClienteId { get; set; }
    }
}
