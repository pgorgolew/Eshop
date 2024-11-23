using System.Net;
using System.Text.Json;
using Eshop.Contracts.Shared;
using Eshop.Domain.SeedWork;

namespace Eshop.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BusinessRuleValidationException ex)
        {
            await HandleBuisnessRuleException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptions(context, ex);
        }
    }

    private static Task HandleBuisnessRuleException(HttpContext context, BusinessRuleValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new ErrorDto(exception.Message, exception.Details);

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
    
    private static Task HandleExceptions(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorDto("Internal Server Error", exception.Message);

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}