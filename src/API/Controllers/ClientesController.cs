using Application.UseCases.Clientes.Commands.AtualizarCliente;
using Application.UseCases.Clientes.Commands.CadastrarCliente;
using Application.UseCases.Clientes.DTOs;
using Application.UseCases.Clientes.Queries.ObterClientePorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <response code="201">Cliente cadastrado com sucesso.</response>
        /// <response code="400">Erro na validação dos dados (ex: CPF inválido).</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarClienteCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        /// <summary>
        /// Obtém os dados de um cliente pelo ID.
        /// </summary>
        /// <param name="id">GUID do cliente</param>
        /// <response code="200">Retorna os dados do cliente.</response>
        /// <response code="404">Cliente não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var result = await _mediator.Send(new ObterClientePorIdQuery(id));

            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza os dados cadastrais de um cliente.
        /// </summary>
        /// <response code="200">Cliente atualizado com sucesso.</response>
        /// <response code="400">ID inválido ou erro de validação.</response>
        /// <response code="404">Cliente não encontrado na base de dados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarClienteCommand command)
        {
            if (id != command.Id) return BadRequest("ID da URL difere do ID do corpo.");

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(new { erro = k.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
