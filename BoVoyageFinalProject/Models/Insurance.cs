using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BoVoyageFinalProject.Models
{
	public class Insurance : BaseModel
	{
		public decimal InsuranceCost { get; set; }

		[Required]
		[Display(Name = "Type d'assurance")]
		public string InsuranceType { get; set; }

		public ICollection<BookingFile> BookingFiles { get; set; }
	}


}