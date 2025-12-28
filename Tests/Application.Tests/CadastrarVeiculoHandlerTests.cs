using Application.UseCases.Veiculos.Commands.CadastrarVeiculo;
using Domain.Entities;
using Domain.Interfaces;
using Moq; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class CadastrarVeiculoHandlerTests
    {
        [Fact]
        public async Task Handle_DeveCadastrarVeiculo_QuandoDadosValidos()
        {
            // Arrange
            var veiculoRepoMock = new Mock<IVeiculoRepository>(); 
            veiculoRepoMock.Setup(r => r.AdicionarAsync(It.IsAny<Veiculo>()))
                           .Returns(Task.CompletedTask);

            var handler = new CadastrarVeiculoHandler(veiculoRepoMock.Object);

            var command = new CadastrarVeiculoCommand
            {
                Marca = "Toyota",
                Modelo = "Corolla",
                Ano = 2024,
                Cor = "Branco",
                Preco = 150000m
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(System.Guid.Empty, result.Id); 
            veiculoRepoMock.Verify(r => r.AdicionarAsync(It.IsAny<Veiculo>()), Times.Once);
        }
    }
}
