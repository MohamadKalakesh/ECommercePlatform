using CustomerService.Application.Abstractions;
using CustomerService.Application.Customers.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService=customerService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer is null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterCustomerCommand command)
        {
            var newCustomer = await _customerService.CreateAsync(command);
            return CreatedAtAction(nameof(GetByEmail), new { email = newCustomer.Email }, newCustomer);
        }
    }
}
