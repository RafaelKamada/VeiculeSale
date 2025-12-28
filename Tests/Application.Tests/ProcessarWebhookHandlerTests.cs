using Application.UseCases.Pagamentos.Commands.ProcessarWebhook;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Moq;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class ProcessarWebhookHandlerTests
    {
        [Fact]
        public async Task ProcessarWebhook_AprovaPagamento_ConfirmaVendaEVendeVeiculo()
        {
            // 1. Arrange
            var veiculo = new Veiculo("Ford", "Ka", 2020, "Preto", 30000m);
            var venda = new Venda(veiculo.Id, Guid.NewGuid(), veiculo.Preco);

            typeof(Venda).GetProperty(nameof(Venda.Veiculo))
                ?.SetValue(venda, veiculo);

            var pagamento = new Pagamento(venda.Id, venda.ValorTotal, "tx-1");

            typeof(Pagamento).GetProperty(nameof(Pagamento.Venda))
                ?.SetValue(pagamento, venda);

            var pagamentoRepo = new Mock<IPagamentoRepository>();
            var vendaRepo = new Mock<IVendaRepository>();
            var veiculoRepo = new Mock<IVeiculoRepository>();

            pagamentoRepo.Setup(r => r.ObterPorCodigoTransacaoAsync(It.IsAny<string>()))
                .ReturnsAsync(pagamento);

            pagamentoRepo.Setup(r => r.AtualizarAsync(It.IsAny<Pagamento>())).Returns(Task.CompletedTask);
            vendaRepo.Setup(r => r.AtualizarAsync(It.IsAny<Venda>())).Returns(Task.CompletedTask);
            veiculoRepo.Setup(r => r.AtualizarAsync(It.IsAny<Veiculo>())).Returns(Task.CompletedTask);

            var handler = new ProcessarWebhookHandler(pagamentoRepo.Object, vendaRepo.Object, veiculoRepo.Object);

            var command = new ProcessarWebhookCommand
            {
                CodigoTransacao = "tx-1",
                NovoStatus = PagamentoStatus.Aprovado
            };

            // 2. Act
            var result = await handler.Handle(command, CancellationToken.None);

            // 3. Assert
            Assert.True(result);
            Assert.Equal(VendaStatus.Concluida, pagamento.Venda.Status);
            Assert.Equal(VeiculoStatus.Vendido, pagamento.Venda.Veiculo.Status);
        }
    }
}
