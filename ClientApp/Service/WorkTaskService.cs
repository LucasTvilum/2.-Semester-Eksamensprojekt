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

    public async Task<WorkTask> GetByBookingId(string bookingid)
    {
        try
        {
            return await http.GetFromJsonAsync<WorkTask>($"{url}/api/worktask/bybooking/{bookingid}");
        }
        catch
        {
            return null; // NotFound or other error
        }
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

    public async Task AddSubscription(Booking booking)
    {
    Console.WriteLine("Add subscription service attempted");

    var existingTask = await GetByBookingId(booking.Id);
    if (existingTask != null)
    {
        Console.WriteLine("Tasks already exist for this subscription — skipping creation.");
        return;
    }

    DateTime firstDate = GetNextWeekday(booking.Day);
    DateTime endDate = firstDate.AddMonths(24);

    // Key = Date, Value = WorkTask
    Dictionary<DateTime, WorkTask> tasks = new();

    // ----- Generate outside jobs -----
    DateTime outsideDate = firstDate;
    while (outsideDate <= endDate)
    {
        if (!tasks.ContainsKey(outsideDate))
        {
            tasks[outsideDate] = new WorkTask
            {
                BookingId = booking.Id,
                Date = outsideDate,
                InsideJob = false, // outside only
                Worker = new Worker { /* ... */ }
            };
        }

        outsideDate = outsideDate.AddDays(7 * booking.OutdoorInterval);
    }

    // ----- Generate inside jobs (only if inside is enabled) -----
    if (booking.InsideJob)
    {
        DateTime insideDate = firstDate;
        while (insideDate <= endDate)
        {
            // If outside job already exists → merge by setting InsideJob = true
            if (tasks.ContainsKey(insideDate))
            {
                tasks[insideDate].InsideJob = true;
            }
            else
            {
                // Should never happen since outside always exists,
                // but safe fallback:
                tasks[insideDate] = new WorkTask
                {
                    BookingId = booking.Id,
                    Date = insideDate,
                    InsideJob = true,
                    Worker = new Worker { /* ... */ }
                };
            }

            insideDate = insideDate.AddDays(7 * booking.InsideInterval);
        }
    }

    var taskList = tasks.Values.ToList();
    await http.PostAsJsonAsync($"{url}/api/worktask/subscription", taskList);
    }
    private DateTime GetNextWeekday(string weekday)
    {
        var today = DateTime.Today;
        var target = weekday switch
        {
            "Mandag" => DayOfWeek.Monday,
            "Tirsdag" => DayOfWeek.Tuesday,
            "Onsdag" => DayOfWeek.Wednesday,
            "Torsdag" => DayOfWeek.Thursday,
            "Fredag" => DayOfWeek.Friday,
            "Lørdag" => DayOfWeek.Saturday,
            "Søndag" => DayOfWeek.Sunday,
            _ => DayOfWeek.Monday
        };

        int daysToAdd = ((int)target - (int)today.DayOfWeek + 7) % 7;
        if (daysToAdd == 0) daysToAdd = 7;

        return today.AddDays(daysToAdd);
    }

    public async Task AddSingleBooking(Booking booking)
    {
        Console.WriteLine("Add worktask booking service attempted");
        
        var existingTask = await GetByBookingId(booking.Id);
        if (existingTask != null)
        {
            Console.WriteLine("Tasks already exist for this booking — skipping creation.");
            return;
        }
        
        WorkTask worktask = new WorkTask
            {
                BookingId = booking.Id,
                Date = booking.Date, 
                InsideJob = booking.InsideJob,                
                Worker = new Worker
                {
                    Username = "lucas",
                    Password = "Lucas",
                    Admin = true,
                    Name = "lucas",
                    PhoneNumber = "123123",
                    Mail = "lucas@mail.com"
                }
            };
       
        await http.PostAsJsonAsync($"{url}/api/worktask/singlebooking", worktask);
        
    }

    public async Task Delete(string id)
    {
        await http.DeleteAsync($"{url}/api/worktask/{id}");
    }
    

    public async Task UpdateWorkTask(WorkTask worktask)
    {
        await http.PutAsJsonAsync<WorkTask>($"{url}/api/worktask/{worktask.Id}", worktask);
    }

    public async Task DeleteAllWorkTaskFromBookingId(string bookingid)
    {
        await http.DeleteAsync($"{url}/api/worktask/deletetasks/{bookingid}");
    }
}