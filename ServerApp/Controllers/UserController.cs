using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;



namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {


        private IUserRepository userRepo;
        public UserController(IUserRepository userRepo) {
            this.userRepo = userRepo;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepo.GetAll();
        }
        
        [HttpGet("customers")]
        public Task<List<Customer>> GetCustomers()
        {
            return userRepo.GetCustomers();
        }
        
        [HttpGet("workers")]
        public  Task<List<Worker>> GetWorkers()
        {
            return userRepo.GetWorkers();
        }
        
        [HttpPost]
        public IActionResult AddUser([FromBody] JsonElement body)
        {
            Console.WriteLine("Raw JSON received: ");
            Console.WriteLine(body.GetRawText());
            
        if (body.ValueKind != JsonValueKind.Object)
            return BadRequest("Invalid JSON");

        // Determine user type from JSON
        var userType = (User.UserType)body.GetProperty("usertype").GetInt32();
        
        string GetString(JsonElement element, string propertyName)
        {
            return element.TryGetProperty(propertyName, out var prop) ? prop.GetString() : null;
        }

        User newUser = userType switch
        {
            Core.Models.User.UserType.Customer => new Customer
            {
                Username = GetString(body, "username"),
                Password = GetString(body, "password"),
                Name = GetString(body, "name"),
                Mail = GetString(body, "mail"),
                PhoneNumber = GetString(body, "phoneNumber"),
                Address = GetString(body, "address"),
                Region = GetString(body, "region"),
                City = GetString(body, "city"),
                Usertype = userType
            },
            Core.Models.User.UserType.Worker => new Worker
            {
                Username = GetString(body, "username"),
                Password = GetString(body, "password"),
                Name = GetString(body, "name"),
                Mail = GetString(body, "mail"),
                PhoneNumber = GetString(body, "phoneNumber"),
                Admin = true,
                Usertype = userType
            },
            _ => new User
            {
                Username = GetString(body, "username"),
                Password = GetString(body, "password"),
                Name = GetString(body, "name"),
                Mail = GetString(body, "mail"),
                PhoneNumber = GetString(body, "phoneNumber"),
                Usertype = userType
            }
        };
        
        Console.WriteLine("Runtime type controller: " + newUser.GetType().Name);
        if (newUser is Customer customer)
        {
            Console.WriteLine($"controllerCustomer Address: {customer.Address}");
            Console.WriteLine($"controllerCustomer Region: {customer.Region}");
            Console.WriteLine($"controllerCustomer City: {customer.City}");
        }
        
        userRepo.Add(newUser);

        return Ok();
        }
        
        [HttpPut("login")]
        public ActionResult<User> Login([FromBody] User loginRequest)
        {
            Console.WriteLine("Controller login check");

            // Find user in repository
            var matchingUser = userRepo.GetAll()
                .FirstOrDefault(u => u.Username == loginRequest.Username 
                                     && u.Password == loginRequest.Password);

            if (matchingUser == null)
                return Unauthorized("Invalid username or password.");

            // Check runtime type
            if (matchingUser is Customer customer)
            {
                Console.WriteLine($"Login Customer: {customer.Username}, Address: {customer.Address}, City: {customer.City}");
            }
            else if (matchingUser is Worker worker)
            {
                Console.WriteLine($"Login Worker: {worker.Username}, Admin: {worker.Admin}");
            }
            else
            {
                Console.WriteLine($"Login User: {matchingUser.Username}");
            }

            // Return as-is; MongoDB mapping ensures Customer/Worker fields are included
            return Ok(matchingUser);
        }
        

        [HttpPut("{id}")]
        public ActionResult<User> Update(string id, [FromBody] User user)
        {
            user.Id = id;
            var updateduser = userRepo.Update(user);
            if (updateduser == null) return NotFound();
            return Ok(updateduser);
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public OkResult Delete(string id)
        {
            userRepo.Delete(id);
            return Ok();
        }
    }
    
}