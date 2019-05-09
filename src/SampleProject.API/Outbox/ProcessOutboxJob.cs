using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Quartz;
using SampleProject.API.Payments;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Customers;

namespace SampleProject.API.Outbox
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly OrdersContext _ordersContext;
        private readonly IUnitOfWork _unitOfWork;

        public ProcessOutboxJob(
            IMediator mediator,
            OrdersContext ordersContext, 
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _ordersContext = ordersContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = _ordersContext.OutboxMessages.Where(x => x.ProcessedDate == null).ToList();

            foreach (var message in messages)
            {
                Type type = Assembly.GetAssembly(typeof(PaymentCreatedNotification)).GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type);

                await this._mediator.Publish((INotification)request);

                message.ProcessedDate = DateTime.UtcNow;

                await this._unitOfWork.CommitAsync();
            }
        }
    }
}