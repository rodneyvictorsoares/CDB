using CDB.Server.Models;
using CDB.Server.Services;
using FluentAssertions;

namespace CDB.Tests.Services
{
    public class CdbCalculatorServiceTests
    {
        private readonly CdbCalculatorService _calculatorService = new();

        [Theory]
        [InlineData(100, 2, 101.95, 101.51)]
        [InlineData(1000, 15, 1156.15, 1128.82)]
        public void Calcular_DeveRetornarValoresBrutoseLiquidosCorretos(decimal valorInicial, int prazoEmMeses, decimal brutoEsperado, decimal liquidoEsperado)
        {
            //Arrage
            var req = new CdbCalculoRequest
            {
                ValorInicial = valorInicial,
                PrazoEmMeses = prazoEmMeses
            };

            //Act
            var resposta = _calculatorService.Calcular(req);

            //Assert
            resposta.ValorFinalBruto
                .Should().BeApproximately(decimal.Round(brutoEsperado, 2), 0.01m);

            resposta.ValorFinalLiquido
                .Should().BeApproximately(decimal.Round(liquidoEsperado, 2), 0.01m);
        }
    }
}
