using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class AnnonceService : IAnnonce
{
    private HttpClient http;
    
    private string url = "http://localhost:5267";

    public AnnonceService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Annonce[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var todoitemlist = await http.GetFromJsonAsync<Annonce[]>($"{url}/api/todo");
       
        return todoitemlist;
    }

    public async Task Add(Annonce item)
    {
        await http.PostAsJsonAsync($"{url}/api/todo", item);
    }

    public async Task Delete(string todoid)
    {
        await http.DeleteAsync($"{url}/api/todo/{todoid}");
    }
}