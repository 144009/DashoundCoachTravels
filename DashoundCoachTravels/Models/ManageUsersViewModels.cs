using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashoundCoachTravels.Models
{
    public class ManageUsersViewModels
    {
    }

    // this model will be used to display fields editable by the administrator - most importantly user role types
    public class EditUserRoleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Street name")]
        public string Street { get; set; }

        [Display(Name = "House number")]
        public string NumHouse { get; set; }

        [Display(Name = "Flat number (optional)")]
        public string NumFlat { get; set; }

        [Display(Name = "Postal code")]
        public string ZIPCode { get; set; }

        [Display(Name = "Permission level")]
        public UserRoleTypes RoleType { get; set; }
    }

    public enum UserRoleTypes
    {
        Customer,
        Employee,
        Administrator
    }
}