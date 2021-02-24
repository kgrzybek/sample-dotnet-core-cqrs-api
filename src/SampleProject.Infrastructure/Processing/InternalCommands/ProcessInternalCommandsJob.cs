using Quartz;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Processing.InternalCommands
{
    [DisallowConcurrentExecution]
    public class ProcessInternalCommandsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInternalCommandsCommand());
        }
    }
}