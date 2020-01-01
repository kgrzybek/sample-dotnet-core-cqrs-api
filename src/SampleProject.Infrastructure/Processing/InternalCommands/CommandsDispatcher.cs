using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Processing;
using SampleProject.Application.Customers;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly OrdersContext _ordersContext;

        public CommandsDispatcher(
            IMediator mediator, 
            OrdersContext ordersContext)
        {
            this._mediator = mediator;
            this._ordersContext = ordersContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var command = await this._ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(command.Type);
            var request = JsonConvert.DeserializeObject(command.Data, type);

            command.ProcessedDate = DateTime.UtcNow;

            await this._mediator.Send((IRequest)request);
        }
    }
}