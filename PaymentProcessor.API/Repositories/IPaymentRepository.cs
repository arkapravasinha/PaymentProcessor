using PaymentProcessor.Data.Entities;
using PaymentProcessor.Data.Enums;

namespace PaymentProcessor.API.Repositories
{
    public interface IPaymentRepository
    {
        int AddPaymentDetails(Payment payment);
        void UpdatePaymentStatus(int paymentId, PaymentStatusValues paymentStatusValues);
    }
}