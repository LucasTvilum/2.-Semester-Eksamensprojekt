using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class BrugerService : IBruger
{
    private HttpClient http;
    
    private string url = "http://localhost:5107";

    public BrugerService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Bruger[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var todoitemlist = await http.GetFromJsonAsync<Bruger[]>($"{url}/api/bruger");
       
        return todoitemlist;
    }

    public async Task Add(Bruger bruger)
    {
        await http.PostAsJsonAsync($"{url}/api/bruger", bruger);
    }

    public async Task Delete(string brugerid)
    {
        await http.DeleteAsync($"{url}/api/bruger/{brugerid}");
    }
    
    public async Task<Bruger> Authenticate(string username, string password)
    {
        Console.WriteLine("login attempt");
        var loginData = new { Username = username, Password = password };
    
        var response = await http.PostAsJsonAsync("api/bruger/", loginData);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("login failed");
            return null; // login failed
        }

        var user = await response.Content.ReadFromJsonAsync<Bruger>();
        return user;
    }
}