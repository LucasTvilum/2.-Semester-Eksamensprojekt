using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using ClientApp.Service;

namespace ClientApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var apiBaseUrl = Environment.GetEnvironmentVariable("ApiBaseUrl") 
                         ?? builder.HostEnvironment.BaseAddress;

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl)
        });

        // Configure HttpClient with environment-specific API URL
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl)
        });

        // Register services
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddSingleton<UserState>();
        builder.Services.AddScoped<IBooking, BookingService>();
        builder.Services.AddScoped<IWorkTask, WorkTaskService>();
        builder.Services.AddScoped<IWindow, WindowService>();
        builder.Services.AddScoped<IUser, UserService>();

        await builder.Build().RunAsync();
    }
}