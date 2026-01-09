using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

/**
 * BookingService håndterer livscyklussen for en booking.
 * Den fungerer som bindeleddet mellem UI og API'et for alt, der har med
 * bestillinger, priser og aftaler at gøre.
 */
public class BookingService : IBooking
{
    private HttpClient http;

    public BookingService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Booking[]> GetAll()
    {
        // Henter alle bookinger (både abonnementer og enkeltbookinger)
        return await http.GetFromJsonAsync<Booking[]>("/api/booking");
    }
    
    public async Task<Booking> GetBookingById(string bookingid)
    {
        // Henter én specifik booking, f.eks. når den skal redigeres
        return await http.GetFromJsonAsync<Booking>($"/api/booking/{bookingid}");
    }

    public async Task Add(Booking booking)
    {
        // Sender en ny booking til serveren som JSON.
        // Bemærk: Her behøver vi ikke switch(payload), da Booking-klassen 
        // indeholder alle felter (type, dato, vinduesliste osv.) i samme klasse.
        var response = await http.PostAsJsonAsync("/api/booking", booking);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"Booking create failed: {errorText}");
        }
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"/api/booking/{id}");
    }
    
    public async Task UpdateBooking(Booking booking)
    {
        // Bruges når admin f.eks. ændrer status på en booking eller tilføjer flere vinduer.
        // Vi bruger PUT til opdateringer.
        await http.PutAsJsonAsync<Booking>($"/api/booking/{booking.Id}", booking);
    }
}