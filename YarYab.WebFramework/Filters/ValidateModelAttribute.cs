using YarYab.Common;
using YarYab.Common.Utilities;
using YarYab.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.WebFramework.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// When the action executes, validates the model state.
        /// </summary>
        /// <param name="HttpActionContext">The context of the action.</param>

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var controller = context.Controller as Controller;

                var model = context.ActionArguments.FirstOrDefault().Value;

                if (model != null)
                {
                    var errors = new ValidationProblemDetails(modelState);

                    var message = ApiResultStatusCode.BadRequest.ToDisplay();

                    var apiResult = new ApiResult<IDictionary<string, string[]>>(false, ApiResultStatusCode.BadRequest, errors.Errors, message);
                    context.Result = new JsonResult(apiResult) { StatusCode = (int)ApiResultStatusCode.BadRequest };
                    context.HttpContext.Response.StatusCode = (int)ApiResultStatusCode.BadRequest;
                }

                else
                {
                    var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest);
                    context.Result = new JsonResult(apiResult) { StatusCode = (int)ApiResultStatusCode.BadRequest };
                    context.HttpContext.Response.StatusCode = (int)ApiResultStatusCode.BadRequest;
                }
                base.OnActionExecuting(context);
            }


        }
    }
}
