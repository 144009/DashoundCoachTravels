using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models.DBEntities
{
    [Table("Trips")]
    public class Trip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the name of trip")]
        [StringLength(100)]
        [Display(Name = "Trip name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter number of spots for the trip")]
        [Range(1, 999, ErrorMessage = "Must be greater than 0")]
        [Display(Name = "Max number of spots in trip")]
        public int NumSpots { get; set; }

        [Required(ErrorMessage = "Enter price of the trip")]
        [DataType(DataType.Currency)]
        [Display(Name = "Trip price")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Enter date of departure")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Departure Date")]
        public DateTime DateDeparture { get; set; }

        [Required(ErrorMessage = "Enter end of trip date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Back Date")]
        public DateTime DateBack { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Trip description (optional)")]
        public string Description { get; set; }

        [Display(Name = "Banner (optional)")]
        public string BannerBig { get; set; }

        [Display(Name = "Thumbnail (optional)")]
        public string BannerSmall { get; set; }

        [Required(ErrorMessage = "Enter designated Coach")]
        [Display(Name = "Coach Vehicle Number")]
        public int CoachNumberId { get; set; }
    }
}