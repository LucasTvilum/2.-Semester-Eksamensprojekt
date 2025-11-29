using Microsoft.AspNetCore.Mvc;
using ServerApp.Repository;
using Core.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {


        private ICustomerRepository customerRepo;

        public CustomerController(ICustomerRepository customerRepo) {
            this.customerRepo = customerRepo;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customerRepo.GetAll();
        }
        
        [HttpPost]
        public void Add([FromBody]Customer customer) {
            Console.WriteLine("Add customer in controller");
            customerRepo.Add(customer);
        }


        [HttpPut("{id}")]
        public ActionResult<Customer> Update(string id, [FromBody] Customer customer)
        {
            customer.Id = id;
            var updatedcustomer = customerRepo.Update(customer);
            if (updatedcustomer == null) return NotFound();
            return Ok(updatedcustomer);
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public OkResult Delete(string id)
        {
            customerRepo.Delete(id);
            return Ok();
        }
    }
    
}