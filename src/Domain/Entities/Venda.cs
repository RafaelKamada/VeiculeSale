using Domain.Enums;

namespace Domain.Entities
{
    public class Venda
    {
        public Venda(Guid veiculoId, Guid clienteId, decimal valorTotal)
        {
            Id = Guid.NewGuid();
            VeiculoId = veiculoId;
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            DataVenda = DateTime.UtcNow;
            Status = VendaStatus.AguardandoPagamento;
        }

        public Guid Id { get; private set; }
        public Guid VeiculoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public DateTime DataVenda { get; private set; }
        public decimal ValorTotal { get; private set; }
        public VendaStatus Status { get; private set; } 

        public virtual Veiculo Veiculo { get; private set; }
        public virtual Cliente Cliente { get; private set; }

        public virtual ICollection<Pagamento> Pagamentos { get; private set; } = new List<Pagamento>();

        public void Confirmar()
        {
            Status = VendaStatus.Concluida;
        }

        public void Cancelar()
        {
            Status = VendaStatus.Cancelada;
        }
    }
}
