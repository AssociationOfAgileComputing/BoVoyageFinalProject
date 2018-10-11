using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class Customer : User
    {
        [Required(ErrorMessage ="Le champ{0} est obligatoire.")]
        [Display(Name ="Date de naissance")]
        [DataType(DataType.Date)]
        [Column(TypeName ="datetime2")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name ="Numéro de téléphone")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Numéro de téléphone invalide")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name ="Adresse ligne 1")]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Adresse ligne 2")]
        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Code Postal")]
        [DataType(DataType.PostalCode, ErrorMessage ="Code Postal invalide")]
        [StringLength(10)]
        public string ZIPCode { get; set; }

        [Required]
        [Display(Name ="Ville")]
        [StringLength(50)]
        public string City { get; set; }


        [Required]
        [Display(Name ="Pays")]
        [StringLength(50)]
        public string Country { get; set; }

        [Display(Name ="Dossiers de réservation")]
        public ICollection<BookingFile>BookingFiles { get; set; }

    }
}