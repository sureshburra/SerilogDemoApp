using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Settings.Configuration;
namespace SerilogDemoApp;

public class Program
{
    public static void Main(string[] args)
    {
        /*
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
        */

        // Create a builder for the web application
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration)
                         .Enrich.FromLogContext();
                         //.WriteTo.Console(); // Due to this its logging twice to console
        });

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();
        app.UseSerilogRequestLogging();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        try
        {
            Log.Information("Application starting up");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed to start correctly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
