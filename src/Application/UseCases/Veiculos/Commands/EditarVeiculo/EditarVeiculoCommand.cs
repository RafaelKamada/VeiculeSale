using MediatR; 

namespace Application.UseCases.Veiculos.Commands.EditarVeiculo
{
    public class EditarVeiculoCommand : IRequest 
    {
        public Guid Id { get; set; }  
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
    }
}
