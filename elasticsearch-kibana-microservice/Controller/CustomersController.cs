using Microsoft.AspNetCore.Mvc;
using elasticsearch_kibana_microservice.Services;
using elasticsearch_kibana_microservice.Models;

namespace elasticsearch_kibana_microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomersController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetAllCustomers")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _service.GetByIdAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _service.CreateAsync(customer);
            return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
        }
    }
}
