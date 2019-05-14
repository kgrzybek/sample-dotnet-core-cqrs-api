using System;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.InternalCommands;

namespace SampleProject.API.InternalCommands
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task EnqueueAsync(IRequest command)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sqlInsert = "INSERT INTO [app].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                         "(@Id, @EnqueueDate, @Type, @Data)";

                await connection.ExecuteAsync(sqlInsert, new
                {
                    Id = Guid.NewGuid(),
                    EnqueueDate = DateTime.UtcNow,
                    Type = command.GetType().FullName,
                    Data = JsonConvert.SerializeObject(command)
                });
            }
        }
    }
}