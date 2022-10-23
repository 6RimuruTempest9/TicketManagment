using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.ViewModels.User
{
    public class EditUserViewModel
    {
        [Required]
        public string TimeZone { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}