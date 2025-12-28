using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests
{
    public class VeiculoRepositoryTests
    {
        [Fact]
        public async Task ListarPorStatusAsync_RetornaOrdenadoPorPreco()
        {
            var options = new DbContextOptionsBuilder<VeiculeSaleDbContext>()
                .UseInMemoryDatabase(databaseName: "db-test-veiculos")
                .Options;

            using var context = new VeiculeSaleDbContext(options);

            context.Veiculos.Add(new Domain.Entities.Veiculo("A", "A", 2020, "C", 2000m));
            context.Veiculos.Add(new Domain.Entities.Veiculo("B", "B", 2020, "C", 1000m));
            await context.SaveChangesAsync();

            var repo = new VeiculoRepository(context);

            var result = await repo.ListarPorStatusAsync(Domain.Enums.VeiculoStatus.Disponivel);

            Assert.Collection(result,
                v => Assert.Equal(1000m, v.Preco),
                v => Assert.Equal(2000m, v.Preco));
        }
    }
}
