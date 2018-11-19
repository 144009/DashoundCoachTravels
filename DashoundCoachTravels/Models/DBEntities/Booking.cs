using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models.DBEntities
{
    [Table("Reservations")]
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter number of seats you want to book")]
        [Range(1, 999, ErrorMessage = "Must be greater than 0")]
        [Display(Name = "Number of seats to book")]
        public int NumPeople { get; set; }

        [Required(ErrorMessage = "Enter the date of reservation")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date")]
        public DateTime DateBooked { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Advance (optional)")]
        public float? Advance { get; set; }

        [DataType(DataType.DateTime)]
        //"<input type="Date" name="???" />" .cshtml
        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Date of payment: advance")]
        public DateTime? DatePayedAdvance { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(NullDisplayText = "-")]
        [Display(Name = "Date of payment: full")]
        public DateTime? DatePayedFull { get; set; }

        [Required(ErrorMessage = "Enter status of booking")]
        [StringLength(100)]
        [Display(Name = "Reservation Status")]
        public string Status { get; set; }

        [ForeignKey("TripsId")]
        public int Id_Trip { get; set; }
        [ForeignKey("UsersId")]
        public string Id_User { get; set; }
        public Trip TripsId { get; set; }
        public ApplicationUser UsersId { get; set; }
    }
}