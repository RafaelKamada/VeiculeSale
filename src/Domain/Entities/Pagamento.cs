using Domain.Enums;

namespace Domain.Entities
{
    public class Pagamento
    {
        public Pagamento(Guid vendaId, decimal valor, string codigoTransacao = null)
        {
            Id = Guid.NewGuid();
            VendaId = vendaId;
            Valor = valor;
            CodigoTransacao = codigoTransacao;
            Status = PagamentoStatus.Processando;
        }

        public Guid Id { get; private set; }
        public Guid VendaId { get; private set; }
        public decimal Valor { get; private set; }
        public string CodigoTransacao { get; private set; } // O ID externo
        public PagamentoStatus Status { get; private set; } // Nome simplificado

        // Navegação
        public virtual Venda Venda { get; private set; }

        // Método crucial para o Webhook
        public void AtualizarStatusPeloWebhook(PagamentoStatus novoStatus)
        {
            // Regra de negócio: não alterar pagamento que já foi finalizado, por exemplo.
            if (Status == PagamentoStatus.Aprovado) return;

            Status = novoStatus;
        }

        public void DefinirCodigoTransacao(string codigo)
        {
            CodigoTransacao = codigo;
        }
    }
}
