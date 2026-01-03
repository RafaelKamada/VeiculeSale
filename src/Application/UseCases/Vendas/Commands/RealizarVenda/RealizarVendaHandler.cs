using Application.UseCases.Vendas.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Vendas.Commands.RealizarVenda
{
    public class RealizarVendaHandler : IRequestHandler<RealizarVendaCommand, VendaRealizadaDto>
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

        public async Task<VendaRealizadaDto> Handle(RealizarVendaCommand request, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepo.ObterPorIdAsync(request.VeiculoId);

            if (veiculo is null)
                throw new KeyNotFoundException("Veículo não encontrado.");

            if (veiculo.Status != VeiculoStatus.Disponivel)
                throw new DomainException("Veículo não está disponível para venda.");

            var cliente = await _clienteRepo.ObterPorIdAsync(request.ClienteId);

            if (cliente is null)
            {
                throw new DomainException("Cliente não encontrado. Cadastre o cliente antes de realizar a venda.");
            }

            var venda = new Venda(veiculo.Id, cliente.Id, veiculo.Preco);
            await _vendaRepo.AdicionarAsync(venda);

            var codigoTransacaoInicial = Guid.NewGuid().ToString();

            var pagamento = new Pagamento(venda.Id, venda.ValorTotal, codigoTransacaoInicial);
            await _pagamentoRepo.AdicionarAsync(pagamento);

            return VendaRealizadaDto.FromEntity(venda, pagamento);
        }
    }
}
