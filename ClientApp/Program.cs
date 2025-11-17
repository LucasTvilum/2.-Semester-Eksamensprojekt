using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
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

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddSingleton<IAnnonce, AnnonceService>();
        builder.Services.AddSingleton<IBruger, BrugerService>();
        builder.Services.AddSingleton<ILokaler, LokaleService>();

        await builder.Build().RunAsync();       
    }
}