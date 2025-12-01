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
        
        [HttpPost]
        public void Add([FromBody]User user) {
            Console.WriteLine("Add user in controller");
            userRepo.Add(user);
        }
        
        [HttpPut("login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            
            Console.WriteLine("Controller login check");
            
            var matchinguser = userRepo.GetAll()
                .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (matchinguser == null)
                return Unauthorized("Invalid username or password.");
            
            
            //fix til at få databasen til at returnere enums korrekt
            // Core.Models. nødvendig fordi User er brugt til andet
            Console.WriteLine(matchinguser.Usertype);
            matchinguser.Usertype = Core.Models.User.ParseUserType((int)matchinguser.Usertype);
            Console.WriteLine(matchinguser.Usertype);
            return Ok(user);
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