using PaymentProcessor.Data;

namespace PaymentProcessor.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(PaymentProcessorDbContext paymentProcessorDbContext,IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        private IPaymentRepository _paymentRepository;

        public IPaymentRepository PaymentRepository
        {
            get { return _paymentRepository; }
        }

    }
}
