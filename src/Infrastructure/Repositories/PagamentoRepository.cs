using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly VeiculeSaleDbContext _context;

        public PagamentoRepository(VeiculeSaleDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<Pagamento?> ObterPorCodigoTransacaoAsync(string codigoTransacao)
        {
            return await _context.Pagamentos
                .Include(p => p.Venda)  
                .ThenInclude(v => v.Veiculo)  
                .FirstOrDefaultAsync(p => p.CodigoTransacao == codigoTransacao);
        }
    }
}
