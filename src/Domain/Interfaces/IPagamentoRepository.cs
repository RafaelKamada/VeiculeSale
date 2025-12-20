using Domain.Entities; 

namespace Domain.Interfaces
{
    public interface IPagamentoRepository
    {
        Task AdicionarAsync(Pagamento pagamento);
        Task AtualizarAsync(Pagamento pagamento); 
        Task<Pagamento?> ObterPorCodigoTransacaoAsync(string codigoTransacao);
    }
}
