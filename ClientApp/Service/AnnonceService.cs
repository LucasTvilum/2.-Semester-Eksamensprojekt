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
        await http.PutAsJsonAsync<Annonce>($"{url}/api/annonce/{annonce.Id}", annonce);
    }

    public async Task<List<Annonce>> GetFiltered(string type, string size, double price, string color, string lokalenavn)
    {
        var filter = new AnnonceFilter()
        {
            Type = type,
            Size = size,
            Price = price,
            Color = color,
            lokale = lokalenavn
        };
        
        var response = await http.PostAsJsonAsync($"{url}/api/annonce/filter", filter);

        response.EnsureSuccessStatusCode();

        var filtreretListe = await response.Content.ReadFromJsonAsync<List<Annonce>>();

        return filtreretListe;
    }
}

   