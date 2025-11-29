using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/window")]
    public class WindowController : ControllerBase
    {
        private IWindowRepository windowRepo;

        public WindowController(IWindowRepository windowRepo) {
            this.windowRepo = windowRepo;
        }

        [HttpGet]
        public IEnumerable<Window> Get()
        {
            return windowRepo.GetAll();
        }
        
        [HttpGet("list")]
        public WindowList GetWindowList()
        {
            return windowRepo.GetWindowList();
        }
        
        [HttpPost]
        public void Add([FromBody]Window window) {
            Console.WriteLine("Add window in controller");
            windowRepo.Add(window);
        }


        [HttpPut("{id}")]
        public ActionResult<Window> Update(string id, [FromBody] Window window)
        {
            window.Id = id;
            var updatedwindow = windowRepo.Update(window);
            if (updatedwindow == null) return NotFound();
            return Ok(updatedwindow);
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public OkResult Delete(string id)
        {
            windowRepo.Delete(id);
            return Ok();
        }
    }
    
}