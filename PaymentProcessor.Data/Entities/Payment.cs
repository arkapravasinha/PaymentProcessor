using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaymentProcessor.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [MaxLength(3)]
        public string SecurityCode { get; set; }

        [Range(0,double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }
    }
}
