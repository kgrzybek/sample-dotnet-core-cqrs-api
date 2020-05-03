using System.Reflection;
using SampleProject.Application.Orders.PlaceCustomerOrder;

namespace SampleProject.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
    }
}