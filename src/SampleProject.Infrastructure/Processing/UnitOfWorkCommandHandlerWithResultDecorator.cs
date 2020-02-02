using System.Threading;
using System.Threading.Tasks;
using SampleProject.Application;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            ICommandHandler<T, TResult> decorated, 
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await this._decorated.Handle(command, cancellationToken);

            await this._unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}