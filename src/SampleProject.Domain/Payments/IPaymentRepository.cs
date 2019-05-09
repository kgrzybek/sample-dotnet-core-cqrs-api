using System;
using System.Threading.Tasks;

namespace SampleProject.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
    }
}