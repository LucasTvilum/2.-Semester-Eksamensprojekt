using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        Console.WriteLine("Runtime type service: " + user.GetType().Name);

        if (user is Customer customer)
        {
            Console.WriteLine("serviceCustomer Address: " + customer.Address);
            Console.WriteLine("serviceCustomer Region: " + customer.Region);
            Console.WriteLine("serviceCustomer City: " + customer.City);
        }

        Console.WriteLine("Add userservice attempted");

        // Cast to runtime type to include derived properties
        //Chatgpt magi for at fixe inheritance, uden det så bliver ekstra customer fields ikke inkluderet i payload
        object payload = user switch
        {
            Customer c => c,
            Worker w => w,
            _ => user
        };

        var response = await http.PostAsJsonAsync($"{url}/api/user", payload);

        if (!response.IsSuccessStatusCode)
        {
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
    
    public async Task<User> ValidateUser(User user)
    {
        Console.WriteLine("Validate user service");
        
        
        var response = await http.PutAsJsonAsync<User>($"{url}/api/user/login/", user);
        
        if (response.IsSuccessStatusCode)
        {
            
            var usertoreturn = await response.Content.ReadFromJsonAsync<User>();
            
            return usertoreturn;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("Invalid username or password.");
            return null;
        }
        else
        {
            Console.WriteLine($"Unexpected status code: {response.StatusCode}");
            return null;
        }
        
    }

    public async Task<List<Worker>> GetWorkers()
    {
        var WorkerList = await http.GetFromJsonAsync<List<Worker>>($"{url}/api/user/workers");
        return WorkerList;
    }

    public async Task<List<Customer>> GetCustomers()
    {
        var CustomerList = await http.GetFromJsonAsync<List<Customer>>($"{url}/api/user/customers");
        return CustomerList;
    }
}