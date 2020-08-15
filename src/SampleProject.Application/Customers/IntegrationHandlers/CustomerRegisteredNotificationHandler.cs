﻿using MediatR;
using SampleProject.Application.Configuration.Processing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public CustomerRegisteredNotificationHandler(
            ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            // Send welcome e-mail message...

            await _commandsScheduler.EnqueueAsync(new MarkCustomerAsWelcomedCommand(
                Guid.NewGuid(),
                notification.CustomerId));
        }
    }
}