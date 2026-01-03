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

        /// <summary>
        /// Cadastra um novo veículo no estoque.
        /// </summary>
        /// <response code="201">Veículo cadastrado com sucesso.</response>
        /// <response code="400">Dados inválidos no corpo da requisição.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarVeiculoCommand command)
        {
            var id = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        /// <summary>
        /// Atualiza os dados de um veículo existente.
        /// </summary>
        /// <remarks>
        /// Não é possível editar um veículo que já foi vendido (Status = Vendido).
        /// </remarks>
        /// <response code="204">Atualização realizada com sucesso.</response>
        /// <response code="400">ID da URL difere do corpo ou dados inválidos.</response>
        /// <response code="404">Veículo não encontrado.</response>
        /// <response code="409">Conflito: Tentativa de editar um veículo já vendido.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Editar(Guid id, [FromBody] EditarVeiculoCommand command)
        {
            try
            {
                if (id != command.Id) return BadRequest("ID da URL diferente do ID do corpo.");

                await _mediator.Send(command);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return Conflict(new { message = "Nao e possivel editar veiculo vendido." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return ValidationProblem("Dados invalidos");
            }
        }

        /// <summary>
        /// Lista os veículos disponíveis para venda.
        /// </summary>
        /// <response code="200">Retorna a lista de veículos.</response>
        [HttpGet("disponiveis")]
        [ProducesResponseType(typeof(IEnumerable<VeiculoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarAVenda()
        {
            var query = new ListarVeiculosQuery(VeiculoStatus.Disponivel);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lista o histórico de veículos vendidos.
        /// </summary>
        /// <response code="200">Retorna a lista de veículos vendidos.</response>
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
