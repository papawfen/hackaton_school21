using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebMvc.Application;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}");

        app.Run();
    }
}