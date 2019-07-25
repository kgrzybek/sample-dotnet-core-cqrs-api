using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Orders.ChangeCustomerOrder;
using SampleProject.API.Orders.GetCustomerOrderDetails;
using SampleProject.API.Orders.GetCustomerOrders;
using SampleProject.API.Orders.PlaceCustomerOrder;
using SampleProject.API.Orders.RemoveCustomerOrder;

namespace SampleProject.API.Orders
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerOrdersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get customer orders.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <returns>List of customer orders.</returns> 
        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult<List<OrderDto>>> GetCustomerOrders(Guid customerId)
        {
            var orders = await _mediator.Send(new GetCustomerOrdersQuery(customerId));

            return Ok(orders);
        }

        /// <summary>
        /// Get customer order details.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        [HttpGet("{customerId}/orders/{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> GetCustomerOrderDetails(Guid orderId)
        {
            var orderDetails = await _mediator.Send(new GetCustomerOrderDetailsQuery(orderId));

            return Ok(orderDetails);
        }

        /// <summary>
        /// Add customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="request">Products list.</param>
        [HttpPost("{customerId}/orders")]
        public async Task<IActionResult> AddCustomerOrder(Guid customerId, CustomerOrderRequest request)
        {
            await _mediator.Send(new PlaceCustomerOrderCommand(customerId, request.Products));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// Change customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        /// <param name="request">List of products.</param>
        [HttpPut("{customerId}/orders/{orderId}")]
        public async Task<IActionResult> ChangeCustomerOrder(Guid customerId, Guid orderId, CustomerOrderRequest request)
        {
            await _mediator.Send(new ChangeCustomerOrderCommand(customerId, orderId, request.Products));

            return Ok();
        }

        /// <summary>
        /// Remove customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        [HttpDelete("{customerId}/orders/{orderId}")]
        public async Task<IActionResult> RemoveCustomerOrder(Guid customerId, Guid orderId)
        {
            await _mediator.Send(new RemoveCustomerOrderCommand(customerId, orderId));

            return Ok();
        }
    }
}
