using MediatR;
using SampleProject.Application;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Infrastructure.Processing.Outbox;

namespace SampleProject.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}