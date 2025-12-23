using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Vendas.Commands.RealizarVenda
{
    public class RealizarVendaHandler : IRequestHandler<RealizarVendaCommand, Guid>
    {
        private readonly IVeiculoRepository _veiculoRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IVendaRepository _vendaRepo;
        private readonly IPagamentoRepository _pagamentoRepo;

        public RealizarVendaHandler(
            IVeiculoRepository veiculoRepo,
            IClienteRepository clienteRepo,
            IVendaRepository vendaRepo,
            IPagamentoRepository pagamentoRepo)
        {
            _veiculoRepo = veiculoRepo;
            _clienteRepo = clienteRepo;
            _vendaRepo = vendaRepo;
            _pagamentoRepo = pagamentoRepo;
        }

        public async Task<Guid> Handle(RealizarVendaCommand request, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepo.ObterPorIdAsync(request.VeiculoId);

            if (veiculo is null)
                throw new Exception("Veículo não encontrado.");

            if (veiculo.Status != VeiculoStatus.Disponivel)
                throw new Exception("Veículo não está disponível para venda.");

            var cliente = await _clienteRepo.ObterPorCpfAsync(request.ClienteCpf);

            if (cliente is null)
            {
                cliente = new Cliente(request.ClienteNome, request.ClienteCpf, request.ClienteEmail);
                await _clienteRepo.AdicionarAsync(cliente);
            }

            var venda = new Venda(veiculo.Id, cliente.Id, veiculo.Preco);
            await _vendaRepo.AdicionarAsync(venda);

            var codigoTransacaoInicial = Guid.NewGuid().ToString();

            var pagamento = new Pagamento(venda.Id, venda.ValorTotal, codigoTransacaoInicial);
            await _pagamentoRepo.AdicionarAsync(pagamento);

            return venda.Id;
        }
    }
}
