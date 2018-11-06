using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models.DBEntities
{
    [Table("Trip_Location")]
    public class Trip_Locations
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Sightseeing order")]
        public int Number { get; set; } // shows order of places to sightsee

        [ForeignKey("Trip")]
        public int TripId { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }
        public Trip Trip { get; set; }
    }
}