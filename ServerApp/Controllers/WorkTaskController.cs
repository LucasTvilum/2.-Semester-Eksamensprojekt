using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/worktask")]
    public class WorkTaskController : ControllerBase
    {
        private IWorkTaskRepository workTaskRepo;

        public WorkTaskController(IWorkTaskRepository workTaskRepo) {
            this.workTaskRepo = workTaskRepo;
        }

        [HttpGet]
        public IEnumerable<WorkTask> Get()
        {
            return workTaskRepo.GetAll();
        }
        
        [HttpPost]
        public void Add([FromBody]WorkTask workTask) {
            Console.WriteLine("Add worktask in controller");
            workTaskRepo.Add(workTask);
        }


        [HttpPut("{id}")]
        public ActionResult<WorkTask> Update(string id, [FromBody] WorkTask workTask)
        {
            workTask.Id = id;
            var updatedworktask = workTaskRepo.Update(workTask);
            if (updatedworktask == null) return NotFound();
            return Ok(updatedworktask);
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public OkResult Delete(string id)
        {
            workTaskRepo.Delete(id);
            return Ok();
        }
    }
    
}