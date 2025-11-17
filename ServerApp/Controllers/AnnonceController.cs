using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/annonce")]
    public class AnnonceController : ControllerBase
    {

       
        private IAnnonceRepository annonceRepo;

        public AnnonceController(IAnnonceRepository annonceRepo) {
            this.annonceRepo = annonceRepo;
        }

        [HttpGet]
        public IEnumerable<Annonce> Get()
        {
            return annonceRepo.GetAll();
        }
        
        [HttpPost]
        public void Add(Annonce annonce) {
            annonceRepo.Add(annonce);
        }


        [HttpGet("{id}")]
        public ActionResult<Annonce> Update(string id, [FromBody] Annonce annonce)
        {
            annonce.Id = id;
            var updated =  annonceRepo.Update(annonce);
            if ((updated = null) != null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete]
        [Route("delete")]
        public void DeleteByQuery([FromQuery] string id)
        {
            annonceRepo.Delete(id);
        }
        
    }
}