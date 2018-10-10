using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Models
{
    public class TravelAgency : BaseModel
    {
        [Required]
        [Display(Name = "Agence")]
        public string Name { get; set; }

        public ICollection<Travel> Travel { get; set; }
    }
}