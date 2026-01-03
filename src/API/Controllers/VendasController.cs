using Application.UseCases.Vendas.Commands.RealizarVenda;
using Application.UseCases.Vendas.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registra a venda de um veículo para um cliente.
        /// </summary>
        /// <remarks>
        /// O veículo deve estar com status 'Disponivel'.
        /// Uma venda será criada com status 'AguardandoPagamento'.
        /// O status do veículo só será alterado para 'Vendido' após a confirmação do pagamento via Webhook.
        /// </remarks>
        /// <response code="201">Venda registrada com sucesso.</response>
        /// <response code="400">Veículo indisponível, cliente inválido ou erro de negócio.</response>
        /// <response code="404">Veículo não encontrado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(VendaRealizadaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RealizarVenda([FromBody] RealizarVendaCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(RealizarVenda), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
