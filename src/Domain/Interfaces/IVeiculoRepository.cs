using Domain.Entities;
using Domain.Enums; 

namespace Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<Veiculo?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Veiculo>> ListarTodosAsync();
         
        Task<IEnumerable<Veiculo>> ListarPorStatusAsync(VeiculoStatus status);

        Task AdicionarAsync(Veiculo veiculo);
        Task AtualizarAsync(Veiculo veiculo); 
    }
}
