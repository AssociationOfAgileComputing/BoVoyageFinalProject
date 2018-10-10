using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
	public class Insurance:BaseModel
	{
		public decimal InsuranceCost { get; set; }

		public enum InsuranceType
		{
			CANCELLATION
		}
	}
}