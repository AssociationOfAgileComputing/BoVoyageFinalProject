using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class Traveller:Person
    {
        public Traveller()
        {
            BookingFiles = new List<BookingFile>();
        }

        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage ="Le champ {0} est obligatoire.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Index(IsUnique = true)]
        [EmailAddress(ErrorMessage = "Le mail existe déjà.")]
        [StringLength(150, ErrorMessage = "Le champ {0} doit contenir {1} caractères maximum.")]
        [Display(Name = "Adresse E-mail")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Le format n'est pas bon.")]
        public string Mail { get; set; }

        [Display(Name = "Dossiers de réservation")]
        public ICollection<BookingFile> BookingFiles { get; set; }
    }
}