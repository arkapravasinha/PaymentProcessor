using PaymentProcessor.API.Repositories;
using PaymentProcessor.Data.Entities;
using PaymentProcessor.Data.Enums;

namespace PaymentProcessor.API.PaymentGateways
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        private bool _isAvailable = true;
        private readonly IUnitOfWork _unitOFWork;

        public CheapPaymentGateway(IUnitOfWork unitOFWork)
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
