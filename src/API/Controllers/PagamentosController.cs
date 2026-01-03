using Application.UseCases.Pagamentos.Commands.ProcessarWebhook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagamentosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Recebe notificações de pagamento (Webhook).
        /// </summary>
        /// <remarks>
        /// Endpoint utilizado por gateways de pagamento externos para notificar alterações de status.
        /// </remarks>
        /// <response code="200">Webhook processado com sucesso.</response>
        /// <response code="400">Erro ao processar o payload do webhook.</response>
        /// <response code="404">Transação não encontrada.</response>
        [HttpPost("webhook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReceberNotificacao([FromBody] ProcessarWebhookCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { mensagem = "Webhook processado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
