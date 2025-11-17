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
        public ActionResult Login([FromBody] Bruger login)
        {
            var bruger = _repo.GetAll().FirstOrDefault(b => b.Name == login.Name);

            if (bruger == null)
                return NotFound("Bruger findes ikke du");

            if (bruger.Password != login.Password)
                return BadRequest("Forkert kodeord homie");

            return Ok("Login succes");
        }
        
        [HttpGet]
        public ActionResult<List<Bruger>> GetAll()
        {
            return Ok(_repo.GetAll());
        }
    }
}