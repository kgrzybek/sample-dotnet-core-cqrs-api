using System.Threading.Tasks;
using MediatR;

namespace SampleProject.Infrastructure.InternalCommands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(IRequest command);
    }
}