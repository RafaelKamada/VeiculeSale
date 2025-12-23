using Domain.Interfaces;
using MediatR; 
namespace Application.UseCases.Veiculos.Commands.EditarVeiculo
{
    public class EditarVeiculoHandler : IRequestHandler<EditarVeiculoCommand>
    {
        private readonly IVeiculoRepository _repository;

        public EditarVeiculoHandler(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EditarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = await _repository.ObterPorIdAsync(request.Id);

            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");
             
            veiculo.AtualizarDados(
                request.Marca,
                request.Modelo,
                request.Ano,
                request.Cor,
                request.Preco
            );

            await _repository.AtualizarAsync(veiculo);
        }
    }
}
