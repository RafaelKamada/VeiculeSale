using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Context; 

namespace Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly VeiculeSaleDbContext _context;

        public VeiculoRepository(VeiculeSaleDbContext context)
        {
            _context = context;
        }

        public async Task<Veiculo?> ObterPorIdAsync(Guid id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task<IEnumerable<Veiculo>> ListarTodosAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ListarPorStatusAsync(VeiculoStatus status)
        {
            return await _context.Veiculos
                .Where(v => v.Status == status)
                .OrderBy(v => v.Preco)  
                .ToListAsync();
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
