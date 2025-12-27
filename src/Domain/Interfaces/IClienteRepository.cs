using Domain.Entities; 

namespace Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(Cliente cliente);
        Task<Cliente?> ObterPorCpfAsync(string cpf);
        Task<Cliente?> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(Cliente cliente);
    }
}
