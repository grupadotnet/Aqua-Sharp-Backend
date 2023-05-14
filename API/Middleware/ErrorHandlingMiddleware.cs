using Aqua_Sharp_Backend.Exceptions;

namespace Aqua_Sharp_Backend.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequest400Exception badRequest400Exception)
        {
            _logger.LogError(badRequest400Exception, badRequest400Exception.Message);
            
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequest400Exception.Message);
        }
        catch (Unauthorized401Exception unauthorized401Exception)
        {
            _logger.LogError(unauthorized401Exception, unauthorized401Exception.Message);
            
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(unauthorized401Exception.Message);
        }
        catch (Forbidden403Exception forbidden403Exception)
        {
            _logger.LogError(forbidden403Exception, forbidden403Exception.Message);
            
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbidden403Exception.Message);
        }
        catch (NotFound404Exception notFound404Exception)
        {
            _logger.LogError(notFound404Exception, notFound404Exception.Message);
            
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound404Exception.Message);
        }
        catch (RequestTimeout408Exception requestTimeout408Exception)
        {
            _logger.LogError(requestTimeout408Exception, requestTimeout408Exception.Message);
            
            context.Response.StatusCode = 408;
            await context.Response.WriteAsync(requestTimeout408Exception.Message);
        }
        catch (Conflict409Exception conflict409Exception)
        {
            _logger.LogError(conflict409Exception, conflict409Exception.Message);
            
            context.Response.StatusCode = 409;
            await context.Response.WriteAsync(conflict409Exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"Upss... Something went wrong :( \n{exception}");
        }
    }
}