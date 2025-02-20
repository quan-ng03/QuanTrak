using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using backend.Services;

namespace backend.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER_NAME = "X-Api-Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApiValidator validator)
    {
        // First check if the header exist
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var value))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        // Then use the API Validator service to check if the key is correct
        if (!validator.Validate(value))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key mismatch.");
            return;
        }

        await _next(context);

    }
}
