﻿using InvoicesManager.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InvoicesManager.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                context.HttpContext.Response.StatusCode = (int)code;
                context.Result = new JsonResult(
                    ((ValidationException)context.Exception).Failures);

                return;
            }

            if (context.Exception is NotAcceptableException)
            {
                code = HttpStatusCode.NotAcceptable;
                context.HttpContext.Response.StatusCode = (int)code;
                context.Result = new JsonResult(context.Exception.Message);

                return;
            }


            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });

        }
    }
}
