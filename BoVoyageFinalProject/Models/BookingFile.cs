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
                
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel Travel { get; set; }

        [Required]
        [Display(Name = "Numéro de carte de crédit")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Prix par personne")]
        public decimal PricePerPerson { get; set; }

        [Display(Name = "Prix total")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Nombre de participants")]
        public int TravellersNumber { get; set; }

        public int? InsuranceId { get; set; }

        [ForeignKey("InsuranceId")]
        public Insurance Insurance { get; set; }

        public BookingFileState BookingFileState { get; set; }

        

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
}