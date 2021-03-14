using PaymentProcessor.Data.Entities;

namespace PaymentProcessor.API.PaymentGateways
{
    public interface IExpensivePaymentGateway
    {
        bool IsAvailable { get; }

        void ProcessPayment(int paymentId);
    }
}