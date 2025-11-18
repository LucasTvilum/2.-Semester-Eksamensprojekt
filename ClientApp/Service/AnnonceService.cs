using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class AnnonceService : IAnnonce
{
    private HttpClient http;
    
    private string url = "http://localhost:5107";

    public AnnonceService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Annonce[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var todoitemlist = await http.GetFromJsonAsync<Annonce[]>($"{url}/api/annonce/");
       
        return todoitemlist;
    }

    public async Task Add(Annonce annonce)
    {
        Console.WriteLine("Add annonceservice");
        await http.PostAsJsonAsync($"{url}/api/annonce", annonce);
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/annonce/{id}");
    }
}