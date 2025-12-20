using MediatR; 

namespace Application.UseCases.Veiculos.Commands.CadastrarVeiculo
{
    public class CadastrarVeiculoCommand : IRequest<Guid>
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
