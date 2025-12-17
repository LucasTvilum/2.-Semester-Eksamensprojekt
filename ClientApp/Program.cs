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
        
        
        //Compile-time C# switch, uses localhost if in debug mode (dev) and else uses HostEnvironment.BaseAddress
        #if DEBUG
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7255") });
        Console.WriteLine("COMPILE MODE: DEBUG");
        #else
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://server-cygzc9budzcuhfcy.switzerlandnorth-01.azurewebsites.net") });
        Console.WriteLine("COMPILE MODE: RELEASE");
        #endif
        

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