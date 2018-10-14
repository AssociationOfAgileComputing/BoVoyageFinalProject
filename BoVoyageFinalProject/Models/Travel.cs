using BoVoyageFinalProject.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class Travel : BaseModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Aller")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateGo { get; set; }

        [Required]
        [Display(Name = "Date retour")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]    
        [DateTravelSuperieur("DateGo")]
        public DateTime DateBack { get; set; }

        [Required]
        [Display(Name = "Place(s) Disponible(s)")]
        [Range(0,int.MaxValue,ErrorMessage = "Valeur invalide")]
        public int SpaceAvailable { get; set; }

        [Required]
        [Display(Name = "Prix")]
        [Price(ErrorMessage = "Valeur invalide")]
        public decimal Price { get; set; }

        public ICollection<BookingFile> BookingFiles { get; set; }

        [Display(Name = "Agence")]
        public int TravelAgencyID { get; set; }

        [ForeignKey("TravelAgencyID")]
        public TravelAgency TravelAgency { get; set; }

        [Display(Name = "Destination")]
        public int DestinationID { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }

        [NotMapped]
        public string TravelDetails
        {
            get
            {
                return string.Format("{0} - {1} | {2}", DateGo.ToString("dd/MM/yyyy"), DateBack.ToString("dd/MM/yyyy"), Destination.Area);
            }
        }
    }
}