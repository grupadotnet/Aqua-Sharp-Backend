using Aqua_Sharp_Backend.Exceptions;

namespace Aqua_Sharp_Backend.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequest400Exception badRequest400Exception)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequest400Exception.Message);
        }
        catch (Unauthorized401Exception unauthorized401Exception)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(unauthorized401Exception.Message);
        }
        catch (Forbidden403Exception forbidden403Exception)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbidden403Exception.Message);
        }
        catch (NotFound404Exception notFound404Exception)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound404Exception.Message);
        }
        catch (RequestTimeout408Exception requestTimeout408Exception)
        {
            context.Response.StatusCode = 408;
            await context.Response.WriteAsync(requestTimeout408Exception.Message);
        }
        catch (Conflict409Exception conflict409Exception)
        {
            context.Response.StatusCode = 409;
            await context.Response.WriteAsync(conflict409Exception.Message);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Upss... Something went wrong :(");
        }
    }
}