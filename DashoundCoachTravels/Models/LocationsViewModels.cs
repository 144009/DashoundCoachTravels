using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models
{
    public class LocationsViewModels
    {
        public IEnumerable<Location> List { get; set; }

        [Required(ErrorMessage = "Destination Country Name")]
        [StringLength(100)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Destination Town Name")]
        [StringLength(100)]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Destination Name")]
        [StringLength(100)]
        [Display(Name = "Place Name")]
        public string Name { get; set; } // example: Colosseum 

        /*[DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Place Description")]
        public string Description { get; set; }*/

        [Display(Name = "Location Photo (optional)")]
        public string LocationImage { get; set; }
    }
}