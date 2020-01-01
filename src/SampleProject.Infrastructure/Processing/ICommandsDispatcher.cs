using System;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
