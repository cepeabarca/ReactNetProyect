using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ReactNetProyect.BackEnd.API.Filters
{
    public class ParseBadRequests : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var castingResult = context.Result as IStatusCodeActionResult;
            if (castingResult == null)
            {
                return;
            }

            var statusCode = castingResult.StatusCode;
            if (statusCode == 400)
            {
                var response = new List<string>();
                var currentResult = context.Result as BadRequestObjectResult;
                if (currentResult.Value is string)
                {
                    response.Add(currentResult.Value.ToString());
                }
                else if (currentResult.Value is IEnumerable<IdentityError> errors)
                {
                    foreach (var error in errors)
                    {
                        response.Add(error.Description);
                    }
                }
                else
                {
                    foreach (var key in context.ModelState.Keys)
                    {
                        foreach (var error in context.ModelState[key].Errors)
                        {
                            response.Add($"{key}: {error.ErrorMessage}");
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }

}
