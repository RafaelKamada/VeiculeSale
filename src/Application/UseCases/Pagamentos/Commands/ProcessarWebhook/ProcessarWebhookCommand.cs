using Domain.Enums;
using MediatR; 

namespace Application.UseCases.Pagamentos.Commands.ProcessarWebhook
{
    public class ProcessarWebhookCommand : IRequest<bool>
    {
        public string CodigoTransacao { get; set; }
        public PagamentoStatus NovoStatus { get; set; }
    }
}
