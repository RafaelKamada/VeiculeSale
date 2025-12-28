using Application.UseCases.Veiculos.Queries.ListarVeiculos;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class ListarVeiculosHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarListaDeVeiculosDto_QuandoExistiremVeiculos()
        {
            // 1. Arrange
            var veiculo1 = new Veiculo("Fiat", "Mobi", 2022, "Branco", 45000m);
            var veiculo2 = new Veiculo("Honda", "Civic", 2020, "Preto", 90000m);
             
            var listaDoBanco = new List<Veiculo> { veiculo1, veiculo2 };

            var veiculoRepoMock = new Mock<IVeiculoRepository>();
             
            veiculoRepoMock.Setup(r => r.ListarPorStatusAsync(VeiculoStatus.Disponivel))
                           .ReturnsAsync(listaDoBanco);

            var handler = new ListarVeiculosHandler(veiculoRepoMock.Object);

            var query = new ListarVeiculosQuery(VeiculoStatus.Disponivel);

            // 2. Act
            var result = await handler.Handle(query, CancellationToken.None);

            // 3. Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());  
             
            var primeiroVeiculo = result.First();
            Assert.Equal("Mobi", primeiroVeiculo.Modelo);
            Assert.Equal(45000m, primeiroVeiculo.Preco);
             
            veiculoRepoMock.Verify(r => r.ListarPorStatusAsync(VeiculoStatus.Disponivel), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHouverVeiculos()
        {
            // 1. Arrange
            var veiculoRepoMock = new Mock<IVeiculoRepository>();
             
            veiculoRepoMock.Setup(r => r.ListarPorStatusAsync(It.IsAny<VeiculoStatus>()))
                           .ReturnsAsync(new List<Veiculo>());

            var handler = new ListarVeiculosHandler(veiculoRepoMock.Object);
            var query = new ListarVeiculosQuery(VeiculoStatus.Disponivel);

            // 2. Act
            var result = await handler.Handle(query, CancellationToken.None);

            // 3. Assert
            Assert.NotNull(result);
            Assert.Empty(result);  
        }
    }
}
