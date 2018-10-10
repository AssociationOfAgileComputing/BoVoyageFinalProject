using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
	public class BookingFile : BaseModel

	{
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public Customer Customer { get; set; }


		public int TravellerId { get; set; }
		[ForeignKey("TravellerId")]
		public Traveller Traveller { get; set; }

		public int TravelId { get; set; }
		[ForeignKey("TravelId")]
		public Travel Travel { get; set; }

		public string CreditCardNumber { get; set; }

		public decimal PricePerPerson { get; set; }

		public decimal TotalPrice { get; set; }

		public int TravellersNumber { get; set; }

		public int? InsuranceId { get; set; }
		[ForeignKey("InsuranceId")]
		public Insurance Insurance { get; set; }

		public enum BookingFileState
		{
			PENDING,
			INPROGRESS,
			DENIED,
			GRANTED
		}

		public enum BookingFileCancellationReason
		{
			CUSTOMER,
			UNAVAILABLEPLACES
		}

		public void GetTotalPrice()
		{

			this.TotalPrice = ((this.PricePerPerson * TravellersNumber));
		}

		public void CheckSolvency()
		{
		}

		public void GrantBookingFile()
		{

		}

		public void BookingCancellation(BookingFileCancellationReason reason)
		{

		}

	}
}