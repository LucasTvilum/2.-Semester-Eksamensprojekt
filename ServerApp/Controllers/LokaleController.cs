using Microsoft.AspNetCore.Mvc;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/lokale")]
    public class LokaleController : ControllerBase
    {
        private readonly ILokaleRepository _repo;

        public LokaleController(ILokaleRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public ActionResult<List<Lokale>> GetAll()
        {
            return Ok(_repo.GetAll());
        }
        
        [HttpGet("{id}")]
        public ActionResult<Lokale> GetById(string id)
        {
            var item = _repo.GetAll();
            if (item == null) return NotFound();
            return Ok(item);
        }
        
        [HttpPost]
        public ActionResult<Lokale> Add([FromBody] Lokale lokale)
        {
            var added = _repo.Add(lokale);
            return Created("", added);
        }
        
        [HttpPut("{id}")]
        public async Task UpdateTime(Lokale lokale)
        {
            _repo.Update(lokale);
        }
    }
}