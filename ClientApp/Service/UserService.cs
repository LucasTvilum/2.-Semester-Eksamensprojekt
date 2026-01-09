using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

/**
 * UserService bruges til at håndtere al kommunikation vedrørende brugere (Kunder og Medarbejdere) 
 * mellem klienten og serverens API. Den indkapsler HTTP-kald (GET, POST, PUT, DELETE) 
 * og sørger for korrekt type-håndtering, specielt ved brug af arv (Inheritance).
 */
public class UserService : IUser
{
    private HttpClient http;

    public UserService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<User[]> GetAll()
    {
        // Henter alle brugere uanset type
        var BookingList = await http.GetFromJsonAsync<User[]>("/api/user");
        return BookingList;
    }
    
    public async Task<User> Get(string userid)
    {
        // Henter en specifik bruger baseret på ID
        var user = await http.GetFromJsonAsync<User>($"/api/user/{userid}");
        return user;
    }

    public async Task Add(User user)
    {
        // Da 'User' er en basisklasse, vil standard JSON-serialisering kun tage felter fra 'User'.
        // Ved at bruge et 'switch' og cast'e til den specifikke type (Customer eller Worker), 
        // sikrer vi, at alle ekstra felter (som Adresse, Region osv.) kommer med i JSON-payloaden til serveren.
        object payload = user switch
        {
            Customer c => c,
            Worker w => w,
            _ => user
        };

        var response = await http.PostAsJsonAsync("/api/user", payload);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"User create failed: {errorText}");
        }
    }

    public async Task Delete(string id)
    {
        // Sletter en bruger via API'et
        await http.DeleteAsync($"/api/user/{id}");
    }
    
    public async Task<User> ValidateUser(User user)
    {
        // Bruges til login-validering. Sender brugeroplysninger til serveren,
        // som tjekker om login er korrekt og returnerer den fulde brugerprofil.
        var response = await http.PutAsJsonAsync<User>("/api/user/login", user);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return null; // Forkert login
        }
        return null;
    }

    // Specifikke kald til at hente filtrerede lister direkte fra backend
    public async Task<List<Worker>> GetWorkers()
    {
        return await http.GetFromJsonAsync<List<Worker>>("/api/user/workers");
    }

    public async Task<List<Customer>> GetCustomers()
    {
        return await http.GetFromJsonAsync<List<Customer>>("/api/user/customers");
    }
    
    public async Task<Customer> GetCustomerById(string customerid)
    {
        return await http.GetFromJsonAsync<Customer>($"/api/user/customers/{customerid}");
    }
}
