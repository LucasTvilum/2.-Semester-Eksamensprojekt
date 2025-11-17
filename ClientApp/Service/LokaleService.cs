using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class LokaleService : ILokaler
{
    private HttpClient http;
    
    private string url = "http://localhost:5267";

    public LokaleService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Lokale[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var Lokalelist = await http.GetFromJsonAsync<Lokale[]>($"{url}/api/todo");
       
        return lokalelist;
    }

    public async Task Add(Lokale lokale)
    {
        await http.PostAsJsonAsync($"{url}/api/todo", lokale);
    }

    public async Task Delete(string todoid)
    {
        await http.DeleteAsync($"{url}/api/todo/{todoid}");
    }
}