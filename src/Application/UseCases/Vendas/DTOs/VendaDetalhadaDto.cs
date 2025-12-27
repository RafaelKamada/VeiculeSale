
namespace Application.UseCases.Vendas.DTOs
{
    public class VendaDetalhadaDto
    {
        public Guid VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }         
        public VeiculoResumoDto Veiculo { get; set; }
        public ClienteResumoDto Cliente { get; set; }
    }

    public class VeiculoResumoDto
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
    }

    public class ClienteResumoDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}
