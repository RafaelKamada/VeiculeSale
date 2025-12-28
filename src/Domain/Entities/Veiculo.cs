using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Veiculo
    {
        public Veiculo(string marca, string modelo, int ano, string cor, decimal preco)
        {
            Id = Guid.NewGuid();
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            Preco = preco;
            Status = VeiculoStatus.Disponivel;
            DataCadastro = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public int Ano { get; private set; } 
        public string Cor { get; private set; }
        public decimal Preco { get; private set; }
        public VeiculoStatus Status { get; private set; } 
        public DateTime DataCadastro { get; private set; }


        public void AtualizarDados(string marca, string modelo, int ano, string cor, decimal preco)
        {
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            Preco = preco;
        }

        public void Vender()
        {
            if (Status != VeiculoStatus.Disponivel)
                throw new DomainException("Veículo não está disponível.");

            Status = VeiculoStatus.Vendido;
        }

        public void Disponibilizar()  
        {
            Status = VeiculoStatus.Disponivel;
        }
    }
}
