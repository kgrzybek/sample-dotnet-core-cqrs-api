using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Customers.RegisterCustomer;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerRequest request)
        {
            var customer = await _mediator.Send(new RegisterCustomerCommand(request.Email, request.Name));

            return Created(string.Empty, customer);
        }
    }
}
