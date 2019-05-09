using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Infrastructure
{
    public class DomainEventsDispatcherCommandHandlerDecorator<T> : IRequestHandler<T, Unit> where T:IRequest
    {
        private readonly IRequestHandler<T, Unit> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        public DomainEventsDispatcherCommandHandlerDecorator(
            IRequestHandler<T, Unit> decorated, 
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(command, cancellationToken);

            await this._unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}