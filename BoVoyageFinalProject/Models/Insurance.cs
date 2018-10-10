using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BoVoyageFinalProject.Models
{
	public class Insurance:BaseModel
	{
		public decimal InsuranceCost { get; set; }
        [EnumDataType(typeof(InsuranceType))]
        public InsuranceType InsuranceType { get; set; }
    }

    public enum InsuranceType
    {
        [EnumMember(Value = "Annulation")]
        CANCELLATION
    }
}