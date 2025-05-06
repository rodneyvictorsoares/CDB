using CDB.Server.Interfaces;
using CDB.Server.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CDB.Server.Services
{
    public class CdbCalculatorService : ICdbCalculatorService
    {
        private static readonly decimal percentualBancoCDI = 1.08M;
        private static readonly decimal CDIMensal = 0.009M;

        public CdbCalculoResponse Calcular(CdbCalculoRequest request)
        {
            decimal valorBruto = request.ValorInicial;

            for (int mes = 1; mes <= request.PrazoEmMeses; mes++)
            {
                valorBruto *= 1 + (CDIMensal * percentualBancoCDI);
            }

            decimal porcentagemImposto = ObterPorcentagemImposto(request.PrazoEmMeses);

            decimal valorLiquido = decimal.Round(valorBruto * (1 - porcentagemImposto), 2, MidpointRounding.ToZero);

            return new CdbCalculoResponse
            {
                ValorFinalBruto = Math.Round(valorBruto, 2, MidpointRounding.ToZero),
                ValorFinalLiquido = valorLiquido
            };
        }

        private static decimal ObterPorcentagemImposto(int meses)
        {
            if (meses <= 6)
            {
                return 0.225M;
            }
            else if (meses <= 12)
            {
                return 0.20M;
            }
            else if (meses <= 24)
            {
                return 0.175M;
            }
            else
            {
                return 0.15M;
            }
        }   
    }
}
