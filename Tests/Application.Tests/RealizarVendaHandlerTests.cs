using Application.UseCases.Vendas.Commands.RealizarVenda;
using Domain.Entities;
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
        public async Task RealizarVenda_CriaVendaEPagamentoQuandoClienteNaoExiste()
        {
            var veiculo = new Veiculo("Ford", "Ka", 2020, "Preto", 30000m);

            var veiculoRepo = new Mock<IVeiculoRepository>();
            var clienteRepo = new Mock<IClienteRepository>();
            var vendaRepo = new Mock<IVendaRepository>();
            var pagamentoRepo = new Mock<IPagamentoRepository>();

            veiculoRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(veiculo);
            clienteRepo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync((Domain.Entities.Cliente)null);

            vendaRepo.Setup(r => r.AdicionarAsync(It.IsAny<Venda>())).Returns(Task.CompletedTask);
            clienteRepo.Setup(r => r.AdicionarAsync(It.IsAny<Domain.Entities.Cliente>())).Returns(Task.CompletedTask);
            pagamentoRepo.Setup(r => r.AdicionarAsync(It.IsAny<Pagamento>())).Returns(Task.CompletedTask);

            var handler = new RealizarVendaHandler(veiculoRepo.Object, clienteRepo.Object, vendaRepo.Object, pagamentoRepo.Object);

            var command = new RealizarVendaCommand
            {
                VeiculoId = veiculo.Id,
                ClienteCpf = "12734951010",
                ClienteNome = "Joao",
                ClienteEmail = "joao@email.com",
                ClienteTelefone = "999999999",
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(veiculo.Preco, result.ValorTotal);
        }
    }
}
