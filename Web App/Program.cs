using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Repository_Pattern;

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
        Builder.Services.AddIdentity<User, IdentityRole>(
            (i) =>
            {
                i.User.RequireUniqueEmail = true;
                i.SignIn.RequireConfirmedPhoneNumber = false;
                i.SignIn.RequireConfirmedEmail = false;
                i.SignIn.RequireConfirmedAccount = false;
            }
            ).AddEntityFrameworkStores<MyDBContext>();
        Builder.Services.Configure<IdentityOptions>(i =>
        {
            i.Password.RequireNonAlphanumeric = false;
            i.Password.RequireUppercase = false;

        });
        Builder.Services.AddScoped<UnitOfWork>();
        Builder.Services.AddScoped<AccountManager>();
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
        Web.UseAuthentication();
        Web.UseAuthorization();
        Web.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{ID?}");
        Web.Run();
    }
}