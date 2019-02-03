using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Orders.AddCustomerOrder;
using SampleProject.API.Orders.ChangeCustomerOrder;
using SampleProject.API.Orders.GetCustomerOrderDetails;
using SampleProject.API.Orders.GetCustomerOrders;
using SampleProject.API.Orders.RemoveCustomerOrder;

namespace SampleProject.API.Orders
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerOrdersController : Controller
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
        [Route("{customerId}/orders")]
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerOrders(Guid customerId)
        {
            var orders = await _mediator.Send(new GetCustomerOrdersQuery(customerId));

            return Ok(orders);
        }

        /// <summary>
        /// Get customer order details.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpGet]
        [ProducesResponseType(typeof(OrderDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerOrderDetails(
            [FromRoute]Guid orderId)
        {
            var orderDetails = await _mediator.Send(new GetCustomerOrderDetailsQuery(orderId));

            return Ok(orderDetails);
        }


        /// <summary>
        /// Add customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="request">Products list.</param>
        [Route("{customerId}/orders")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomerOrder(
            [FromRoute]Guid customerId, 
            [FromBody]CustomerOrderRequest request)
        {
           await _mediator.Send(new AddCustomerOrderCommand(customerId, request.Products));

           return Created(string.Empty, null);
        }

        /// <summary>
        /// Change customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        /// <param name="request">List of products.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeCustomerOrder(
            [FromRoute]Guid customerId, 
            [FromRoute]Guid orderId,
            [FromBody]CustomerOrderRequest request)
        {
            await _mediator.Send(new ChangeCustomerOrderCommand(customerId, orderId, request.Products));

            return Ok();
        }

        /// <summary>
        /// Remove customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpDelete]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveCustomerOrder(
            [FromRoute]Guid customerId,
            [FromRoute]Guid orderId)
        {
            await _mediator.Send(new RemoveCustomerOrderCommand(customerId, orderId));

            return Ok();
        }
    }
}
