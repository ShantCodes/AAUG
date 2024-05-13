using Newtonsoft.Json;

namespace AAUG.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new { error = ex.Message };

                var jsonResponse = JsonConvert.SerializeObject(response);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
