using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class WindowService : IWindow
{
    private HttpClient http;

    private string url = "http://localhost:5107";

    public WindowService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Window[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var windowArray = await http.GetFromJsonAsync<Window[]>($"{url}/api/window/");

        return windowArray;
    }
    public async Task Add(Window window)
    {
        Console.WriteLine("Add bookingservice attempted");
        var response = await http.PostAsJsonAsync($"{url}/api/window", window);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Window create failed: {errorText}");
        }

        Console.WriteLine("Window created successfully");
    }
    
    public async Task AddLocation(WindowLocation windowlocation)
    {
        Console.WriteLine("Add bookingservice attempted");
        var response = await http.PostAsJsonAsync($"{url}/api/window/location", windowlocation);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Window create failed: {errorText}");
        }

        Console.WriteLine("Window created successfully");
    }

    
    public async Task AddType(WindowType windowtype)
    {
        Console.WriteLine("Add bookingservice attempted");
        var response = await http.PostAsJsonAsync($"{url}/api/window/type", windowtype);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Window create failed: {errorText}");
        }

        Console.WriteLine("Window created successfully");
    }


    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/window/{id}");
    }
    

    public async Task UpdateWindow(Window window)
    {
        await http.PutAsJsonAsync<Window>($"{url}/api/window/{window.Id}", window);
    }

    public async Task<decimal> CalculatePrice(List<Window> windows)
    {
        var response = await http.PostAsJsonAsync($"{url}/api/window/calculate", new { Windows = windows });
        return await response.Content.ReadFromJsonAsync<decimal>();
    }
    
    public async Task<List<WindowType>> GetAllWindowTypes()
    {
        Console.WriteLine("GetAll windowtypes");
        var windowtypes = await http.GetFromJsonAsync<List<WindowType>>($"{url}/api/window/type");

        return windowtypes;
    }
    public async Task<List<WindowLocation>> GetAllWindowLocations()
    {
        Console.WriteLine("GetAll windowlocations");
        var windowlocations = await http.GetFromJsonAsync<List<WindowLocation>>($"{url}/api/window/location");

        return windowlocations;
    }
}