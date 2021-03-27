using Microsoft.EntityFrameworkCore;
using PaymentProcessor.Data;
using PaymentProcessor.Data.Entities;
using PaymentProcessor.Data.Enums;
using System;
using System.Linq;

namespace PaymentProcessor.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentProcessorDbContext _paymentProcessorDbContext;

        public PaymentRepository(PaymentProcessorDbContext paymentProcessorDbContext)
        {
            _paymentProcessorDbContext = paymentProcessorDbContext;
        }

        public int AddPaymentDetails(Payment payment)
        {
            // Added by Dipankar on Mar 27, 2021
            if (payment == null)
                return 0;
            payment.PaymentStatus = new PaymentStatus()
            {
                Status = PaymentStatusValues.Pending.ToString(),
                Payment = payment
            };
            _paymentProcessorDbContext.Payments.Add(payment);
            _paymentProcessorDbContext.SaveChanges();
            return payment.Id;
        }

        public void UpdatePaymentStatus(int paymentId,PaymentStatusValues paymentStatusValues)
        {
            var payment = _paymentProcessorDbContext.Payments
                            .Include(o => o.PaymentStatus)
                            .FirstOrDefault(o => o.Id.Equals(paymentId));
            if(payment!=null)
            {
                payment.PaymentStatus.Status = paymentStatusValues.ToString();
                _paymentProcessorDbContext.SaveChanges();
            }
        }
    }
}
