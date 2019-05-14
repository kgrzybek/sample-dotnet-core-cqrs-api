using System;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.InternalCommands
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}