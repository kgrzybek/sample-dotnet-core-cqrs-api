using System;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Quartz;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API.Outbox
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMediator _mediator;

        public ProcessOutboxJob(
            ISqlConnectionFactory sqlConnectionFactory,
            IMediator mediator)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                string sql = "SELECT " +
                             "[OutboxMessage].[Id], " +
                             "[OutboxMessage].[Type], " +
                             "[OutboxMessage].[Data] " +
                             "FROM [app].[OutboxMessages] AS [OutboxMessage] " +
                             "WHERE [OutboxMessage].[ProcessedDate] IS NULL";
                var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

                foreach (var message in messages)
                {
                    Type type = Assembly.GetAssembly(typeof(PaymentCreatedNotification)).GetType(message.Type);
                    var notification = JsonConvert.DeserializeObject(message.Data, type);

                    try
                    {
                        await this._mediator.Publish((INotification)notification);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    

                    string sqlInsert = "UPDATE [app].[OutboxMessages] " +
                                       "SET [ProcessedDate] = @Date " +
                                       "WHERE [Id] = @Id";

                    await connection.ExecuteAsync(sqlInsert, new
                    {
                        Date = DateTime.UtcNow,
                        message.Id
                    });
                }
            }
        }
    }
}