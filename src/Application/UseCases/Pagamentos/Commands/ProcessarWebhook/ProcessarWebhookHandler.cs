using Domain.Enums;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Pagamentos.Commands.ProcessarWebhook
{
    public class ProcessarWebhookHandler : IRequestHandler<ProcessarWebhookCommand, bool>
    {
        private readonly IPagamentoRepository _pagamentoRepo;
        private readonly IVendaRepository _vendaRepo;
        private readonly IVeiculoRepository _veiculoRepo;

        public ProcessarWebhookHandler(
            IPagamentoRepository pagamentoRepo,
            IVendaRepository vendaRepo,
            IVeiculoRepository veiculoRepo)
        {
            _pagamentoRepo = pagamentoRepo;
            _vendaRepo = vendaRepo;
            _veiculoRepo = veiculoRepo;
        }

        public async Task<bool> Handle(ProcessarWebhookCommand request, CancellationToken cancellationToken)
        {
            var pagamento = await _pagamentoRepo.ObterPorCodigoTransacaoAsync(request.CodigoTransacao);

            if (pagamento == null)
                throw new Exception("Transação não encontrada.");

            pagamento.AtualizarStatusPeloWebhook(request.NovoStatus);
            await _pagamentoRepo.AtualizarAsync(pagamento);

            var venda = pagamento.Venda;
            var veiculo = venda.Veiculo;

            if (request.NovoStatus == PagamentoStatus.Aprovado)
            {
                venda.Confirmar();
                veiculo.Vender();
            }
            else if (request.NovoStatus == PagamentoStatus.Cancelado)
            {
                venda.Cancelar();
                veiculo.Disponibilizar();
            }

            await _vendaRepo.AtualizarAsync(venda);
            await _veiculoRepo.AtualizarAsync(veiculo);

            return true;
        }
    }
}
