using PaymentProcessor.Data.Entities;

namespace PaymentProcessor.API.PaymentGateways
{
    public interface ICheapPaymentGateway
    {
        bool IsAvailable { get; }

        void ProcessPayment(int paymentId);
    }
}