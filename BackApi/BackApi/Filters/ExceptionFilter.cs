using BackApi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebService.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public const string InternalErrorMessage = "An unexpected error occurred";

    private readonly Dictionary<Tuple<Type, string>, IActionResult> _brlExceptions = new()
    {
        {
            GetExceptionTuple(typeof(ReflectionServiceException), ReflectionServiceException.DllScanErrorMessage),
            FilterHelper.GetObjectResult(HttpStatusCode.InternalServerError, InternalErrorMessage)
        }
    };

    private static Tuple<Type, string> GetExceptionTuple(Type t, string exceptionMessage) =>
        new(t, exceptionMessage);

    public void OnException(ExceptionContext context)
    {
        Exception e = context.Exception;
        var t = new Tuple<Type, string>(e.GetType(), e.Message);

        if (_brlExceptions.TryGetValue(t, out var result))
        {
            context.Result = result;
            context.ExceptionHandled = true;
            return;
        }

        Console.WriteLine(e);
        context.Result = FilterHelper.GetObjectResult(HttpStatusCode.InternalServerError, InternalErrorMessage);
        context.ExceptionHandled = true;
    }
}
