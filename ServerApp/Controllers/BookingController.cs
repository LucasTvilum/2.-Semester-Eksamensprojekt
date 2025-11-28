using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {


        private IBookingInterface bookingRepo;

        public BookingController(IBookingInterface bookingRepo) {
            this.bookingRepo = bookingRepo;
        }

        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return bookingRepo.GetAll();
        }
        
        [HttpPost]
        public void Add([FromBody]Booking booking) {
            Console.WriteLine("Add bookingservice");
            bookingRepo.Add(booking);
        }


        [HttpPut("{id}")]
        public ActionResult<Booking> Update(string id, [FromBody] Booking booking)
        {
            booking.BookingId = id;
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