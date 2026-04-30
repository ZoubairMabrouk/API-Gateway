namespace API_Gateway.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string Header = "X-Correlation-ID";

        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(Header))
            {
                context.Request.Headers[Header] = Guid.NewGuid().ToString();
            }

            context.Response.Headers[Header] =
                context.Request.Headers[Header]!;

            await _next(context);
        }
    }
}
