using Application.UseCases.Vendas.DTOs;
using MediatR; 

namespace Application.UseCases.Vendas.Commands.RealizarVenda
{
    public class RealizarVendaCommand : IRequest<VendaRealizadaDto>
    {
        public Guid VeiculoId { get; set; } 
        public string ClienteCpf { get; set; } = string.Empty;
        public string ClienteNome { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;
        public string ClienteTelefone { get; set; } = string.Empty;
    }
}
