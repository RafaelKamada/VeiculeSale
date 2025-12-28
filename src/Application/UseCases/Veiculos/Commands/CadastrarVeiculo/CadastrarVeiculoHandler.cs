using Application.UseCases.Veiculos.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UseCases.Veiculos.Commands.CadastrarVeiculo
{
    public class CadastrarVeiculoHandler : IRequestHandler<CadastrarVeiculoCommand, VeiculoDto>
    {
        private readonly IVeiculoRepository _repository;
         
        public CadastrarVeiculoHandler(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task<VeiculoDto> Handle(CadastrarVeiculoCommand request, CancellationToken cancellationToken)
        { 
            var veiculo = new Veiculo(
                request.Marca,
                request.Modelo,
                request.Ano,
                request.Cor,
                request.Preco
            );
             
            await _repository.AdicionarAsync(veiculo);
             
            return VeiculoDto.FromEntity(veiculo);
        }
    }
}
