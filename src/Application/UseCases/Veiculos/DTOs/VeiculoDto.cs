using Domain.Entities; 

namespace Application.UseCases.Veiculos.DTOs
{
    public class VeiculoDto
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public string Status { get; set; }  
         
        public static VeiculoDto FromEntity(Veiculo veiculo)
        {
            return new VeiculoDto
            {
                Id = veiculo.Id,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor,
                Preco = veiculo.Preco,
                Status = veiculo.Status.ToString()
            };
        }
    }
}
