using Domain.Enums;

namespace Domain.Entities
{
    public class Veiculo
    {
        // Construtor para garantir estado válido na criação
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
        public int Ano { get; private set; } // Unificado
        public string Cor { get; private set; }
        public decimal Preco { get; private set; }
        public VeiculoStatus Status { get; private set; } // Nome simplificado
        public DateTime DataCadastro { get; private set; }

        // Comportamentos (Métodos de Domínio)

        public void AtualizarDados(string marca, string modelo, int ano, string cor, decimal preco)
        {
            // Aqui você poderia colocar validações. Ex: Preço não pode ser negativo
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            Preco = preco;
        }

        public void Vender()
        {
            if (Status != VeiculoStatus.Disponivel)
                throw new Exception("Veículo não está disponível.");

            Status = VeiculoStatus.Vendido;
        }

        public void Disponibilizar() // Caso a venda seja cancelada
        {
            Status = VeiculoStatus.Disponivel;
        }
    }
}
