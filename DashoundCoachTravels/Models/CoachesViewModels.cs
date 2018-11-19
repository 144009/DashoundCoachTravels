using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models
{
    public class CoachesViewModels
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

        [Required(ErrorMessage = "Enter number of seats")]
        [Range(1, 999, ErrorMessage = "Must be greater than 0")]
        [Display(Name = "Max number of seats")]
        public int Seats { get; set; }

        /*[Required(ErrorMessage = "Enter the date of acquiring the vehicle")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Acquired")]
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Enter vehicle number")]
        [Range(1, 999, ErrorMessage = "Must be greater than 0")]
        [Display(Name = "Vehicle Number")]
        public int VehicleNumber { get; set; }*/

        [Display(Name = "Vehicle Photo (optional)")]
        public string VehScreenshot { get; set; }
    }
}