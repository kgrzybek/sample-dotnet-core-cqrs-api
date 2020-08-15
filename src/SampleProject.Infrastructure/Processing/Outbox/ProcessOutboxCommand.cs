using MediatR;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}