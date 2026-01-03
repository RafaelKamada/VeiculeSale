using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using Xunit;

namespace Domain.Tests
{
    public class CpfTests
    {
        [Fact]
        public void Cpf_Valido_CriaObjeto()
        {
            var raw = "529.982.247-25"; 

            var cpf = new Cpf(raw);

            Assert.Equal("52998224725", cpf.Numero);
            Assert.Equal("529.982.247-25", cpf.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("111.111.111-11")]
        public void Cpf_Invalido_Lanca(string valor)
        {
            Assert.Throws<DomainException>(() => new Cpf(valor));
        }
    }
}
