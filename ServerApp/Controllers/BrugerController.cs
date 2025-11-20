using Microsoft.AspNetCore.Mvc;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/bruger")]
    public class BrugerController : ControllerBase
    {
        private readonly IBrugerRepository _repo;
        public record LoginRequest(string Name, string Password);


        public BrugerController(IBrugerRepository repo)
        {
            _repo = repo;
        }
        
        [HttpPost]
        public ActionResult<Bruger> Add([FromBody] Bruger bruger)
        {
            var added = _repo.Add(bruger);
            return Created("", added);
        }
        
        [HttpPost("login")]
        public Bruger Login([FromBody]  LoginRequest login)
        {
            var bruger = _repo.GetAll().FirstOrDefault(b => b.Name == login.Name && b.Password == login.Password);

            if (bruger == null)
          Console.WriteLine("bruger ikkee fundet");

            if (bruger.Password != login.Password)
               Console.WriteLine("password fejl");

            return bruger;
        }
        
        [HttpGet]
        public ActionResult<List<Bruger>> GetAll()
        {
            return Ok(_repo.GetAll());
        }
    }
}