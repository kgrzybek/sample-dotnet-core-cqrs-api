using System.Threading.Tasks;
using MediatR;

namespace SampleProject.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(IRequest command);
    }
}