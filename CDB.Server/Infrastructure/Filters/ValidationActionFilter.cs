using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CDB.Server.Infrastructure.Filters
{
    public class ValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var validationTasks = context.ActionArguments
                .Select(async kvp =>
                {
                    var value = kvp.Value!;
                    var validatorType = typeof(IValidator<>)
                        .MakeGenericType(value.GetType());
                    if (context.HttpContext.RequestServices
                        .GetService(validatorType) is not IValidator validator)
                        return (string[]?)null;

                    var result = await validator
                        .ValidateAsync(new ValidationContext<object>(value));
                    return result.IsValid
                        ? null
                        : result.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray();
                })
                .ToList();

            var results = await Task.WhenAll(validationTasks);

            var firstFailure = results.FirstOrDefault(r => r != null);
            if (firstFailure != null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errors = firstFailure
                });
                return;
            }

            await next();
        }
    }
}
