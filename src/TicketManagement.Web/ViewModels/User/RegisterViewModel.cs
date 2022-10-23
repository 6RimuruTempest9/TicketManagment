using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password confirm")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Language")]
        [DataType(DataType.Text)]
        public string Language { get; set; }

        [Required]
        [Display(Name = "Time zone")]
        public string TimeZone { get; set; }
    }
}
