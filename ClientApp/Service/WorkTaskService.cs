using System.Net.Http.Json;
using Core.Models;

namespace ClientApp.Service;

// WorkTaskService implementerer IWorkTask interfacet (Client-side proxy)
public class WorkTaskService : IWorkTask
{
    private readonly UserState _userState; // Injiceret tilstand for at kende den aktuelle bruger
    private HttpClient http; // HttpClient til at kommunikere med API'en
    
    // Constructor-injection: Henter de nødvendige services udefra
    public WorkTaskService(HttpClient http, UserState userState)
    {
        this.http = http;
        _userState = userState;
    }

    // Henter alle arbejdsopgaver via et HTTP GET kald
    public async Task<WorkTask[]> GetAll()
    {
        Console.WriteLine("GetAll from mock");
        var worktaskList = await http.GetFromJsonAsync<WorkTask[]>("/api/worktask");

        return worktaskList;
    }

    // Henter en specifik opgave baseret på et BookingId
    public async Task<WorkTask> GetByBookingId(string bookingid)
    {
        try
        {
            // Forsøger at hente opgaven - returnerer null hvis API'en kaster en fejl (f.eks. 404)
            return await http.GetFromJsonAsync<WorkTask>($"/api/worktask/bybooking/{bookingid}");
        }
        catch
        {
            return null; // Fejlhåndtering hvis opgaven ikke findes
        }
    }

    // Tilføjer en enkelt arbejdsopgave direkte til databasen
    public async Task Add(WorkTask worktask)
    {
        Console.WriteLine("Add worktask service attempted");
        var response = await http.PostAsJsonAsync("/api/worktask", worktask);
        
        // Tjekker om serveren svarede med succes (status 200-299)
        if (!response.IsSuccessStatusCode)
        {
            // Hvis det fejler, udlæses fejlbeskeden fra serveren (f.eks. valideringsfejl)
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server returned error:");
            Console.WriteLine(errorText);

            throw new Exception($"Booking create failed: {errorText}");
        }

        Console.WriteLine("Booking created successfully");
    }

    // ALGORITME: Genererer en serie af arbejdsopgaver for et 2-årigt abonnement
    public async Task AddSubscription(Booking booking)
    {
        Console.WriteLine("Add subscription service attempted");

        // Idempotens-tjek: Sikrer at vi ikke opretter dubletter hvis de allerede findes
        var existingTask = await GetByBookingId(booking.Id);
        if (existingTask != null)
        {
            Console.WriteLine("Tasks already exist for this subscription — skipping creation.");
            return;
        }

        string workerid = "";
        var currentUser = _userState.CurrentUser;

        // Tildeling: Hvis en medarbejder opretter det, sættes de som ansvarlig med det samme
        if (currentUser.Usertype == User.UserType.Worker)
        {
            workerid = currentUser.Id;   
        }

        // Find den første dato baseret på ugedags-tekst (f.eks. næste mandag)
        DateTime firstDate = GetNextWeekday(booking.Day).Date;
        DateTime endDate = firstDate.AddMonths(24); // Abonnementet løber i 2 år

        // Dictionary bruges til at undgå dubletter på samme dato (Dato er nøglen)
        Dictionary<DateTime, WorkTask> tasks = new();

        // GENERERING AF UDVENDIGE OPGAVER
        DateTime outsideDate = firstDate;
        while (outsideDate <= endDate)
        {
            tasks[outsideDate] = new WorkTask
            {
                BookingId = booking.Id,
                Date = DateTime.SpecifyKind(outsideDate, DateTimeKind.Utc), // UTC sikrer ensartet tid på tværs af systemer
                InsideJob = false,
                WorkerId = workerid
            };

            // Hopper frem i tid baseret på OutdoorInterval (f.eks. hver 4. uge)
            outsideDate = outsideDate.AddDays(7 * booking.OutdoorInterval);
        }
        
        // GENERERING AF INDVENDIGE OPGAVER (Hvis valgt)
        if (booking.InsideInterval != 0)
        {
            DateTime insideDate = firstDate;
            while (insideDate <= endDate)
            {
                // Hvis der allerede findes en udvendig opgave denne dag, kombineres de (Merge)
                if (tasks.TryGetValue(insideDate, out var existing))
                {
                    existing.InsideJob = true;
                }
                else
                {
                    // Ellers oprettes en ren indvendig opgave
                    tasks[insideDate] = new WorkTask
                    {
                        BookingId = booking.Id,
                        Date = insideDate,
                        InsideJob = true,
                        WorkerId = workerid
                    };
                }

                insideDate = insideDate.AddDays(7 * booking.InsideInterval);
            }
        }

        // Konverterer Dictionary værdier til en liste og sender dem alle til API'en på én gang
        var taskList = tasks.Values.ToList();
        await http.PostAsJsonAsync("/api/worktask/subscription", taskList);
    }

    // Opretter en enkelt arbejdsopgave baseret på en enkeltbooking
    public async Task AddSingleBooking(Booking booking)
    {
        Console.WriteLine("Add worktask booking service attempted");
        
        var existingTask = await GetByBookingId(booking.Id);
        if (existingTask != null)
        {
            Console.WriteLine("Tasks already exist for this booking — skipping creation.");
            return;
        }

        string workerid = "";
        var currentUser = _userState.CurrentUser;

        // Tjekker om brugeren er en Worker (Arv/Polymorfisme tjek)
        if (currentUser.Usertype == User.UserType.Worker)
        {
            workerid = currentUser.Id;   
        }
        
        // Mapper data fra Booking til WorkTask objektet
        WorkTask worktask = new WorkTask
            {
                BookingId = booking.Id,
                Date = booking.Date, 
                InsideJob = booking.InsideJob,                
                WorkerId = workerid
            };
       
        await http.PostAsJsonAsync("/api/worktask/singlebooking", worktask);
    }
    
    // Hjælpefunktion til at beregne næste forekomst af en ugedag
    private DateTime GetNextWeekday(string weekday)
    {
        var today = DateTime.Today;
        // Bruger en switch expression til at konvertere tekst til DayOfWeek enum
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

        // Beregner antal dage til næste ugedag
        int daysToAdd = ((int)target - (int)today.DayOfWeek + 7) % 7;
        if (daysToAdd == 0) daysToAdd = 7; // Hvis det er i dag, vælger vi næste uge

        return today.AddDays(daysToAdd);
    }

    // Sletter en arbejdsopgave via HTTP DELETE
    public async Task Delete(string id)
    {
        await http.DeleteAsync($"/api/worktask/{id}");
    }
    
    // Opdaterer en arbejdsopgave via HTTP PUT
    public async Task UpdateWorkTask(WorkTask worktask)
    {
        await http.PutAsJsonAsync<WorkTask>($"/api/worktask/{worktask.Id}", worktask);
    }

    // Sletter alle arbejdsopgaver tilknyttet et bestemt BookingId
    public async Task DeleteAllWorkTaskFromBookingId(string bookingid)
    {
        await http.DeleteAsync($"/api/worktask/deletebookings/{bookingid}");
    }
}