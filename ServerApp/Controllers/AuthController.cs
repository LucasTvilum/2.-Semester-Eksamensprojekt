using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest req, [FromServices] IBrugerRepository repo)
    {
        var exists = repo.GetAll().Any(u => u.Name == req.Name);
        if (exists) return Conflict("User already exists");

        var user = new Bruger { Name = req.Name, Password = req.Password };
        repo.Add(user);
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req, [FromServices] IBrugerRepository repo)
    {
        var user = repo.GetAll().FirstOrDefault(u => u.Name == req.Name && u.Password == req.Password);
        if (user is null) return Unauthorized();
        return Ok(new AuthResult(true, user.Name));
    }
}

public sealed record RegisterRequest(string Name, string Password);
public sealed record LoginRequest(string Name, string Password);
public sealed record AuthResult(bool Success, string? Name);