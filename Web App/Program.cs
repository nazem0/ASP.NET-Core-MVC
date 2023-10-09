using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web_App.Middlewares;

public class Program
{
    public static void Main()
    {
        WebApplicationBuilder Builder = WebApplication.CreateBuilder();
        Builder.Services.AddControllersWithViews();
        WebApplication Web = Builder.Build();
        Web.UseMiddleware<ProductMiddleware>();
        Web.UseMiddleware<CategoryMiddleware>();
        //Web.MapDefaultControllerRoute();
        Web.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{ID?}");
        Web.Run();
    }
}