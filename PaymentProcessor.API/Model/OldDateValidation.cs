using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentProcessor.API.Model
{
    public class OldDateValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!DateTime.TryParse(value?.ToString(), out DateTime dtValue))
                return false;
            TimeSpan diff = DateTime.UtcNow - dtValue;
            if (diff.TotalDays > 1)
                return false;
            return true;
        }
    }
}
