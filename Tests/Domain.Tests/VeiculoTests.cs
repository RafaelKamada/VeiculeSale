using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace Domain.Tests
{
    public class VeiculoTests
    {
        [Fact]
        public void CriarVeiculo_DefinePropriedadesEStatusDisponivel()
        {
            var veiculo = new Veiculo("Ford", "Ka", 2020, "Preto", 30000m);

            Assert.Equal("Ford", veiculo.Marca);
            Assert.Equal("Ka", veiculo.Modelo);
            Assert.Equal(2020, veiculo.Ano);
            Assert.Equal("Preto", veiculo.Cor);
            Assert.Equal(30000m, veiculo.Preco);
            Assert.Equal(VeiculoStatus.Disponivel, veiculo.Status);
        }
    }
}
