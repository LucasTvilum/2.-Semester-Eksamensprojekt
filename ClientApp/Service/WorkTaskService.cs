using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class WorkTaskService : IWorkTask
{
    private HttpClient http;

    private string url = "http://localhost:5107";

    public WorkTaskService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<WorkTask[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var worktaskList = await http.GetFromJsonAsync<WorkTask[]>($"{url}/api/worktask/");

        return worktaskList;
    }

    public async Task Add(WorkTask worktask)
    {
        Console.WriteLine("Add worktask service attempted");
        var response = await http.PostAsJsonAsync($"{url}/api/worktask", worktask);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Booking create failed: {errorText}");
        }

        Console.WriteLine("Booking created successfully");
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/worktask/{id}");
    }
    

    public async Task UpdateWorkTask(WorkTask worktask)
    {
        await http.PutAsJsonAsync<WorkTask>($"{url}/api/worktask/{worktask.Id}", worktask);
    }
    
}