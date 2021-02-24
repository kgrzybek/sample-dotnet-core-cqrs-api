﻿using Dapper;
using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.DomainEvents;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand, Unit>
    {
        private readonly IMediator _mediator;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessOutboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
        {
            System.Data.IDbConnection connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[OutboxMessage].[Id], " +
                               "[OutboxMessage].[Type], " +
                               "[OutboxMessage].[Data] " +
                               "FROM [app].[OutboxMessages] AS [OutboxMessage] " +
                               "WHERE [OutboxMessage].[ProcessedDate] IS NULL";

            System.Collections.Generic.IEnumerable<OutboxMessageDto> messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            System.Collections.Generic.List<OutboxMessageDto> messagesList = messages.AsList();

            const string sqlUpdateProcessedDate = "UPDATE [app].[OutboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";
            if (messagesList.Count > 0)
            {
                foreach (OutboxMessageDto message in messagesList)
                {
                    Type type = Assemblies.Application
                        .GetType(message.Type);
                    IDomainEventNotification request = JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification;

                    using (LogContext.Push(new OutboxMessageContextEnricher(request)))
                    {
                        await _mediator.Publish(request, cancellationToken);

                        await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                        {
                            Date = DateTime.UtcNow,
                            message.Id
                        });
                    }

                }
            }

            return Unit.Value;
        }

        private class OutboxMessageContextEnricher : ILogEventEnricher
        {
            private readonly IDomainEventNotification _notification;

            public OutboxMessageContextEnricher(IDomainEventNotification notification)
            {
                _notification = notification;
            }
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{_notification.Id.ToString()}")));
            }
        }
    }
}