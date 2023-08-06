﻿namespace ThreadsBackend.Middleware;

using System.Net;
using ThreadsBackend.DTOs;
using ThreadsBackend.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAscync(context, ex);
        }
    }

    public async Task HandleExceptionAscync(HttpContext context, Exception e)
    {
        this._logger.LogError(e.ToString());

        var statusCode = GetStatusCode(e);
        var error = new ErrorResponseDTO
        {
            Message = e.Message,
            Endpoint = context.Request.Path,
            Timestamp = DateTime.UtcNow,
            StatusCode = statusCode,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadHttpRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            NotFoundException => (int)HttpStatusCode.NoContent,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    }
}
