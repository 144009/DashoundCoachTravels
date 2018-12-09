using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashoundCoachTravels.Models
{
    public class TripsViewModels
    {
        [StringLength(100)]
        [Display(Name = "Trip name")]
        public string Name { get; set; }

        [Range(1, 999, ErrorMessage = "Must be greater than 0")]
        [Display(Name = "Max number of spots in trip")]
        public int NumSpots { get; set; }

        [Display(Name = "Number of spots left in trip")]
        public int NumSpotsLeft{ get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Trip price")] 
        public float Price { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Maximum Trip price")] //used in search query SEARCH BY PRICE
        public float PriceMax { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Minimum Trip price")] //used in search query SEARCH BY PRICE
        public float PriceMin { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Departure Date")]
        public DateTime DateDeparture { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Back Date")]
        public DateTime DateBack { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Trip description (optional)")]
        public string Description { get; set; }

        [Display(Name = "Banner (optional)")]
        public string Banner { get; set; }

        [StringLength(100)]
        [Display(Name = "Country")]
        public string Country { get; set; } // Destination country for trip

        [StringLength(100)]
        [Display(Name = "Town")]
        public string Town { get; set; } //destination town for trip

        [Display(Name = "Coach Vehicle Number")]
        public string CoachModel { get; set; }

        [Display(Name = "Coach Vehicle Banner")]
        public string CoachBanner { get; set; }

        public IEnumerable<ViewEditTripsViewModel> List { get; set; } // to create a list of results for price search

    }
    public class ViewEditTripsViewModel
    {
        public Trip TripInstance { get; set; }

        [Display(Name = "Number of reservations")]
        public int NumberOfReservations { get; set; }

        [Display(Name = "Number of spots left in trip")]
        public int NumSpotsLeft { get; set; }

        [Display(Name = "Coach Vehicle Number")]
        public string CoachModel { get; set; }

        [Display(Name = "Coach Vehicle Banner")]
        public string CoachBanner { get; set; }

        [Display(Name = "Route")]
        public TripLocationsViewModels Route { get; set; }

        public IEnumerable<SelectListItem> CoachVehicleIdList { get; set; }
    }


}