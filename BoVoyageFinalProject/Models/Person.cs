using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoVoyageFinalProject.Models
{
    public abstract class Person : BaseModel
    {


        [Display(Name = "Civilité")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Prénom")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z][a-z^\d\-\'^($@#^%§!\*""\p{P})]+$", ErrorMessage = "Le nom doit contenir au minimum 2 lettres, commencer par une majuscule et ne pas contenir de caractères spéciaux")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Nom")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z][a-z^\d\-\'^($@#^%§!\*""\p{P})]+$",ErrorMessage="Le nom doit contenir au minimum 2 lettres, commencer par une majuscule et ne pas contenir de caractères spéciaux")]
        public string LastName { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstName);
            }
        }

    }
}