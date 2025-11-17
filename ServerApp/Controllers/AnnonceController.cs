using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/annonce")]
    public class AnnonceController : ControllerBase
    {

        private readonly IAnnonceRepository _repo;

        public AnnonceController(IAnnonceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<Annonce> GetAll()
        {
            return Ok(_repo.GetAll());
        }
        
        [HttpPost]
        public ActionResult<Annonce> Add([FromBody] Annonce annonce)
        {
            var added = _repo.Add(annonce);
            return CreatedAtAction(nameof(GetAll), new { id = added.Id }, added);
        }

        [HttpGet("{id}")]
        public ActionResult<Annonce> Update(string id, [FromBody] Annonce annonce)
        {
            annonce.Id = id;
            var updated =  _repo.Update(annonce);
            if ((updated = null) != null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _repo.GetAll().FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();
            _repo.Delete(item);
            return NoContent();
        }
        
    }
}