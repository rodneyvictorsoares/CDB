using CDB.Server.Interfaces;
using CDB.Server.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CDB.Server.Services
{
    public class CDBCalculatorService : ICDBCalculatorService
    {
        private static readonly decimal percentualBancoCDI = 1.08M;
        private static readonly decimal CDIMensal = 0.009M;

        public CDBCalculoResponse Calcular(CDBCalculoRequest request)
        {
            decimal valorBruto = request.ValorInicial;

            for (int mes = 1; mes <= request.PrazoEmMeses; mes++)
            {
                valorBruto *= 1 + (CDIMensal * percentualBancoCDI);
            }

            decimal porcentagemImposto = ObterPorcentagemImposto(request.PrazoEmMeses);

            //decimal valorLiquido = valorBruto * (1 - porcentagemImposto);
            decimal valorLiquido = decimal.Round(valorBruto * (1 - porcentagemImposto), 2, MidpointRounding.ToZero);

            return new CDBCalculoResponse
            {
                ValorFinalBruto = Math.Round(valorBruto, 2, MidpointRounding.ToZero),
                ValorFinalLiquido = valorLiquido
            };
        }

        private static decimal ObterPorcentagemImposto(int meses) =>
            meses <= 6 ? 0.225M :
            meses <= 12 ? 0.20M :
            meses <= 24 ? 0.175M :
                          0.15M;
    }
}
