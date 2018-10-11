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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}")]
        public DateTime DateGo { get; set; }

        [Required]
        [Display(Name = "Date retour")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}")]
        public DateTime DateBack { get; set; }

        [Required]
        [Display(Name = "Place disponible")]
        public int SpaceAvailable { get; set; }

        [Display(Name = "Prix")]
        public decimal? Price { get; set; }

        public ICollection<BookingFile> BookingFiles { get; set; }

        public int TravelAgencyID { get; set; }

        [ForeignKey("TravelAgencyID")]
        public TravelAgency TravelAgency { get; set; }

        public int DestinationID { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }
}