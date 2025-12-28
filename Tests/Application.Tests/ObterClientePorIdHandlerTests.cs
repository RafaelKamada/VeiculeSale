using Application.UseCases.Clientes.Queries.ObterClientePorId;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class ObterClientePorIdHandlerTests
    {
        [Fact]
        public async Task Handle_RetornaClienteDtoQuandoExiste()
        {
            var cliente = new Cliente("Joao", "52998224725", "joao@example.com", "999");

            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorIdAsync(It.IsAny<System.Guid>())).ReturnsAsync(cliente);

            var handler = new ObterClientePorIdHandler(repo.Object);

            var query = new ObterClientePorIdQuery(cliente.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(cliente.Nome, result.Nome);
        }

        [Fact]
        public async Task Handle_RetornaNullQuandoNaoExiste()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.ObterPorIdAsync(It.IsAny<System.Guid>())).ReturnsAsync((Cliente)null);

            var handler = new ObterClientePorIdHandler(repo.Object);

            var query = new ObterClientePorIdQuery(System.Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
