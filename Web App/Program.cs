using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web_App.Middlewares;
using Repository_Pattern;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main()
    {
        WebApplicationBuilder Builder = WebApplication.CreateBuilder();
        Builder.Services.AddControllersWithViews();

        //Repository Pattern X), Single Instance Per Request
        Builder.Services.AddDbContext<MyDBContext>(
            (dbContext) =>
            {
                dbContext
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Builder.Configuration.GetConnectionString("MyDB"));

            }
            );
        Builder.Services.AddScoped<MyDBContext>();
        Builder.Services.AddScoped<UnitOfWork>();
        Builder.Services.AddScoped<ProductManager>();
        Builder.Services.AddScoped<CategoryManager>();


        WebApplication Web = Builder.Build();
        //Web.UseMiddleware<ProductMiddleware>();
        //Web.UseMiddleware<CategoryMiddleware>();
        Web.UseStaticFiles();

        //Web.UseStaticFiles(new StaticFileOptions
        //{
        //    FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/wwwroot"),
        //    RequestPath = ("")
        //});


        //Web.MapDefaultControllerRoute();
        Web.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{ID?}");
        Web.Run();
    }
}