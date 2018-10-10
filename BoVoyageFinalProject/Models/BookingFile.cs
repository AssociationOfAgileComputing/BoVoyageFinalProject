using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Display(Name = "Numéro de carte de crédit")]
        [StringLength(16)]
        //[DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Prix par personne")]
        public decimal PricePerPerson { get; set; }

        [Display(Name = "Prix total")]
        [StringLength(10)]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Nombre de participants")]
        [StringLength(2)]
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

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Traveller> Travellers { get; set; }

        public ICollection<Insurance> Insurances { get; set; }
    }
}