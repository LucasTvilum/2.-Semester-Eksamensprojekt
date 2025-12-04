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
        
        private IWindowTypeRepository windowTypeRepo;
        
        private IWindowLocationRepository windowLocationRepo;

        public WindowController(IWindowRepository windowRepo, IWindowTypeRepository windowTypeRepo, IWindowLocationRepository windowLocationRepo) {
            this.windowRepo = windowRepo;
            this.windowTypeRepo = windowTypeRepo;
            this.windowLocationRepo = windowLocationRepo;
        }

        [HttpGet]
        public IEnumerable<Window> Get()
        {
            return windowRepo.GetAll();
        }
        
        [HttpGet("type")]
        public List<WindowType> GetWindowTypeList()
        {
            return windowTypeRepo.GetAll();
        }
        
        [HttpGet("location")]
        public List<WindowLocation> GetWindowLocationList()
        {
            return windowLocationRepo.GetAll();
        }
        
        [HttpPost]
        public void Add([FromBody]Window window) {
            Console.WriteLine("Add window in controller");
            windowRepo.Add(window);
        }
        
        [HttpPost ("location")]
        public void AddLocation([FromBody]WindowLocation windowLocation) {
            Console.WriteLine("Add windowlocation in controller");
            windowLocationRepo.Add(windowLocation);
        }
        
        [HttpPost("type")]
        public void AddType([FromBody]WindowType windowType) {
            Console.WriteLine("Add windowtype in controller");
            windowTypeRepo.Add(windowType);
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