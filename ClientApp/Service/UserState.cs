namespace ClientApp.Service;
using Core.Models;
using Microsoft.AspNetCore.Components;
using ClientApp.Service;


public class UserState
{
    private readonly IBruger brugerService;
    
    public UserState(IBruger brugerService)
    {
        this.brugerService = brugerService;
    }
    public Bruger? CurrentUser { get; private set; }
    public bool IsAdmin { get; private set; }

    public event Action? OnChange;

    public async Task<bool> Login(string username, string password)
    {
        Console.WriteLine($"Login {username}");
        var user = await brugerService.Authenticate(username, password);
        Console.WriteLine("post await");
        if (user == null)
            return false;

        CurrentUser = user;
        
        IsAdmin = string.Equals(username, "Admin", StringComparison.OrdinalIgnoreCase)
                  && string.Equals(password, "Admin", StringComparison.Ordinal);

        
        NotifyStateChanged();
        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
        IsAdmin = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}