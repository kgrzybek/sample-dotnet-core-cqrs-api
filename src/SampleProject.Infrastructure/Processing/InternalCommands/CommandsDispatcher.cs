using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Application.Customers;
using SampleProject.Infrastructure.Database;
using System;
using System.Reflection;
using System.Threading.Tasks;

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
            _mediator = mediator;
            _ordersContext = ordersContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            InternalCommand internalCommand = await _ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await _mediator.Send(command);
        }
    }
}