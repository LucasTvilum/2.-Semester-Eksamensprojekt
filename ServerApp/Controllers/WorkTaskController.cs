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
        
        [HttpPost("subscription")]
        public void AddSubscription([FromBody]List<WorkTask> workTasks) {
            Console.WriteLine("Add worktask subscription in controller");
            workTaskRepo.AddSubscription(workTasks);
        }
        
        [HttpPost("singlebooking")]
        public void AddSingleBooking([FromBody]WorkTask workTask) {
            Console.WriteLine("Add worktask single booking in controller");
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
        public OkResult Delete(string id)
        {
            workTaskRepo.Delete(id);
            return Ok();
        }

    }
    
}