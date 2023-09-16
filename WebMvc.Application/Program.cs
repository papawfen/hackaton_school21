using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebMvc.Application.Services;

namespace WebMvc.Application;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<IAuthService, DummyAuthService>();

        var app = builder.Build();

        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Auth}/{action=Login}");

        app.Run();
    }
}