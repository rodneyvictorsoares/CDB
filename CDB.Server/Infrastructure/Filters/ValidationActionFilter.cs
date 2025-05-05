using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CDB.Server.Infrastructure.Filters
{
    public class ValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments)
            {
                var validatorType = typeof(IValidator<>).MakeGenericType(arg.Value!.GetType());
                if (context.HttpContext.RequestServices.GetService(validatorType) is IValidator validator)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(arg.Value!));
                    if (!validationResult.IsValid)
                    {
                        var messages = validationResult.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray();

                        context.Result = new BadRequestObjectResult(new
                        {
                            errors = messages
                        });

                        return;
                    }
                }
            }
            await next();
        }
    }
}
