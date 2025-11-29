using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class BookingService : IBooking
{
    private HttpClient http;

    private string url = "http://localhost:5107";

    public BookingService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Booking[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var BookingList = await http.GetFromJsonAsync<Booking[]>($"{url}/api/booking/");

        return BookingList;
    }

    public async Task Add(Booking booking)
    {
        Console.WriteLine("Add bookingservice attempted");
        var response = await http.PostAsJsonAsync($"{url}/api/booking", booking);
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
        await http.DeleteAsync($"{url}/api/booking/{id}");
    }
    

    public async Task UpdateBooking(Booking booking)
    {
        await http.PutAsJsonAsync<Booking>($"{url}/api/booking/{booking.Id}", booking);
    }
    
}