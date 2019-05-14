using System;
using System.Threading.Tasks;

namespace SampleProject.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(Guid id);

        Task AddAsync(Payment payment);
    }
}