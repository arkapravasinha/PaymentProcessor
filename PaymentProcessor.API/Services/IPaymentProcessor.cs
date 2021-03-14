using PaymentProcessor.Data.Entities;

namespace PaymentProcessor.API.Services
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(Payment payment);
    }
}