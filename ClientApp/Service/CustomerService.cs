using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class CustomerService : ICustomer
{
    private HttpClient http;

    private string url = "http://localhost:5107";

    public CustomerService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Customer[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var BookingList = await http.GetFromJsonAsync<Customer[]>($"{url}/api/customer/");

        return BookingList;
    }

    public async Task Add(Customer customer)
    {
        Console.WriteLine("Add userservice attemppted");
        var response = await http.PostAsJsonAsync($"{url}/api/customer", customer);
        if (!response.IsSuccessStatusCode)
        {
            // Read error body (this contains ModelState errors)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Customer create failed: {errorText}");
        }

        Console.WriteLine("Customer created successfully");
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/customer/{id}");
    }
    

    public async Task UpdateBooking(Customer customer)
    {
        await http.PutAsJsonAsync<User>($"{url}/api/customer/{customer.Id}", customer);
    }
    
}