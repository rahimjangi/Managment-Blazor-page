using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AbbyWeb.MiddleWares;

public class CustomCheck
{
    private readonly RequestDelegate _next;

    public CustomCheck(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.WriteAsync("This is from custome middleware"); 
    }

}
public static class MiddleWareExtensionCustom
{
    public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomCheck>();
    }
}
