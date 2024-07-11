using YarYab.Common;
using YarYab.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
namespace YarYab.WebFramework.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okobjectResult)
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okobjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = okobjectResult.StatusCode };
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.Success);
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
            }
            //return BadRequest() method create an objectResult with StatusCode 400 in recent versions, So the following code has changed a bit.
            else if (context.Result is ObjectResult badRequestobjectResult && badRequestobjectResult.StatusCode == 400)
            {
                string message = null;
                switch (badRequestobjectResult.Value)
                {
                    case ValidationProblemDetails validationProblemDetails:
                        var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct();
                        message = string.Join(" | ", errorMessages);
                        break;
                    case SerializableError errors:
                        var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                        message = string.Join(" | ", errorMessages2);
                        break;
                    case var value when value != null && !(value is ProblemDetails):
                        message = badRequestobjectResult.Value.ToString();
                        break;
                }

                var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest, message);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestobjectResult.StatusCode };
            }
            else if (context.Result is ObjectResult notFoundobjectResult && notFoundobjectResult.StatusCode == 404)
            {
                string message = null;
                if (notFoundobjectResult.Value != null && !(notFoundobjectResult.Value is ProblemDetails))
                    message = notFoundobjectResult.Value.ToString();

                //var apiResult = new ApiResult<object>(false, ApiResultStatusCode.NotFound, notFoundobjectResult.Value);
                var apiResult = new ApiResult(false, ApiResultStatusCode.NotFound, message);
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundobjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.Success, contentResult.Content);
                context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, objectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}

