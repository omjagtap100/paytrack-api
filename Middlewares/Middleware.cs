using System.Globalization;
using System.Text;
namespace paytrack_api.Middlewares
{
    public class RequestLocaleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestLocaleMiddleware(RequestDelegate next, ILogger<RequestLocaleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Console.WriteLine($"Method: {context.Request.Method}");
            Console.WriteLine($"Path: {context.Request.Path}");
            string requestBody = "";
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            _logger.LogInformation("Request Body:{@requestBody}", requestBody);
      

            await _next(context);
        }

    }
    public static class RequestLocaleMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLocale(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLocaleMiddleware>();
        }
    }
}
