using CDB.Server.Models;
using CDB.Server.Validators;
using FluentAssertions;
using FluentValidation.Results;

namespace CDB.Tests.Validators
{
    public class CDBCalculoRequestValidatorTests
    {
        private readonly CDBCalculoRequestValidator _validator = new();

        [Theory]
        [InlineData(0, 2, "ValorInicial")]
        [InlineData(-1, 5, "ValorInicial")]
        [InlineData(100, 1, "PrazoEmMeses")]
        public void Validate_DeveFalharParaEntradasInvalidas(decimal valorInicial, int prazoEmMeses, string propriedadeEsperada)
        {
            // Arrange
            var req = new CDBCalculoRequest
            {
                ValorInicial = valorInicial,
                PrazoEmMeses = prazoEmMeses
            };

            //Act
            ValidationResult result = _validator.Validate(req);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == propriedadeEsperada);
        }

        [Fact]
        public void validate_DevePassarParaEntradaValida()
        {
            //Arrage
            var req = new CDBCalculoRequest
            {
                ValorInicial = 100,
                PrazoEmMeses = 3
            };

            //Act
            var result = _validator.Validate(req);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
