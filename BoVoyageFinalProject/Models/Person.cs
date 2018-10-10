﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoVoyageFinalProject.Models
{
    public abstract class Person : BaseModel
    {


        [Display(Name ="Civilité")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Prénom")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z][a-z\D\-\'][^$@#^%§!\p{P}\*""]+$")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Nom")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z][a-z\D\-\'][^$@#^%§!\p{P}\*""]+$")]
        public string LastName { get; set; }


        [Index(IsUnique = true)]
        [EmailAddress(ErrorMessage = "Le mail existe déjà.")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(150, ErrorMessage = "Le champ {0} doit contenir {1} caractères maximum.")]
        [Display(Name = "Adresse E-mail")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Le format n'est pas bon.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Mot de Passe")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "{0} incorrect.")]
        [StringLength(150)]
        public string Password { get; set; }


        [NotMapped]
        [Display(Name = "Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="La confirmation n'est pas bonne.")]
        public string ConfirmedPassword { get; set; }

    }
}