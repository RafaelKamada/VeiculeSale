using Application.UseCases.Clientes.Commands.AtualizarCliente;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class AtualizarClienteHandlerTests
    {
        [Fact]
        public async Task Handle_AtualizaClienteExistente()
        {
            var cliente = new Cliente("Joao", "52998224725", "joao@example.com", "999");

            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorIdAsync(It.IsAny<System.Guid>())).ReturnsAsync(cliente);
            repo.Setup(r => r.AtualizarAsync(It.IsAny<Cliente>())).Returns(Task.CompletedTask);

            var handler = new AtualizarClienteHandler(repo.Object);

            var command = new AtualizarClienteCommand
            {
                Id = cliente.Id,
                Nome = "Joao Updated",
                Email = "novo@example.com",
                Telefone = "111"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(command.Nome, result.Nome);
            Assert.Equal(command.Email, result.Email);
        }

        [Fact]
        public async Task Handle_ClienteNaoEncontrado_Lanca()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorIdAsync(It.IsAny<System.Guid>())).ReturnsAsync((Cliente)null);

            var handler = new AtualizarClienteHandler(repo.Object);

            var command = new AtualizarClienteCommand { Id = System.Guid.NewGuid(), Nome = "x", Email = "x@x.com", Telefone = "x" };

            await Assert.ThrowsAsync<System.Collections.Generic.KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
