using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebMvc.Application.Services;
using WebMvc.Application.Services.AuthService;
using WebMvc.Application.Services.Configuration;

namespace WebMvc.Application;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.Configure<FileServiceConfiguration>(
            builder.Configuration.GetSection("FileServiceConfiguration"));

        builder.Services.AddSingleton<IAuthService, DummyAuthService>();
        builder.Services.AddSingleton<IFileService, FileService>();

        var app = builder.Build();

        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Auth}/{action=Login}");

        app.Run();
    }
}