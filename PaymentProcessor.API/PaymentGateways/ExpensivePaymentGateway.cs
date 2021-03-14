using PaymentProcessor.API.Repositories;
using PaymentProcessor.Data.Entities;
using PaymentProcessor.Data.Enums;

namespace PaymentProcessor.API.PaymentGateways
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {

        private bool _isAvailable = true;
        private readonly IUnitOfWork _unitOFWork;

        public ExpensivePaymentGateway(IUnitOfWork unitOFWork)
        {
            _unitOFWork = unitOFWork;
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            private set { _isAvailable = value; }
        }


        public void ProcessPayment(int paymentId)
        {
            _unitOFWork.PaymentRepository.UpdatePaymentStatus(paymentId, PaymentStatusValues.Processed);
        }
    }
}
