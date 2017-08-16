using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektNET.Validators
{
    public class Min : ValidationAttribute
    {
        private double min;

        public Min(double min)
        {
            this.min = min;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            double wartosc = (double)value;
            if(min > wartosc)
                    return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}