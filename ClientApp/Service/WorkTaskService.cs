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

    public async Task AddSubscription(Booking booking)
    {
        Console.WriteLine("Add subscription service attempted");
        //Add a WorkTask for each interval outside and inside, starting on datetime.day.now using data from booking object
// 1️⃣ Find the first task date (next weekday based on booking.Day)
        DateTime firstDate = GetNextWeekday(booking.Day);

        // 2️⃣ Generate next 12 months (you can adjust)
        DateTime endDate = firstDate.AddMonths(24);

        List<WorkTask> tasksToCreate = new();

        // 3️⃣ OUTSIDE tasks
        DateTime outsideDate = firstDate;

        while (outsideDate <= endDate)
        {
            tasksToCreate.Add(new WorkTask
            {
                BookingId = booking.Id,
                Date = outsideDate,
                InsideJob = booking.InsideJob,                // outside
                Worker = new Worker
                {
                    Username = "lucas",
                    Password = "Lucas",
                    Admin = true,
                    Name = "lucas",
                    PhoneNumber = "123123",
                    Mail = "lucas@mail.com"
                }
                
            });

            outsideDate = outsideDate.AddDays(7 * booking.OutdoorInterval);
        }

        // 4️⃣ INSIDE tasks (only if booking.InsideJob == true)
        if (booking.InsideJob)
        {
            DateTime insideDate = firstDate;

            while (insideDate <= endDate)
            {
                tasksToCreate.Add(new WorkTask
                {
                    BookingId = booking.Id,
                    Date = insideDate,
                    InsideJob = true,              // inside
                    Worker = new Worker
                    {
                        Username = "lucas",
                        Password = "Lucas",
                        Admin = true,
                        Name = "lucas",
                        PhoneNumber = "123123",
                        Mail = "lucas@mail.com"
                    }
                });

                insideDate = insideDate.AddDays(7 * booking.InsideInterval);
            }

         
        }

        if (tasksToCreate.Count != 0)
        {
            await http.PostAsJsonAsync($"{url}/api/worktask/subscription", tasksToCreate);
        }
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
        
        WorkTask worktask = new WorkTask
            {
                BookingId = booking.Id,
                Date = booking.Date, //
                InsideJob = booking.InsideJob,                // outside
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
    
}