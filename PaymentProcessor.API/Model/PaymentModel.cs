using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentProcessor.API.Model
{
    public class PaymentModel
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        [OldDateValidation]
        public DateTime ExpirationDate { get; set; }

        [MaxLength(3)]
        public string SecurityCode { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
