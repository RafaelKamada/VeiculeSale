using Application.UseCases.Veiculos.Commands.CadastrarVeiculo;
using Application.UseCases.Veiculos.Commands.EditarVeiculo;
using Application.UseCases.Veiculos.DTOs;
using Application.UseCases.Veiculos.Queries.ListarVeiculos;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VeiculosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarVeiculoCommand command)
        {
            var id = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Editar(Guid id, [FromBody] EditarVeiculoCommand command)
        {
            if (id != command.Id) return BadRequest("ID da URL difere do ID do corpo.");

            await _mediator.Send(command);
            return NoContent();  
        }

        [HttpGet("a-venda")]
        [ProducesResponseType(typeof(IEnumerable<VeiculoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarAVenda()
        {
            var query = new ListarVeiculosQuery(VeiculoStatus.Disponivel);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("vendidos")]
        [ProducesResponseType(typeof(IEnumerable<VeiculoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarVendidos()
        {
            var query = new ListarVeiculosQuery(VeiculoStatus.Vendido);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
