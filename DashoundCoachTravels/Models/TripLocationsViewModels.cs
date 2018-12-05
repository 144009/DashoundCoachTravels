using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models
{
    public class TripLocationsViewModels
    {
        public int Id_Trip { get; set; }
        public string TripName { get; set; }

        public List<TripLocationsInstanceViewModels> ListElement { get; set; }

    }

    public class TripLocationsInstanceViewModels
    {
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Place Name")]
        public string Name { get; set; } // example: Colosseum 

        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Place Description")]
        public string Description { get; set; }

        [Display(Name = "Location Photo (optional)")]
        public string LocationImage { get; set; }

        [Display(Name = "Sightseeing order")]
        public int Number { get; set; } // shows order of places to sightsee
        public string TripName { get; set; }

        public int RouteInstanceId { get; set; }


    }

    public class AddTripLocationsViewModel
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

        [Display(Name = "Location Photo (optional)")]
        public string LocationImage { get; set; }

        [Display(Name = "Sightseeing order")]
        public int Number { get; set; } // shows order of places to sightsee

        public int TripId { get; set; }
    }
}