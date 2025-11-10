using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Filters;

public static class FilterHelper
{
    internal static ObjectResult GetObjectResult(HttpStatusCode statusCode, string message)
    {
        return new ObjectResult(new
        {
            InnerCode = statusCode.ToString(),
            Message = message
        })
        {
            StatusCode = (int)statusCode
        };
    }
}
