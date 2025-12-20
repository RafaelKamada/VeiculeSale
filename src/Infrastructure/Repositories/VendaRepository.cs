using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context; 

namespace Infrastructure.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VeiculeSaleDbContext _context;

        public VendaRepository(VeiculeSaleDbContext context)
        {
            _context = context;
        }

        public async Task<Venda?> ObterPorIdAsync(Guid id)
        { 
            return await _context.Vendas
                .Include(v => v.Veiculo)
                .Include(v => v.Pagamentos)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AdicionarAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        // Método especial para o Webhook
        public async Task<Pagamento?> ObterPagamentoPorCodigoExternoAsync(string codigoTransacao)
        {
            return await _context.Pagamentos
                .Include(p => p.Venda)  
                .ThenInclude(v => v.Veiculo)  
                .FirstOrDefaultAsync(p => p.CodigoTransacao == codigoTransacao);
        }
    }
}
