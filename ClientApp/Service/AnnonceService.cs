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
        var annonceliste = await http.GetFromJsonAsync<Annonce[]>($"{url}/api/annonce/");

        return annonceliste;
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

    public async Task UpdateAnnonce(Annonce annonce)
    {
        await http.PutAsJsonAsync<Annonce>($"{url}/api/annonce", annonce);
    }

    public async Task<List<Annonce>> GetFiltered(string type, double price, string color, string lokalenavn)
    {
        
        var filter = new Annonce
        {
            Type = type,
            Price = price,
            Color = color,
        };
       //List<Annonce> filtreretlist = await http.GetFromJsonAsync<List<Annonce>>($"{url}/api/annonce/", filter);
        List<Annonce> filtreretlist = new List<Annonce>();
        return filtreretlist;
    }
}

   