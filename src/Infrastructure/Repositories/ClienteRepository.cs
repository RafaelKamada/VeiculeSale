using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly VeiculeSaleDbContext _context;

        public ClienteRepository(VeiculeSaleDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> ObterPorCpfAsync(string cpf)
        {
            Cpf cpfVo;

            try
            {
                cpfVo = new Cpf(cpf);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao processar CPF: {ex.Message}");
            }

            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpfVo);
        }

        public async Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
