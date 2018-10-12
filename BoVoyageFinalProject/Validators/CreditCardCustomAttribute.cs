using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CreditCardCustomAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var number = (string)value;
            if (number != null && number.Length <= 16 && number.Length >= 19)
            {
                return new ValidationResult("Le numéro de carte de crédit doit contenir entre 16 et 19 caractères");
            }
            return ValidationResult.Success;
        }

    }
}