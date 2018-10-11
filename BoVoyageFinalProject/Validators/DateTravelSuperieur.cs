using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTravelSuperieur : CompareAttribute
    {
        public DateTravelSuperieur(string otherProperty) : base(otherProperty)
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date1 = (DateTime)value;
            var date2 = (DateTime)validationContext.ObjectType.GetProperty(OtherProperty)
                .GetValue(validationContext.ObjectInstance);

            return (date1 >= date2) 
                ? ValidationResult.Success
                : new ValidationResult("La date de retour ne peut être inférieure à la date de départ");
        }
    }
}