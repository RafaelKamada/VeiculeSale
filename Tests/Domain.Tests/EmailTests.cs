using Domain.ValueObjects;
using System;
using Xunit;

namespace Domain.Tests
{
    public class EmailTests
    {
        [Fact]
        public void Email_Valido_CriaObjeto()
        {
            var email = new Email("user@example.com");
            Assert.Equal("user@example.com", email.Address);
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid-email")]
        [InlineData("a@b")]
        public void Email_Invalido_Lanca(string valor)
        {
            Assert.Throws<Exception>(() => new Email(valor));
        }
    }
}
