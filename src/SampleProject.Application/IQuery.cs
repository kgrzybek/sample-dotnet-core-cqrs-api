using MediatR;

namespace SampleProject.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}