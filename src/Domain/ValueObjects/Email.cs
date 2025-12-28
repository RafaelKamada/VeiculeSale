using Domain.Exceptions;
using System.Text.RegularExpressions; 

namespace Domain.ValueObjects
{
    public record Email 
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new DomainException("E-mail não pode ser vazio.");

            if (!Regex.IsMatch(address, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                throw new DomainException("E-mail inválido.");

            Address = address;
        }
         
        public static implicit operator string(Email email) => email.Address;
        public static implicit operator Email(string address) => new Email(address);
    }
}
