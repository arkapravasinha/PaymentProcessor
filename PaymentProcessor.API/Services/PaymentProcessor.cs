using PaymentProcessor.API.PaymentGateways;
using PaymentProcessor.API.Repositories;
using PaymentProcessor.Data.Entities;
using PaymentProcessor.Data.Enums;
using System;

namespace PaymentProcessor.API.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentProcessor(IExpensivePaymentGateway expensivePaymentGateway,
            ICheapPaymentGateway cheapPaymentGateway,
            IUnitOfWork unitOfWork)
        {
            _expensivePaymentGateway = expensivePaymentGateway;
            _cheapPaymentGateway = cheapPaymentGateway;
            _unitOfWork = unitOfWork;
        }

        public void ProcessPayment(Payment payment)
        {
            var paymentId = _unitOfWork.PaymentRepository.AddPaymentDetails(payment);
            if (payment.Amount <= 20)
                ProcessAmountLessThanOrEquals20(paymentId);
            else if (payment.Amount > 20 && payment.Amount <= 500)
                ProcessAmountGreaterThan20AndLessThanEqualTo500(paymentId);
            else
                ProcessAmountGreaterThan500(paymentId);
        }

        private void ProcessAmountGreaterThan500(int paymentId)
        {
            var results = RetryMechanism.RetryWithSame(() =>
                _expensivePaymentGateway.ProcessPayment(paymentId), 3);
            if (!results)
                ProcessFailedPayment(paymentId);
        }

        private void ProcessAmountGreaterThan20AndLessThanEqualTo500(int paymentId)
        {
            var results=RetryMechanism.RetryWithAlternate(() =>
                _expensivePaymentGateway.ProcessPayment(paymentId),
                () =>
                _cheapPaymentGateway.ProcessPayment(paymentId));
                if(!results)
                    ProcessFailedPayment(paymentId);
        }

        private void ProcessAmountLessThanOrEquals20(int paymentId)
        {
            var results = RetryMechanism.RetryWithSame(() => _cheapPaymentGateway.ProcessPayment(paymentId), 0);
            if (!results)
                ProcessFailedPayment(paymentId);
        }

        private void ProcessFailedPayment(int paymentId)
        {
            try
            {
                _unitOfWork.PaymentRepository.UpdatePaymentStatus(paymentId, PaymentStatusValues.Failed);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
