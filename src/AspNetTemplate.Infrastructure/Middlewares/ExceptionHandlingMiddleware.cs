using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Services;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace AspNetTemplate.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        bool hasException = false;

        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);

            hasException = true;
        }

        if (!hasException) await HandleErrorResponseAsync(context);
    }

    private static async Task HandleErrorResponseAsync(HttpContext context)
    {
        if (context.Response.StatusCode >= StatusCodes.Status400BadRequest)
        {
            ProblemDetailsDto response = new(
                LocalizationService.GetMessage(Message.UnauthorizedOperation) ?? "",
                context.Response.StatusCode,
                context.Request.Path
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string instancePath = context.Request.Path;

        string exceptionMessage = exception.Message switch
        {
            "Value cannot be null. (Parameter 'request')" => LocalizationService.GetMessage(Message.UnexpectedRequestError) ?? "",
            _ => exception.Message
        };

        ProblemDetailsDto response = exception switch
        {
            ArgumentNullException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status400BadRequest, instancePath),
            BadRequestException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status400BadRequest, instancePath),
            ConflictException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status409Conflict, instancePath),
            NotFoundException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status404NotFound, instancePath),
            UnauthorizedException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status401Unauthorized, instancePath),
            ValidationException => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status400BadRequest, instancePath),
            _ => new ProblemDetailsDto(exceptionMessage, StatusCodes.Status500InternalServerError, instancePath)
        };

        context.Response.StatusCode = response.Status;

        await context.Response.WriteAsJsonAsync(response);
    }
}
