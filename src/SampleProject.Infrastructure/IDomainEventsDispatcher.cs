using System.Threading.Tasks;

namespace SampleProject.Infrastructure
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}