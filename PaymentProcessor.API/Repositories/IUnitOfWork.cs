namespace PaymentProcessor.API.Repositories
{
    public interface IUnitOfWork
    {
        IPaymentRepository PaymentRepository { get; }
    }
}