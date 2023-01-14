namespace ASP.NET_Core_useful_techniques.Infrastructure.Extensions
{
    using ASP.NET_Core_useful_techniques.Infrastructure.Middleware;
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }

    }
}
