using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PIMTool.Services.Common.Exception;
using PIMTool.Shared.Contract.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIMTool.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;


        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;

            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(CoreException), HandleCoreException },
                { typeof(DataValidationException), HandleDataValidationException},
            };
        }
        public void OnException(ExceptionContext context)
        {
            HandleException(context);
        }

        public void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string actionName = controllerActionDescriptor?.ActionName;
            var errors = context.ModelState
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                );
            var details = new ErrorObjectRes
            {
                Status = StatusCodes.Status400BadRequest,
                Title = actionName,
                Detail = context.Exception.Message,
                Errors = errors,
                RequestId = context.HttpContext.TraceIdentifier
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleDataValidationException(ExceptionContext context)
        {
            var dataValidationException = context.Exception as DataValidationException;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string actionName = controllerActionDescriptor?.ActionName;
            var details = new ErrorObjectRes
            {
                Status = StatusCodes.Status400BadRequest,
                Title = actionName,
                Detail = dataValidationException.Message,
                RequestId = context.HttpContext.TraceIdentifier,
                Errors = dataValidationException.Errors,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        public void HandleCoreException(ExceptionContext context)
        {
            var coreException = context.Exception as CoreException;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string actionName = controllerActionDescriptor?.ActionName;
            var details = new ErrorObjectRes
            {
                Status = coreException.StatusCode,
                Title = actionName,
                Detail = coreException.Message,
                RequestId = context.HttpContext.TraceIdentifier
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = coreException.StatusCode
            };

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ErrorObjectRes
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = context.Exception.Message,
                RequestId = context.HttpContext.TraceIdentifier
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            _logger.LogError(context.Exception.Message, context.Exception);

            context.ExceptionHandled = true;
        }
    }
}
