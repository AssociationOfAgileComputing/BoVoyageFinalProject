using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoVoyageFinalProject.Areas.BackOffice.Models
{
    public class AuthenticationLoginViewModels
    {
        [Display(Name ="E-mail")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Mail { get; set; }

        [Display(Name ="Mot de Passe")]
        [Required(ErrorMessage ="Le champ {0} est obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}