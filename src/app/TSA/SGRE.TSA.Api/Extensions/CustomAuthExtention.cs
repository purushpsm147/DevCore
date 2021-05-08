using Microsoft.AspNetCore.Builder;
using SGRE.TSA.Api.Middlewares;

namespace SGRE.TSA.Api.Extensions
{
    public static class CustomAuthExtention
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestAuthorizationMiddleware>();
        }
    }
}
