using Autofac;
using MediatR;
using SampleProject.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Processing
{
    public static class QueriesExecutor
    {
        public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            using (ILifetimeScope scope = CompositionRoot.BeginLifetimeScope())
            {
                IMediator mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}