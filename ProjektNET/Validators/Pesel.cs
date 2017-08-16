using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektNET.Validators
{
    public class Pesel : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            string pesel = (String)value;
            if (pesel.Length != 11)
                return new ValidationResult(ErrorMessage);
            else if (((((pesel[0] - 48) * 9 + (pesel[1] - 48) * 7 + (pesel[2] - 48) * 3 + (pesel[3] - 48) + (pesel[4] - 48) * 9 + (pesel[5] - 48) * 7 + (pesel[6] - 48) * 3 + (pesel[7] - 48) + (pesel[8] - 48) * 9 + (pesel[9] - 48) * 7) % 10 == 0 && ((pesel[10] - 48) != 0))
                    || ((pesel[0] - 48) * 9 + (pesel[1] - 48) * 7 + (pesel[2] - 48) * 3 + (pesel[3] - 48) + (pesel[4] - 48) * 9 + (pesel[5] - 48) * 7 + (pesel[6] - 48) * 3 + (pesel[7] - 48) + (pesel[8] - 48) * 9 + (pesel[9] - 48) * 7) % 10 != (pesel[10] - 48)))
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}