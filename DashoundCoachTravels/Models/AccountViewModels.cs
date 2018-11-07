using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DashoundCoachTravels.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel // logging requires username and password
    {
        [Required]
        [Display(Name = "Your user name")]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Your password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel // registering requires: username, email, password, name, surname, country,
        // town, street, house number, zipcode. Those fields were added below to extend the basic 
        // RegisterViewModel
    {
        //NEW
        [Required(ErrorMessage = "Enter your user name.")]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Enter a valid email.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter a password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //ALL FIELDS BELOW ARE NEW

        [Required(ErrorMessage = "Enter your name.")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname.")]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your country.")]
        [StringLength(100)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter your town name.")]
        [StringLength(100)]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Enter your street name.")]
        [StringLength(100)]
        [Display(Name = "Street name")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Enter your house number.")]
        [Display(Name = "House number")]
        public string NumHouse { get; set; }

        [Display(Name = "Enter your flat number (optional)")]
        public string NumFlat { get; set; }

        [Required(ErrorMessage = "Enter your postal code")]
        [StringLength(100)]
        [Display(Name = "Postal code")]
        public string ZIPCode { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Enter your email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Enter your email to recieve a message.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
