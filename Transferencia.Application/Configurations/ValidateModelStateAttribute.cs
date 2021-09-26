using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Transferencia.Application.Configurations
{
    public class ValidateModelStateAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception as ValidationException;
            if (ex == null) return;

            var errors = ex.Errors.Select(x => new
            {
                x.PropertyName,
                x.ErrorMessage,
                x.AttemptedValue
            });

            var responseObj = new
            {
                Message = "Bad Request",
                Errors = errors
            };

            context.Result = new JsonResult(responseObj)
            {
                StatusCode = 400
            };
        }
    }
}
