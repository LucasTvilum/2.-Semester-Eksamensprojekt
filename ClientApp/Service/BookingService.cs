using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

public class BookingService : IBooking
{
    private HttpClient http;

    public BookingService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Booking[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var BookingList = await http.GetFromJsonAsync<Booking[]>("/api/booking");

        return BookingList;
    }
    
    public async Task<Booking> GetBookingById(string  bookingid)
    {
        Console.WriteLine("GetAll from mock");
        var booking = await http.GetFromJsonAsync<Booking>($"/api/booking/{bookingid}");

        return booking;
    }

    public async Task Add(Booking booking)
    {
        Console.WriteLine("Add bookingservice attempted" + booking.CustomerId);
        Console.WriteLine("Add bookingservice date" + booking.Date);
        var response = await http.PostAsJsonAsync("/api/booking", booking);
        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);
            throw new Exception($"Booking create failed: {errorText}");
        }

        Console.WriteLine("Booking created successfully");
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"/api/booking/{id}");
    }
    

    public async Task UpdateBooking(Booking booking)
    {
        await http.PutAsJsonAsync<Booking>($"/api/booking/{booking.Id}", booking);
    }
    
}