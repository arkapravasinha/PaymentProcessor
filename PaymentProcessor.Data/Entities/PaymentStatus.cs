using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaymentProcessor.Data.Entities
{
    public class PaymentStatus
    {
        [ForeignKey("Payment")]
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
