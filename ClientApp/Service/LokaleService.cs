using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class LokaleService : ILokaler
{
    private HttpClient http;
    
    private string url = "http://localhost:5107";

    public LokaleService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<List<Lokale>> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var Lokalelist = await http.GetFromJsonAsync<Lokale[]>($"{url}/api/lokale");
        
        return Lokalelist.ToList();
    }

    public async Task Add(Lokale lokale)
    {
        await http.PostAsJsonAsync($"{url}/api/lokale", lokale);
    }

    public async Task Delete(string todoid)
    {
        await http.DeleteAsync($"{url}/api/lokale/{todoid}");
    }

    public async Task UpdateTime(Lokale lokale, string time)
    {
        string id = lokale.Id;
        lokale.Åbningstid = time;
        await http.PutAsJsonAsync($"{url}/api/lokale/{id}", lokale);
    }
}