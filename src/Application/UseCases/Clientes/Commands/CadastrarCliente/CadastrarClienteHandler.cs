using Application.UseCases.Clientes.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR; 

namespace Application.UseCases.Clientes.Commands.CadastrarCliente
{
    public class CadastrarClienteHandler : IRequestHandler<CadastrarClienteCommand, ClienteDto>
    {
        private readonly IClienteRepository _repository;

        public CadastrarClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteDto> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        { 
            var clienteExistente = await _repository.ObterPorCpfAsync(request.Cpf);

            if (clienteExistente != null)
                throw new DomainException("Já existe um cliente cadastrado com este CPF.");
             
            var cliente = new Cliente(request.Nome, request.Cpf, request.Email, request.Telefone);
             
            await _repository.AdicionarAsync(cliente);
             
            return ClienteDto.FromEntity(cliente);
        }
    }
}
