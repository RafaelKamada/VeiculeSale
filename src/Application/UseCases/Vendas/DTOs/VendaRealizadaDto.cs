using Domain.Entities; 

namespace Application.UseCases.Vendas.DTOs
{
    public class VendaRealizadaDto
    {
        public Guid Id { get; set; }
        public Guid VeiculoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public string CodigoTransacaoPagamento { get; set; }  

        public static VendaRealizadaDto FromEntity(Venda venda, Pagamento pagamento)
        {
            return new VendaRealizadaDto
            {
                Id = venda.Id,
                VeiculoId = venda.VeiculoId,
                ClienteId = venda.ClienteId,
                ValorTotal = venda.ValorTotal,
                Status = venda.Status.ToString(),
                CodigoTransacaoPagamento = pagamento.CodigoTransacao
            };
        }
    }
}
