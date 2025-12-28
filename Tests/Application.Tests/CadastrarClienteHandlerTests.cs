using Application.UseCases.Clientes.Commands.CadastrarCliente;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class CadastrarClienteHandlerTests
    {
        [Fact]
        public async Task Handle_CadastraClienteQuandoNaoExiste()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync((Cliente)null);
            repo.Setup(r => r.AdicionarAsync(It.IsAny<Cliente>())).Returns(Task.CompletedTask);

            var handler = new CadastrarClienteHandler(repo.Object);

            var command = new CadastrarClienteCommand
            {
                Nome = "Joao",
                Cpf = "52998224725",
                Email = "joao@example.com",
                Telefone = "999999999"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(command.Nome, result.Nome);
            Assert.Equal(command.Email, result.Email);
        }

        [Fact]
        public async Task Handle_QuandoClienteExiste_Lanca()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Cliente("x", "52998224725", "x@x.com", "000"));

            var handler = new CadastrarClienteHandler(repo.Object);

            var command = new CadastrarClienteCommand
            {
                Nome = "Joao",
                Cpf = "52998224725",
                Email = "joao@example.com",
                Telefone = "999999999"
            };

            await Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
