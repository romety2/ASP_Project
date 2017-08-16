using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektNET.Validators
{
    public class Napis : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            string napis = (String)value;
            int i = 0;
            while (i < napis.Length)
            {
                if (
                    (napis[i] < 65 || napis[i] > 90) &&
                    (napis[i] < 97 || napis[i] > 122) &&
                    napis[i] != 'Ą' && napis[i] != 'ą' &&
                    napis[i] != 'Ć' && napis[i] != 'ć' &&
                    napis[i] != 'Ę' && napis[i] != 'ę' &&
                    napis[i] != 'Ł' && napis[i] != 'ł' &&
                    napis[i] != 'Ń' && napis[i] != 'ń' &&
                    napis[i] != 'Ó' && napis[i] != 'ó' &&
                    napis[i] != 'Ś' && napis[i] != 'ś' &&
                    napis[i] != 'Ź' && napis[i] != 'ź' &&
                    napis[i] != 'Ż' && napis[i] != 'ż'
                  )
                    return new ValidationResult(ErrorMessage);
                i++;
            }
            return ValidationResult.Success;
        }
    }
}