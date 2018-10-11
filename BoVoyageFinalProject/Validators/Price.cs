using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Price : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var x = (decimal)value;
            return x > decimal.Zero;
        }
    }
}