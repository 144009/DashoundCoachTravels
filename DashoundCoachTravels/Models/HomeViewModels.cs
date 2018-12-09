using DashoundCoachTravels.Models.DBEntities;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models
{
    public class FleetViewModel
    {
        public IEnumerable<Coach> List { get; set; }

        [Required(ErrorMessage = "Enter vehicle brand")]
        [StringLength(100)]
        [Display(Name = "Vehicle brand")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Enter vehicle model")]
        [StringLength(100)]
        [Display(Name = "Vehicle model")]
        public string VehModel { get; set; }

        [Display(Name = "Vehicle Photo (optional)")]
        public string VehScreenshot { get; set; }
    }

    public class StatisticsViewModel
    {
        [Display(Name = "Number of active user accounts")]
        public int TotalUsers { get; set; }

        [Display(Name = "Number of active employee accounts")]
        public int TotalEmployee { get; set; }

        [Display(Name = "Number of avaliable locations")]
        public int TotalLocations { get; set; }

        [Display(Name = "Number of vehicles")]
        public int TotalVehicles { get; set; }

        [Display(Name = "Average vehicle seats")]
        public int AvgVehicleSeats { get; set; }

        [Display(Name = "Number of trips")]
        public int TotalTrips { get; set; }

        [Display(Name = "Number of trips that are yet to end")]
        public int TotalActiveTrips { get; set; }

        [Display(Name = "Number of spots in all trips")]
        public int TotalTripsSpots { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Average prce of a trip")]
        public float AvgTripPrice { get; set; }

        [Display(Name = "Average trip spots")]
        public int AvgTripSpots { get; set; }

        [Display(Name = "Number of spots reserved for trips")]
        public int TotalReservationSpotsBooked { get; set; }

        [Display(Name = "Average % of spots reserved in total")]
        public int AvgTotalSpotsReserved { get; set; }

        [Display(Name = "Number of reservations")]
        public int TotalTripsBooked { get; set; }

        [Display(Name = "Number of reservations in last month")]
        public int TotalReservationsLastMonth { get; set; }

        [Display(Name = "Number of reservations in last year")]
        public List<ReservationsInMonth> ReservationsPerMonth { get; set; }

        public Highcharts ReservationChart { get; set; }
    }

    public class ReservationsInMonth
    {
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Month name")]
        public string MonthName { get; set; }

        [Display(Name = "Reservations")]
        public int MonthTotalReservations { get; set; }

    }

}