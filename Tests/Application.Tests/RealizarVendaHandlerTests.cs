using Application.UseCases.Vendas.Commands.RealizarVenda;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class RealizarVendaHandlerTests
    {
        [Fact]
        public async Task RealizarVenda_DeveCriarVenda_QuandoClienteEVeiculoExistem()
        {
            // Arrange
            var veiculo = new Veiculo("Ford", "Ka", 2020, "Preto", 30000m);
            var cliente = new Domain.Entities.Cliente("Joao", "00000000191", "joao@email.com", "11999999999");

            var veiculoRepo = new Mock<IVeiculoRepository>();
            var clienteRepo = new Mock<IClienteRepository>();
            var vendaRepo = new Mock<IVendaRepository>();
            var pagamentoRepo = new Mock<IPagamentoRepository>();
             
            veiculoRepo.Setup(r => r.ObterPorIdAsync(veiculo.Id)).ReturnsAsync(veiculo);
             
            clienteRepo.Setup(r => r.ObterPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
             
            vendaRepo.Setup(r => r.AdicionarAsync(It.IsAny<Venda>())).Returns(Task.CompletedTask);
            pagamentoRepo.Setup(r => r.AdicionarAsync(It.IsAny<Pagamento>())).Returns(Task.CompletedTask);

            var handler = new RealizarVendaHandler(veiculoRepo.Object, clienteRepo.Object, vendaRepo.Object, pagamentoRepo.Object);
             
            var command = new RealizarVendaCommand
            {
                VeiculoId = veiculo.Id,
                ClienteId = cliente.Id
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(veiculo.Preco, result.ValorTotal); 
            vendaRepo.Verify(r => r.AdicionarAsync(It.IsAny<Venda>()), Times.Once); 
            pagamentoRepo.Verify(r => r.AdicionarAsync(It.IsAny<Pagamento>()), Times.Once);
        }

        [Fact]
        public async Task RealizarVenda_DeveLancarErro_QuandoClienteNaoExiste()
        {
            // Arrange
            var veiculo = new Veiculo("Ford", "Ka", 2020, "Preto", 30000m);
            var clienteIdInexistente = Guid.NewGuid(); 
            var veiculoRepo = new Mock<IVeiculoRepository>();
            var clienteRepo = new Mock<IClienteRepository>();
            var vendaRepo = new Mock<IVendaRepository>();
            var pagamentoRepo = new Mock<IPagamentoRepository>();
             
            veiculoRepo.Setup(r => r.ObterPorIdAsync(veiculo.Id)).ReturnsAsync(veiculo); 
            clienteRepo.Setup(r => r.ObterPorIdAsync(clienteIdInexistente)).ReturnsAsync((Domain.Entities.Cliente)null);

            var handler = new RealizarVendaHandler(veiculoRepo.Object, clienteRepo.Object, vendaRepo.Object, pagamentoRepo.Object);

            var command = new RealizarVendaCommand
            {
                VeiculoId = veiculo.Id,
                ClienteId = clienteIdInexistente
            };

            // Act & Assert 
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, CancellationToken.None)); 
            Assert.Contains("Cliente não encontrado", exception.Message); 
            vendaRepo.Verify(r => r.AdicionarAsync(It.IsAny<Venda>()), Times.Never);
        }
    }
}
