namespace FurnitureStore3.Middleware
{
    public class ProductsProtectionMiddleware    
    {
        private readonly RequestDelegate _next;

        public ProductsProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().StartsWith("/products/"))
            {
                if (httpContext.User.Identity?.IsAuthenticated != true)
                {
                    httpContext.Response.Redirect("/user/login/");
                    return Task.CompletedTask;
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ProductsProtectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseProductsProtection(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProductsProtectionMiddleware>();
        }
    }
}
