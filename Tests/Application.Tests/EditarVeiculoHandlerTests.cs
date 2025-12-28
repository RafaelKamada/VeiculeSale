using Application.UseCases.Veiculos.Commands.EditarVeiculo;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class EditarVeiculoHandlerTests
    {
        [Fact]
        public async Task Handle_DeveEditarVeiculo_QuandoVeiculoExiste()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var veiculoExistente = new Veiculo("Fiat", "Uno", 2010, "Prata", 15000m); 
            typeof(Veiculo).GetProperty("Id")?.SetValue(veiculoExistente, veiculoId);

            var veiculoRepoMock = new Mock<IVeiculoRepository>();
             
            veiculoRepoMock.Setup(r => r.ObterPorIdAsync(veiculoId))
                           .ReturnsAsync(veiculoExistente);

            veiculoRepoMock.Setup(r => r.AtualizarAsync(It.IsAny<Veiculo>()))
                           .Returns(Task.CompletedTask);

            var handler = new EditarVeiculoHandler(veiculoRepoMock.Object);

            var command = new EditarVeiculoCommand
            {
                Id = veiculoId,
                Marca = "Fiat",
                Modelo = "Uno Mille", 
                Ano = 2011,          
                Cor = "Preto",        
                Preco = 18000m       
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            veiculoRepoMock.Verify(r => r.AtualizarAsync(It.Is<Veiculo>(v =>
                v.Modelo == "Uno Mille" &&
                v.Preco == 18000m
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecao_QuandoVeiculoNaoExiste()
        {
            // Arrange
            var veiculoRepoMock = new Mock<IVeiculoRepository>(); 
            veiculoRepoMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((Veiculo?)null);

            var handler = new EditarVeiculoHandler(veiculoRepoMock.Object);
            var command = new EditarVeiculoCommand { Id = Guid.NewGuid() };

            // Act & Assert 
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
