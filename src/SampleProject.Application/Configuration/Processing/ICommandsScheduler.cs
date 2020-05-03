using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}