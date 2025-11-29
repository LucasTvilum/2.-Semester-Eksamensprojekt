using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class UserService : IUser
{
    private HttpClient http;

    private string url = "http://localhost:5107";

    public UserService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<User[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var BookingList = await http.GetFromJsonAsync<User[]>($"{url}/api/user/");

        return BookingList;
    }

    public async Task Add(User user)
    {
        Console.WriteLine("Add userservice attemppted");
        var response = await http.PostAsJsonAsync($"{url}/api/user", user);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"User create failed: {errorText}");
        }

        Console.WriteLine("User created successfully");
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/user/{id}");
    }
    

    public async Task UpdateBooking(User user)
    {
        await http.PutAsJsonAsync<User>($"{url}/api/user/{user.Id}", user);
    }
    
}