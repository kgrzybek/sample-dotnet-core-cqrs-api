using System.Threading.Tasks;
using Autofac;
using MediatR;
using SampleProject.Application;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Infrastructure.Processing
{
    public static class QueriesExecutor
    {
        public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}