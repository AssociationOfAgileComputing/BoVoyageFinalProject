using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Models
{
    public class Destination : BaseModel
    {
        [Required]
        [Display(Name = "Continent")]
        public string Continent { get; set; }

        [Required]
        [Display(Name = "Pays")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string Area { get; set; }

        [StringLength(250)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        public ICollection<Travel> Travels { get; set; }

        [Display(Name = "Images")]
        public ICollection<DestinationPicture> Pictures { get; set; }
    }
}