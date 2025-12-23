using MediatR; 

namespace Application.UseCases.Vendas.Commands.RealizarVenda
{
    public class RealizarVendaCommand : IRequest<Guid>
    {
        public Guid VeiculoId { get; set; }

        // Dados do Cliente (Simplificado para o teste)
        public string ClienteCpf { get; set; } = string.Empty;
        public string ClienteNome { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;
        public string ClienteTelefone { get; set; } = string.Empty;
    }
}
