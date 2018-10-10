using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class Destination : BaseModel
    {
        [Required]
        [Display(Name = "Continent")]
        public string Continent { get; set; }

        [Required]
        [Display(Name = "Pays")]
        public string Pays { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [StringLength(250)]
        [Display(Name = "description")]
        public string Description { get; set; }

        public List<Travel> Travels { get; set; }

        [Display(Name = "Images")]
        public ICollection<DestinationPicture> Pictures { get; set; }
    }
}