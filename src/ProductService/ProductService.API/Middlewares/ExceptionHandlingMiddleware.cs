using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ProductService.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Request.ContentType = "application/json";

                var errors = ex.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList());

                var json = JsonSerializer.Serialize(new { errors });
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(json);
            }
        }
    }
}
