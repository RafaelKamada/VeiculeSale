using Application.UseCases.Vendas.Commands.RealizarVenda;
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

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RealizarVenda([FromBody] RealizarVendaCommand command)
        {
            try
            {
                var vendaId = await _mediator.Send(command);

                return CreatedAtAction(nameof(RealizarVenda), new { id = vendaId }, vendaId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
