using System;
using System.Threading.Tasks;

namespace SampleProject.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(PaymentId id);

        Task AddAsync(Payment payment);
    }
}