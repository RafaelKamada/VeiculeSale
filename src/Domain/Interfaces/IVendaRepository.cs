using Domain.Entities; 

namespace Domain.Interfaces
{
    public interface IVendaRepository
    {
        Task<Venda?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Venda venda);
        Task AtualizarAsync(Venda venda);         
        Task<Pagamento?> ObterPagamentoPorCodigoExternoAsync(string codigoTransacao);
    }
}
