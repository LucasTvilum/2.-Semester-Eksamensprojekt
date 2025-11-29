using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {


        private IBookingRepository bookingRepo;

        public BookingController(IBookingRepository bookingRepo) {
            this.bookingRepo = bookingRepo;
        }

        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return bookingRepo.GetAll();
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]Booking booking) {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Console.WriteLine("Add bookingcontroller");
            bookingRepo.Add(booking);
            
            return Ok(booking);
        }


        [HttpPut("{id}")]
        public ActionResult<Booking> Update(string id, [FromBody] Booking booking)
        {
            booking.Id = id;
            var updated = bookingRepo.Update(booking);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public OkResult Delete(string id)
        {
            bookingRepo.Delete(id);
            return Ok();
        }
    }
    
}