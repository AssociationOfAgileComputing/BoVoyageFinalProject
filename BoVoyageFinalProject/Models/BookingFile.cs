using BoVoyageFinalProject.Validators;
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
        public BookingFile()
        {
            Insurances = new List<Insurance>();
            Travellers = new List<Traveller>();
        }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
                
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel Travel { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Numéro de carte de crédit")]
		[CreditCardCustomAttribute]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Prix total")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Nombre de participants")]
        public int TravellersNumber { get; set; }

        [Display(Name = "Faites vous partie du voyage ?")]
        public bool IsCustomerTraveller { get; set; }

		[Required]
		[Display(Name = "Etat du dossier")]
		public string BookingFileState { get; set; }

		
		[Display(Name = "Raison de l'annulation")]
		public string BookingFileCancellationReason { get; set; }


		public void GetTotalPrice()
        {
            int totalTravellers = TravellersNumber;
            if (IsCustomerTraveller)
            {
                totalTravellers++;
            }

            this.TotalPrice = ((Travel.Price * totalTravellers));
        }

        public bool CheckPlaceNumber(int placeNumber)
        {
            int places = TravellersNumber;
            if (IsCustomerTraveller)
            {
                places++;
            }
            return placeNumber >= places;
        }

        public void CheckSolvency()
        {
        }

        public void GrantBookingFile()
        {
        }

        public void BookingCancellation(string reason)
        {
        }

        public ICollection<Traveller> Travellers { get; set; }

        public ICollection<Insurance> Insurances { get; set; }
    }

    

   
}