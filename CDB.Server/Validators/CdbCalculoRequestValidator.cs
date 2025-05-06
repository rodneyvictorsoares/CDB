using CDB.Server.Models;
using FluentValidation;

namespace CDB.Server.Validators
{
    public class CdbCalculoRequestValidator : AbstractValidator<CdbCalculoRequest>
    {
        public CdbCalculoRequestValidator()
        {
            RuleFor(x => x.ValorInicial)
                .GreaterThan(0)
                .WithMessage("Valor Inicial da aplicação deve ser positivo.");

            RuleFor(x => x.PrazoEmMeses)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Prazo mínimo deve ser superior a 1 mês.");

        }
    }
}
