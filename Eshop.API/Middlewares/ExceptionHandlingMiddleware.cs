using System.Net;
using System.Text.Json;
using Eshop.API.Service;
using Eshop.Application.Metrics;
using Eshop.Contracts.Shared;
using Eshop.Domain.SeedWork;

namespace Eshop.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, MetricsService metricsService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorType = ErrorTypeExtensions.FromException(ex);
            var metricName = errorType.ToMetricName();

            metricsService.RecordError(metricName);

            context.Response.ContentType = "application/json";
            var response = CreateErrorResponse(context, ex);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private static ErrorDto CreateErrorResponse(HttpContext context, Exception exception)
    {
        if (exception is BusinessRuleValidationException businessException)        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new ErrorDto(businessException.Message, businessException.Details);
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return new ErrorDto("Internal Server Error", exception.Message);
    }
}