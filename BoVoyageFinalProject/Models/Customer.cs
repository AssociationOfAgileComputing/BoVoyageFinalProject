using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class Customer : Person
    {
        [Required(ErrorMessage ="Le champ{0} est obligatoire.")]
        [Display(Name ="Date de naissance")]
        [DataType(DataType.Date)]
        [Column(TypeName ="datetime2")]
        public DateTime BirthDate { get; set; }

        [CreditCard(ErrorMessage="Numéro de carte invalide.")]
        public string CreditCardNumber { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name ="Dossiers de réservation")]
        public ICollection<BookingFile>BookingFiles { get; set; }

    }
}