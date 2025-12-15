using System.Net.Http.Json;
using Core.Models;


namespace ClientApp.Service;

public class WorkTaskService : IWorkTask
{
    private readonly UserState _userState;
    private HttpClient http;

    private string url = "http://localhost:5107";

    public WorkTaskService(HttpClient http, UserState userState)
    {
        this.http = http;
        _userState = userState;
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

    Worker worker = new Worker();

    var currentUser = _userState.CurrentUser;

    //Inheritance casting currentUser to worker object
    if (currentUser is Worker w)
    {
        worker = w;   
    }

    DateTime firstDate = GetNextWeekday(booking.Day).Date;
    DateTime endDate = firstDate.AddMonths(24);

    // Key = Date, Value = WorkTask
    Dictionary<DateTime, WorkTask> tasks = new();

    // ----- Generate outside jobs -----
    DateTime outsideDate = firstDate;
    while (outsideDate <= endDate)
    {
        tasks[outsideDate] = new WorkTask
        {
            BookingId = booking.Id,
            Date = DateTime.SpecifyKind(outsideDate, DateTimeKind.Utc),
            InsideJob = false,
            WorkerId = worker.Id
        };

        outsideDate = outsideDate.AddDays(7 * booking.OutdoorInterval);
    }
    
    // ----- Generate inside jobs (only if inside is enabled) -----
    if (booking.InsideInterval != 0)
    {
        DateTime insideDate = firstDate;
        while (insideDate <= endDate)
        {
            if (tasks.TryGetValue(insideDate, out var existing))
            {
                existing.InsideJob = true;   // merge
            }
            else
            {
                tasks[insideDate] = new WorkTask
                {
                    BookingId = booking.Id,
                    Date = insideDate,
                    InsideJob = true,
                    WorkerId = worker.Id
                };
            }

            insideDate = insideDate.AddDays(7 * booking.InsideInterval);
        }
    }

    var taskList = tasks.Values.ToList();
    await http.PostAsJsonAsync($"{url}/api/worktask/subscription", taskList);
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
        
        Worker worker = new Worker();

        var currentUser = _userState.CurrentUser;

        //Inheritance casting currentUser to worker object
        if (currentUser is Worker w)
        {
            worker = w;   
        }
        
        WorkTask worktask = new WorkTask
            {
                BookingId = booking.Id,
                Date = booking.Date, 
                InsideJob = booking.InsideJob,                
                WorkerId = worker.Id
            };
       
        await http.PostAsJsonAsync($"{url}/api/worktask/singlebooking", worktask);
        
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
        await http.DeleteAsync($"{url}/api/worktask/deletebookings/{bookingid}");
    }
}